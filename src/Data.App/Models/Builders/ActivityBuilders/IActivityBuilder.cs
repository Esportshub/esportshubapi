using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.ActivityBuilders
{
    public interface IActivityBuilder : IBuilder<Activity>
    {
         IActivityBuilder SetActivityGuid(Guid activityGuid);
         IActivityBuilder SetTitle(string title);
         IActivityBuilder SetDescription(string description);
         IActivityBuilder SetPlayer(Player player);
    }

}