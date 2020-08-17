using System.Collections.Generic;

namespace DataAccess
{
    public interface IMovieRepository
    {
        List<Movies> GetAll();

        Movies GetById(int id);

        bool EditSave(Movies movie);

        bool CreateSave(Movies movie);

        bool DeleteSave(Movies movie);
    }
}