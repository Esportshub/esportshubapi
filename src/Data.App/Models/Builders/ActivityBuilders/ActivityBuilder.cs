using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.ActivityBuilders {
    public class ActivityBuilder : IActivityBuilder
    {
        private readonly Activity _activity;

        public ActivityBuilder(Activity activity)
        {
            _activity = activity;
        }

        public Activity Build()
        {
            return _activity;
        }

        public IActivityBuilder SetActivityGuid(Guid activityGuid)
        {
            _activity.ActivityGuid = activityGuid;
            return this;
        }

        public IActivityBuilder SetDescription(string description)
        {
            _activity.Description = description;
            return this;
        }

        public IActivityBuilder SetPlayer(Player player)
        {
            _activity.Player = player;
            return this;
        }

        public IActivityBuilder SetTitle(string title)
        {
            _activity.Title = title;
            return this;
        }
    }
}