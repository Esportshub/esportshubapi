using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.ActivityBuilders
{
    public interface IActivityBuilder : IBuilder<Activity>
    {
         IActivityBuilder SetActivityGuid(Guid input);
         IActivityBuilder SetTitle(string input);
         IActivityBuilder SetDescription(string input);
         IActivityBuilder SetPlayer(Player input);
    }

}