using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

namespace DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        //private readonly TPCoreWebEntities _context;
        private readonly DbContext _context;
        private DbSet<Movies> _dbSet;
        //private IUnitOfWork _uok;

        public MovieRepository(DbContext dbContext)
        {
            //_uok = unitOfWork;
            _context = dbContext;
            _dbSet = _context.Set<Movies>();
        }

        public List<Movies> GetAll()
        {
            List<Movies> movies = new List<Movies>();
            movies = _dbSet.OrderByDescending(c => c.ReleaseDate).ToList();

            return movies;
            //using (TPMVCWeb db = new TPMVCWeb())
            //using (TPMVCWeb db = new TPMVCWeb())
            //{
            //db.Database.Log = (log) => System.Diagnostics.Debug.WriteLine(log);
            //movies = db.Movies.OrderByDescending(c => c.ReleaseDate).ToList();
            //}

            //return _context.Movies.OrderByDescending(c => c.ReleaseDate).ToList();
        }

        public Movies GetById(int id)
        {
            Movies movie = new Movies();
            movie = _dbSet.Find(id);

            //using (TPMVCWeb db = new TPMVCWeb())
            //{
            //    movie = db.Movies.Find(id);
            //    //movie = db.Movies.Where(c => c.Id == id).FirstOrDefault();
            //}

            return movie ?? new Movies();
        }

        public void Delete(Movies movie)
        {   
            var findMovie = _dbSet.Find(movie.Id);
            if (findMovie != null)
            {
                if (_context.Entry(findMovie).State == EntityState.Deleted)
                {
                    _dbSet.Attach(findMovie);
                }
                _dbSet.Remove(findMovie);
            }
        }

        public void Add(Movies movie)
        {
            using TPMVCWeb db = new TPMVCWeb();
            var findMovie = db.Movies.Find(movie.Id);
            if (findMovie == null)
            {
                db.Movies.Add(movie);
            }
        }

        public void Update(Movies movie)
        {
            Movies findMovie = _dbSet.Find(movie.Id);
            _context.Entry(findMovie).CurrentValues.SetValues(movie);
        }

        //public bool EditSave(Movies movie)
        //{
        //    bool result = false;

        //    using (TPMVCWeb db = new TPMVCWeb())
        //    {
        //        //exec sp
        //        //db.MovieActors.SqlQuery("EXECUTE [dbo].[GetAllProducts]");
        //        using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                db.Entry(movie).State = EntityState.Modified;
        //                db.SaveChanges();

        //                dbTrans.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                dbTrans.Rollback();
        //            }
        //        }
        //        //db.Entry(person).Property(p => p.FirstName).IsModified = true;
        //        //Movies findMovie = db.Movies.Find(movie.Id);
        //        //if (findMovie != null)
        //        //{
        //        //    //findMovie.Title = movie.Title;
        //        //    //findMovie.ReleaseDate = movie.ReleaseDate;
        //        //    //findMovie.Genre = movie.Genre;
        //        //    //findMovie.Price = movie.Price;

        //        //    db.Entry(findMovie).CurrentValues.SetValues(movie);

        //        //    db.SaveChanges();
        //        //    result = true;
        //        //}
        //    }
        //    return result;
        //}

        //public bool DeleteSave(Movies movie)
        //{
        //    bool result = false;

        //    try
        //    {
        //        using (TPMVCWeb db = new TPMVCWeb())
        //        {
        //            var findMovie = db.Movies.Find(movie.Id);
        //            if (findMovie != null)
        //            {
        //                db.Movies.Remove(findMovie);
        //                db.SaveChanges();
        //            }
        //        }

        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message.ToString();
        //    }

        //    return result;
        //}

        //public bool CreateSave(Movies movie)
        //{
        //    bool result = false;

        //    try
        //    {
        //        using (TPMVCWeb db = new TPMVCWeb())
        //        {
        //            var findMovie = db.Movies.Find(movie.Id);
        //            if (findMovie == null)
        //            {
        //                db.Movies.Add(movie);
        //                db.SaveChanges();
        //            }
        //        }

        //        result = true;
        //    }
        //    catch (Exception ex)
        //    { }

        //    return result;
        //}
    }
}
