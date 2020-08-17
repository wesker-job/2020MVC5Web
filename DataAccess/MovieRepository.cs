using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;
using System.Data.Entity.Core.Common.CommandTrees;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations.Builders;

namespace DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        //private readonly TPCoreWebEntities _context;

        public MovieRepository()
        {
            //TPCoreWebEntities tpCoreWebEntities = new TPCoreWebEntities();
            //_context = tpCoreWebEntities;
        }

        public List<Movies> GetAll()
        {
            List<Movies> movies = new List<Movies>();

            //using (TPMVCWeb db = new TPMVCWeb())
            using (CodeFirstEF db = new CodeFirstEF())
            {
                //db.Database.Log = (log) => System.Diagnostics.Debug.WriteLine(log);

                var temp = db.Movies.FirstOrDefault();
                movies = db.Movies.OrderByDescending(c => c.ReleaseDate).ToList();
            }

            return movies;
            //return _context.Movies.OrderByDescending(c => c.ReleaseDate).ToList();
        }

        public Movies GetById(int id)
        {
            Movies movie = new Movies();

            using (CodeFirstEF db = new CodeFirstEF())
            {
                movie = db.Movies.Find(id);
                //movie = db.Movies.Where(c => c.Id == id).FirstOrDefault();
            }

            return movie ?? new Movies();
        }

        public bool DeleteSave(Movies movie)
        {
            bool result = false;

            try
            {
                using (CodeFirstEF db = new CodeFirstEF())
                {
                    var findMovie = db.Movies.Find(movie.Id);
                    if (findMovie != null)
                    {
                        db.Movies.Remove(findMovie);
                        db.SaveChanges();
                    }
                }

                result = true;
            }
            catch(Exception ex)
            {
                string error = ex.Message.ToString();
            }

            return result;
        }

        public bool CreateSave(Movies movie)
        {
            bool result = false;

            try
            {
                using (CodeFirstEF db = new CodeFirstEF())
                {
                    var findMovie = db.Movies.Find(movie.Id);
                    if (findMovie == null)
                    {
                        db.Movies.Add(movie);
                        db.SaveChanges();
                    }
                }

                result = true;
            }
            catch (Exception ex)
            { }

            return result;
        }

        public bool EditSave(Movies movie)
        {
            bool result = false;

            using (CodeFirstEF db = new CodeFirstEF())
            {
                //exec sp
                //db.MovieActors.SqlQuery("EXECUTE [dbo].[GetAllProducts]");
                using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Entry(movie).State = EntityState.Modified;
                        db.SaveChanges();

                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                    }
                }
                //db.Entry(person).Property(p => p.FirstName).IsModified = true;
                //Movies findMovie = db.Movies.Find(movie.Id);
                //if (findMovie != null)
                //{
                //    //findMovie.Title = movie.Title;
                //    //findMovie.ReleaseDate = movie.ReleaseDate;
                //    //findMovie.Genre = movie.Genre;
                //    //findMovie.Price = movie.Price;

                //    db.Entry(findMovie).CurrentValues.SetValues(movie);

                //    db.SaveChanges();
                //    result = true;
                //}
            }
            return result;
        }
    }
}
