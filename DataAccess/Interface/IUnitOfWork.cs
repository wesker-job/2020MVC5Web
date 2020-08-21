using System;
using System.Data.Entity;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        void Save();

        IRepository<T> Repository<T>() where T : class;

        IMovieRepository MovieRepository { get; }
        //IActorRepository ActorRepository { get; }
    }
}
