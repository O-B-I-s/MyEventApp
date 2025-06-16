using FluentNHibernate.Mapping;
using MyEventApp.Core.Models;
using NHibernate.Type;

namespace MyEventApp.Data.Mappings
{
    /// <summary>
    /// NHibernate mapping configuration for the Event entity.
    /// </summary>
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Table("Events");
            Id(x => x.Id)
           .Column("Id")
           .CustomType<GuidType>()
           .CustomSqlType("TEXT")
           .GeneratedBy.Assigned();
            Map(x => x.Name).Column("Name").Not.Nullable();
            Map(x => x.StartsOn).Column("StartsOn").Not.Nullable();
            Map(x => x.EndsOn).Column("EndsOn").Not.Nullable();
            Map(x => x.Location).Column("Location").Not.Nullable();
        }
    }
}
