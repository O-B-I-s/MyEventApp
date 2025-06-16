using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using MyEventApp.Data.Mappings;
using NHibernate;

namespace MyEventApp.Data
{
    public class NHibernateHelper
    {
        private readonly IConfiguration _config;
        private static ISessionFactory _sf;
        public NHibernateHelper(IConfiguration config)
        {
            _config = config;
        }


        public ISessionFactory CreateSessionFactory()
        {
            if (_sf != null) return _sf;
            var connectionString = _config.GetConnectionString("SQLite");



            //Configure Fluent NHibernate
            try
            {
                _sf = Fluently.Configure()
                   .Database(
                       SQLiteConfiguration.Standard
                           .ConnectionString(connectionString)
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
