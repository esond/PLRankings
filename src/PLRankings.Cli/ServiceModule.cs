using System.Net.Http;
using Ninject.Modules;
using PLRankings.Data;
using PLRankings.Manager;

namespace PLRankings.Cli
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRankingManager>().To<RankingManager>();
            Bind<ICompetitionDataAccess>().To<HtmlCompetitionDataAccess>();

            Bind<HttpClient>().ToSelf().InSingletonScope();
        }
    }
}