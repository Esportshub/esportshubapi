using System;
using System.ComponentModel.DataAnnotations.Schema;
using RestfulApi.Extensions.Entities;
using RestfulApi.Models.Esportshub.Entities;
using RestfulApi.Models.Esportshub.Entities.Player;

namespace EsportshubApi.Models.Entities
{
    public class Activity : EsportshubEntity
    {
        public Activity() { }

        public static ActivityBuilder Builder()
        {
            return new ActivityBuilder(new Activity());
        }

        public int ActivityId { get; private set; }

        public Guid ActivityGuid { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }

        public Player Player { get; set; }

        [NotMapped]
        public int Id => ActivityId;

        public override bool Equals(object obj)
        {
            Activity activity = (Activity)obj;

            if (this.CompareEntities(obj))
                return activity.ActivityId == this.ActivityId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}