using System;
using EsportshubApi.Extensions;

namespace EsportshubApi.Models.Entities
{
    public class Activity
    {
        public Activity() { }

        public static ActivityBuilder Builder()
        {
            return new ActivityBuilder(new Activity());
        }

        public int ActivityId { get; set; }
        public Guid ActivityGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Player Player { get; set; }

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