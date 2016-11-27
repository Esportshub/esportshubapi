using System;
using RestfulApi.Models.Esportshub.Entities.Player;
using RestfulApi.Patterns.Builder;

namespace EsportshubApi.Models.Entities 
{
    public interface IActivityBuilder : IBuilder<Activity, ActivityValidator>
    {
         IActivityBuilder SetActivityGuid(Guid input);
         IActivityBuilder SetTitle(string input);
         IActivityBuilder SetDescription(string input);
         IActivityBuilder SetPlayer(Player input);
    }

}