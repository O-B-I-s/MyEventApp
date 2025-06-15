using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MyEventApp.Data.Mappings;
using NHibernate;

namespace MyEventApp.Data
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sf;
        public static ISessionFactory CreateSessionFactory()
        {
            if (_sf != null) return _sf;

            // Build absolute path to the DB file
            var baseDir = AppContext.BaseDirectory;
            var dbPath = Path.GetFullPath(Path.Combine(baseDir,
                              "..", "..", "..", "Db", "skillsAssessmentEvents.db"));
            Console.WriteLine("Using database at: " + dbPath);
            var connString = $"Data Source={dbPath};Version=3;";

            //Configure Fluent NHibernate
            try
            {
                _sf = Fluently.Configure()
                   .Database(
                       SQLiteConfiguration.Standard
                           .ConnectionString(connString)
                           .ShowSql()
                   )
                   .Mappings(m => m.FluentMappings
                       .AddFromAssemblyOf<EventMap>()
                       .AddFromAssemblyOf<TicketSaleMap>()
                   )
                   .BuildSessionFactory();

                return _sf;
            }
            catch (FluentConfigurationException ex)
            {
                Console.Error.WriteLine("NHibernate failed to configure:");
                foreach (var r in ex.PotentialReasons)
                    Console.Error.WriteLine(" - " + r);
                Console.Error.WriteLine("Inner exception: " + ex.InnerException);
                throw;
            }
        }
    }
}
