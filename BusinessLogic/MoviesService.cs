using BusinessLogic.DTO;
using DataAccess;
using DataAccess.DataManager;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;

namespace BusinessLogic
{
    public class MoviesService
    {
        public List<MovieDto> GetAll()
        {
            List<MovieDto> result = new List<MovieDto>();

            //IMovieRepository imovieRepository = new MovieRepository();
            //List<Movies> movieList = imovieRepository.GetAll();
            MovieManager manager = new MovieManager();
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
            MovieManager manager = new MovieManager();
            var movie = manager.GetById(id);
            dto = this.MovieDTOTrans(movie);

            return dto;
        }

        public MovieDetailDto GetByIdDetail(int id)
        {
            MovieDetailDto dto = new MovieDetailDto();

            //IMovieRepository imovieRepository = new MovieRepository();
            //var movie = imovieRepository.GetById(id);
            MovieManager manager = new MovieManager();
            var movie = manager.GetById(id);
            //dto = this.MovieDTOTrans(movie);

            MovieActorsRepository maRepo = new MovieActorsRepository();
            var getActors = maRepo.GetActorsByMovieId(id);
            dto = this.movieDetailDtoTrans(movie, getActors);

            return dto;
        }


        public bool EditSave(MovieDto dto)
        {
            bool saveResult = false;

            //IMovieRepository imovieRepository = new MovieRepository();
            //saveResult = imovieRepository.EditSave(this.MovieTrans(dto));
            Movies movies = this.MovieTrans(dto);
            MovieManager manager = new MovieManager();
            saveResult = manager.Update(movies);

            return saveResult;
        }

        public bool DeleteSave(MovieDto dto)
        {
            bool saveResult = false;

            //IMovieRepository imovieRepository = new MovieRepository();
            //saveResult = imovieRepository.DeleteSave(this.MovieTrans(dto));
            Movies movies = this.MovieTrans(dto);
            MovieManager manager = new MovieManager();
            saveResult = manager.Delete(movies);

            return saveResult;
        }

        public bool CreateSave(MovieDto dto)
        {
            bool saveResult = false;

            //IMovieRepository imovieRepository = new MovieRepository();
            //saveResult = imovieRepository.CreateSave(this.MovieTrans(dto));
            Movies movies = this.MovieTrans(dto);
            MovieManager manager = new MovieManager();
            saveResult = manager.Add(movies);

            return saveResult;
        }
        private MovieDetailDto movieDetailDtoTrans(Movies movies, List<Actors> actors)
        {
            MovieDetailDto dto = new MovieDetailDto();
            dto.Id = movies.Id;
            dto.Title = movies.Title;
            dto.ReleaseDate = movies.ReleaseDate;
            dto.Genre = movies.Genre;
            dto.Price = movies.Price ?? 0;

            dto.ActorList = new List<ActorDto>();
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
