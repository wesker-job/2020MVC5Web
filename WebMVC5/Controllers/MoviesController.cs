using BusinessLogic;
using BusinessLogic.DTO;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Mvc;
using WebMVC5.ViewModel;

namespace WebMVC5.Controllers
{
    public class MoviesController : Controller
    {
        //private readonly MovieRepository _repository;

        public MoviesController()
        {
            //_repository = new MovieRepository();
        }

        // GET: Movies
        public ActionResult Index()
        {
            MoviesService service = new MoviesService();
            var result = service.GetAll();
            //List<Movies> moviesEntity = _repository.GetAll();
            //TPWebEntities dbContext = new TPWebEntities();
            //List<Movies> moviesEntity = dbContext.Movies.ToList();
            List<MovieViewModel> vm = this.MovieListVMTrans(result);
            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            MoviesService service = new MoviesService();
            var item = service.GetById(id);
            MovieViewModel vm = this.MovieVMTrans(item);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                MoviesService service = new MoviesService();
                service.EditSave(this.MovieDtoTrans(model));
                return RedirectToAction("Index");
            }
            else
            {
                Dictionary<string, string[]> modelStateErrors = ModelState.
                    Where(c => c.Value.Errors.Count > 0).
                    ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).
                    ToArray());

                ViewData["errors"] = modelStateErrors;
                return View(model);
            }

            
        }

        public ActionResult Delete(int id)
        {
            MoviesService service = new MoviesService();
            var item = service.GetById(id);
            MovieViewModel vm = this.MovieVMTrans(item);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Delete(MovieViewModel model)
        {
            MoviesService service = new MoviesService();
            service.DeleteSave(this.MovieDtoTrans(model));
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            MovieViewModel vm = new MovieViewModel()
            {
                ReleaseDate = new System.DateTime(2020, 01, 05),
                Price = 250
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(MovieViewModel model)
        {
            MoviesService service = new MoviesService();
            service.CreateSave(this.MovieDtoTrans(model));
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            MoviesService service = new MoviesService();
            MovieDto query = service.GetById(id);
            MovieViewModel vm = new MovieViewModel();
            vm = this.MovieVMTrans(query);

            return View(vm);
        }

        private List<MovieViewModel> MovieListVMTrans(List<MovieDto> movies)
        {
            List<MovieViewModel> movieVM = new List<MovieViewModel>();

            foreach (MovieDto movie in movies)
            {
                movieVM.Add(this.MovieVMTrans(movie));
            }

            return movieVM;
        }

        private MovieViewModel MovieVMTrans(MovieDto movie)
        {
            MovieViewModel movieVM = new MovieViewModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price
            };

            return movieVM;
        }

        private MovieDto MovieDtoTrans(MovieViewModel movie)
        {
            MovieDto movieVM = new MovieDto()
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price
            };

            return movieVM;
        }
    }
}