using System;
using System.Collections.Generic;
using Data.App.Extensions.Entities;
using RestfulApi.App.Dtos.ActivitiesDtos;
using RestfulApi.App.Dtos.IntegrationsDtos;

namespace RestfulApi.App.Dtos.PlayerDtos
{
    public sealed class PlayerDto
    {
        public Guid PlayerGuid { get; set; }

        public string Nickname { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string AccountForeignKey { get; set; }

        public List<PlayerDto> Followers { get; set; }

        public List<PlayerGamesDto> PlayerGames { get; set; }

        public List<PlayerTeamsDto> PlayerTeams { get; set; }
        public List<IntegrationDto> Integrations { get; set; }
        public List<ActivityDto> Activities { get; set; }
        public List<PlayerGroupsDto> PlayerGroups { get; set; }

        public override bool Equals (object obj)
        {
            PlayerDto objPlayer = (PlayerDto)obj;

            if (this.CompareEntities(obj))
                return objPlayer.PlayerId == PlayerId;

            return false;
        }

        /**@TODO: Test if this hashcode works in hashmap */
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}