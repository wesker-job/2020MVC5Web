using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataManager
{
    public class MovieManager
    {
        private IUnitOfWork _Uok = null;

        public MovieManager()
        { }

        public MovieManager(IUnitOfWork unitOfWork)
        {
            _Uok = unitOfWork;
        }

        public List<Movies> GetAll()
        {
            var result = _Uok.movieRepository.GetAll();

            return result;
        }

        public Movies GetById(int id)
        {
            var result = _Uok.movieRepository.GetById(id);

            return result;
        }

        public bool Update(Movies movies)
        {
            bool result = false;
            try
            {
                _Uok.movieRepository.Update(movies);
                _Uok.Save();

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool Delete(Movies movies)
        {
            bool result = false;
            try
            {
                _Uok.movieRepository.Delete(movies);
                _Uok.Save();

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool Add(Movies movie)
        {
            bool result = false;
            try
            {
                _Uok.movieRepository.Add(movie);
                UokInstance.Save();

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        protected IUnitOfWork UokInstance
        {
            get
            {
                _Uok = _Uok ?? new EFUnitOfWork(new TPMVCWeb());
                return _Uok;
            }
        }

    }
}
