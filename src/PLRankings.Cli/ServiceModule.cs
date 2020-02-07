using System.Net.Http;
using Ninject.Modules;
using PLRankings.Access;
using PLRankings.Manager;
using PLRankings.Resource;

namespace PLRankings.Cli
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRankingManager>().To<RankingManager>();
            Bind<ICompetitionDataAccess>().To<HtmlCompetitionDataAccess>();
            Bind<ICompetitionDatabase>().To<HtmlCompetitionDatabase>();

            Bind<HttpClient>().ToSelf().InSingletonScope();
        }
    }
}