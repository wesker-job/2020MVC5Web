using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MovieActorsRepository
    {
        public MovieActorsRepository() { }

        public List<Actors> GetActorsByMovieId(int movieid)
        {
            List<Actors> actorList = new List<Actors>();

            List<string> runTime = new List<string>();
            using (CodeFirstEF db = new CodeFirstEF())
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Reset();//碼表歸零
                sw.Start();//碼表開始計時
                var result = db.MovieActors.Where(o => o.MovieId == movieid)
                    .Select(c => c.ActorId);

                var actors = db.Actors.Where(o => result.Contains(o.Id)).ToList();
                sw.Stop();//碼錶停止
                runTime.Add($"第一種:{sw.Elapsed.TotalMilliseconds.ToString()}");

                //var result = db.Actors
                //    .Where(c =>
                //    db.MovieActors.Where(o => o.MovieId == movieid).Select(x => x.ActorId)
                //    .ToList().Contains(c.Id));

                sw.Reset();//碼表歸零
                sw.Start();//碼表開始計時
                var actor = db.MovieActors
                    .Where(c => c.MovieId == movieid)
                    .Join(db.Actors, c => c.ActorId, o => o.Id,
                    (c, o) => new { NewID = o.Id, NewName = o.Name, NewIntro = o.Intro })
                    .OrderBy(co => co.NewID).ToList();
                sw.Stop();//碼錶停止
                runTime.Add($"第二種:{sw.Elapsed.TotalMilliseconds}");

                sw.Reset();//碼表歸零
                sw.Start();//碼表開始計時
                var getActorList = db.Actors.Join(db.MovieActors, a => a.Id, ma => ma.ActorId,
                    (a, ma) => new { NewMovieId = ma.MovieId, NewID = a.Id, NewName = a.Name, NewIntro = a.Intro })
                    .Where( c => c.NewMovieId == movieid).ToList();
                runTime.Add($"第三種:{sw.Elapsed.TotalMilliseconds}");

                foreach(var item in getActorList)
                {
                    actorList.Add(new Actors()
                    {
                        Id = item.NewID,
                        Name = item.NewName,
                        Intro = item.NewIntro
                    });
                }
            }

            return actorList;
        }
    }
}
