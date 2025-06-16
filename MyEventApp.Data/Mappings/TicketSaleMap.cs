using FluentNHibernate.Mapping;
using MyEventApp.Core.Models;
using NHibernate.Type;

namespace MyEventApp.Data.Mappings
{
    /// <summary>
    /// NHibernate mapping configuration for the TicketSale entity.
    /// </summary>
    public class TicketSaleMap : ClassMap<TicketSale>
    {
        public TicketSaleMap()
        {
            Table("TicketSales");
            Id(x => x.Id)
               .Column("Id")
               .CustomType<GuidType>()
               .CustomSqlType("TEXT")
               .GeneratedBy.Assigned();
            Map(x => x.EventId)
                .Column("EventId")
                .CustomType<GuidType>()
                .CustomSqlType("TEXT")
                .Not.Nullable();

            Map(x => x.UserId)
                .Column("UserId")
                .CustomType<GuidType>()
                .CustomSqlType("TEXT")
                .Not.Nullable();
            Map(x => x.PurchaseDate).Column("PurchaseDate").Not.Nullable();
            Map(x => x.PriceInCents).Column("PriceInCents").Not.Nullable();
        }
    }
}
