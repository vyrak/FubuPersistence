using Raven.Client;
using StructureMap.Configuration.DSL;

namespace FubuPersistence.RavenDb
{
    public class RavenDbRegistry : Registry
    {
        public RavenDbRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<IDocumentStoreConfigurationAction>();
                x.AddAllTypesOf<IDocumentStoreConfigurationAction>();
            });

            ForSingletonOf<IDocumentStore>().Use(c => c.GetInstance<IDocumentStoreBuilder>().Build());

            For<IDocumentSession>().Use(c => c.GetInstance<ISessionBoundary>().Session());

            For<ISessionBoundary>().Use<SessionBoundary>();

            For<IPersistor>().Use<RavenPersistor>();

            For<ITransaction>().Use<RavenTransaction>();
        }
    }
}