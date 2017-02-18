using System;
using System.ComponentModel.DataAnnotations.Schema;
using Data.App.Extensions.Entities;

namespace Data.App.Models.Entities
{
    public class EsportshubEvent : IEsportshubEntity
    {

        public int EsportshubEventId { get; set; }
        public string Name { get; set; }
        public Guid EsportshubEventGuid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        [NotMapped]
        public int Id => EsportshubEventId;

        [NotMapped]
        public Guid Guid => EsportshubEventGuid;

        public override bool Equals(object obj)
        {
            EsportshubEvent @event = (EsportshubEvent) obj;

            if (this.CompareEntities(obj))
                return @event.EsportshubEventId == this.EsportshubEventId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}