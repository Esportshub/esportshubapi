using System;
using Patterns.Builder;

namespace EsportshubApi.Models.Entities 
{
    public interface IActivityBuilder : IBuilder<Activity, ActivityValidator>
    {
         IActivityBuilder SetActivityId(int input);
         IActivityBuilder SetActivityGuid(Guid input);
         IActivityBuilder SetTitle(string input);
         IActivityBuilder SetDescription(string input);
         IActivityBuilder SetCreated(DateTime input);
         IActivityBuilder SetUpdated(DateTime input);
         IActivityBuilder SetPlayer(Player input);
    }

}