using System;
using System.Collections.Generic;

namespace DataAccess.DataManager
{
    public class MovieDAL
    {
        private IUnitOfWork _Uok = null;

        public MovieDAL()
        { }

        public MovieDAL(IUnitOfWork unitOfWork)
        {
            _Uok = unitOfWork;
        }

        public List<Movies> GetAll()
        {
            List<Movies> result = UokInstance.MovieRepository.GetAll();

            return result;
        }

        public Movies GetById(int id)
        {
            Movies result = UokInstance.MovieRepository.GetById(id);

            return result;
        }

        public bool Update(Movies movies)
        {
            bool result = false;
            try
            {
                UokInstance.MovieRepository.Update(movies);
                UokInstance.Save();

                result = true;
            }
            catch (Exception)
            {

            }

            return result;
        }

        public bool Delete(Movies movies)
        {
            bool result = false;
            try
            {
                UokInstance.MovieRepository.Delete(movies);
                UokInstance.Save();

                result = true;
            }
            catch (Exception)
            {

            }

            return result;
        }

        public bool Add(Movies movie)
        {
            bool result = false;
            try
            {
                UokInstance.MovieRepository.Add(movie);
                UokInstance.Save();

                result = true;
            }
            catch (Exception)
            {

            }

            return result;
        }

        protected IUnitOfWork UokInstance
        {
            get
            {
                _Uok ??= new EFUnitOfWork(new TPMVCWeb());
                return _Uok;
            }
        }

    }
}
