using System;
using System.Collections.Generic;
using RestfulApi.App.Dtos.EventsDtos;
using RestfulApi.App.Dtos.PlayerDtos;
using RestfulApi.App.Dtos.TeamDtos;

namespace RestfulApi.App.Dtos.GameDtos
{
    public class GameDto
    {
        public int GameId { get;  set; }

        public string Name { get; set; }

        public Guid GameGuid { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public List<TeamDto> Teams { get; set; }

        public List<GameEventDto> GameEvents { get; set; }

        public List<PlayerGamesDto> PlayerGames { get; set; }


    }
}