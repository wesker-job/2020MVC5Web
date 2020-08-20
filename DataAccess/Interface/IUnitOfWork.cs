using System;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        IRepository<T> Repository<T>() where T : class;

        public IMovieRepository movieRepository { get; }
    }
}
