using BusinessLogic.DTO;
using DataAccess;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class MoviesService
    {
        public List<MovieDto> GetAll()
        {
            List<MovieDto> result = new List<MovieDto>();

            IMovieRepository imovieRepository = new MovieRepository();
            List<Movies> movieList = imovieRepository.GetAll();

            foreach (Movies movies in movieList)
            {
                result.Add(this.MovieDTOTrans(movies));
            }

            MovieActorsRepository maRepo = new MovieActorsRepository();
            string tmp = maRepo.GetActorsByMovieId(6);

            return result;
        }

        public MovieDto GetById(int id)
        {
            MovieDto dto = new MovieDto();

            IMovieRepository imovieRepository = new MovieRepository();
            var movie = imovieRepository.GetById(id);
            dto = this.MovieDTOTrans(movie);

            return dto;
        }

        public bool EditSave(MovieDto dto)
        {
            bool saveResult = false;

            IMovieRepository imovieRepository = new MovieRepository();
            saveResult = imovieRepository.EditSave(this.MovieTrans(dto));

            return saveResult;
        }

        public bool DeleteSave(MovieDto dto)
        {
            bool saveResult = false;

            IMovieRepository imovieRepository = new MovieRepository();
            saveResult = imovieRepository.DeleteSave(this.MovieTrans(dto));

            return saveResult;
        }

        public bool CreateSave(MovieDto dto)
        {
            bool saveResult = false;

            IMovieRepository imovieRepository = new MovieRepository();
            saveResult = imovieRepository.CreateSave(this.MovieTrans(dto));

            return saveResult;
        }

        private MovieDto MovieDTOTrans(Movies movies)
        {
            return new MovieDto()
            {
                Id = movies.Id,
                Title = movies.Title,
                ReleaseDate = movies.ReleaseDate,
                Genre = movies.Genre,
                Price = movies.Price.HasValue ? movies.Price.Value : 0
            };
        }

        private Movies MovieTrans(MovieDto movies)
        {
            return new Movies()
            {
                Id = movies.Id,
                Title = movies.Title,
                ReleaseDate = movies.ReleaseDate,
                Genre = movies.Genre,
                Price = movies.Price
            };
        }
    }
}
