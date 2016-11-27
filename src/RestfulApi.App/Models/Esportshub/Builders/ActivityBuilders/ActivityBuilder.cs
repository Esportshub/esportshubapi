using System;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Esportshub.Builders.ActivityBuilders {
    public class ActivityBuilder : IActivityBuilder
    {
        private Activity _activity;
        public ActivityBuilder(Activity activity)
        {
            _activity = activity;
        }

        public Activity Build()
        {
            return _activity;
        }

        public IActivityBuilder SetActivityGuid(Guid input)
        {
            _activity.ActivityGuid = input;
            return this;
        }

        public IActivityBuilder SetDescription(string input)
        {
            _activity.Description = input;
            return this;
        }

        public IActivityBuilder SetPlayer(Player input)
        {
            _activity.Player = input;
            return this;
        }

        public IActivityBuilder SetTitle(string input)
        {
            _activity.Title = input;
            return this;
        }
    }
}