using System.Collections.Generic;

namespace DataAccess
{
    public interface IMovieRepository
    {
        List<Movies> GetAll();

        Movies GetById(int id);

        void Update(Movies movie);

        void Delete(Movies movie);

        void Add(Movies movie);

        //bool EditSave(Movies movie);

        //bool CreateSave(Movies movie);

        //bool DeleteSave(Movies movie);
    }
}