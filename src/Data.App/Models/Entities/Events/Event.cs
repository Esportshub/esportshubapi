using System;
using System.ComponentModel.DataAnnotations.Schema;
using Data.App.Extensions.Entities;

namespace Data.App.Models.Entities.Events
{
    public abstract class Event : IEsportshubEntity
    {

        public int EventId { get; set; }
        public string Name { get; set; }
        public Guid EventGuid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        [NotMapped]
        public int Id => EventId;

        public override bool Equals(object obj)
        {
            Event @event = (Event) obj;

            if (this.CompareEntities(obj))
                return @event.EventId == this.EventId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}