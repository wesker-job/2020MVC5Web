using BusinessLogic.DTO;
using DataAccess;
using DataAccess.DataManager;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class MoviesService
    {
        public List<MovieDto> GetAll()
        {
            List<MovieDto> result = new List<MovieDto>();

            //IMovieRepository imovieRepository = new MovieRepository();
            //List<Movies> movieList = imovieRepository.GetAll();
            MovieDAL manager = new MovieDAL();
            var movieList = manager.GetAll();

            foreach (Movies movies in movieList)
            {
                result.Add(this.MovieDTOTrans(movies));
            }

            return result;
        }

        public MovieDto GetById(int id)
        {
            MovieDto dto = new MovieDto();

            //IMovieRepository imovieRepository = new MovieRepository();
            //var movie = imovieRepository.GetById(id);
            //var movie = moviesRepo.Read(x => x.Id == id);
            MovieDAL manager = new MovieDAL();
            var movie = manager.GetById(id);
            dto = this.MovieDTOTrans(movie);

            return dto;
        }

        public MovieDetailDto GetByIdDetail(int id)
        {
            MovieDetailDto dto = new MovieDetailDto();

            //IMovieRepository imovieRepository = new MovieRepository();
            //var movie = imovieRepository.GetById(id);
            MovieDAL manager = new MovieDAL();
            var movie = manager.GetById(id);
            //dto = this.MovieDTOTrans(movie);

            MovieActorsRepository maRepo = new MovieActorsRepository();
            var getActors = maRepo.GetActorsByMovieId(id);
            dto = this.MovieDetailDtoTrans(movie, getActors);

            return dto;
        }


        public bool EditSave(MovieDto dto)
        {
            bool saveResult = false;

            //IMovieRepository imovieRepository = new MovieRepository();
            //saveResult = imovieRepository.EditSave(this.MovieTrans(dto));
            Movies movies = this.MovieTrans(dto);
            MovieDAL manager = new MovieDAL();
            saveResult = manager.Update(movies);

            return saveResult;
        }

        public bool DeleteSave(MovieDto dto)
        {
            //IMovieRepository imovieRepository = new MovieRepository();
            //saveResult = imovieRepository.DeleteSave(this.MovieTrans(dto));
            Movies movies = this.MovieTrans(dto);
            MovieDAL manager = new MovieDAL();
            bool saveResult = manager.Delete(movies);

            return saveResult;
        }

        public bool CreateSave(MovieDto dto)
        {
            //IMovieRepository imovieRepository = new MovieRepository();
            //saveResult = imovieRepository.CreateSave(this.MovieTrans(dto));
            Movies movies = this.MovieTrans(dto);
            MovieDAL manager = new MovieDAL();
            bool saveResult = manager.Add(movies);

            return saveResult;
        }
        private MovieDetailDto MovieDetailDtoTrans(Movies movies, List<Actors> actors)
        {
            MovieDetailDto dto = new MovieDetailDto
            {
                Id = movies.Id,
                Title = movies.Title,
                ReleaseDate = movies.ReleaseDate,
                Genre = movies.Genre,
                Price = movies.Price ?? 0,

                ActorList = new List<ActorDto>()
            };

            foreach (var item in actors)
            {
                dto.ActorList.Add(new ActorDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Intro = item.Intro
                });
            }

            return dto;
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
