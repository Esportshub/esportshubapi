using System;
using System.ComponentModel.DataAnnotations.Schema;
using Data.App.Extensions.Entities;
using Data.App.Models.Builders.ActivityBuilders;

namespace Data.App.Models.Entities
{
    public class Activity : IEsportshubEntity
    {
        public Activity() { }

        public static ActivityBuilder Builder()
        {
            return new ActivityBuilder(new Activity());
        }

        public int ActivityId { get;  set; }

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