using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Data.App.Extensions.Entities;
using Data.App.Models.Builders.GameBuilders;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Entities
{
    public class Game : IEsportshubEntity
    {
        private Game() { }

        public static GameBuilder Builder()
        {
            return new GameBuilder(new Game());
        }

        public int GameId { get; set; }

        public string Name { get; set; }

        public Guid GameGuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private  set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }

        public List<Team> Teams { get; set; }

        public List<EsportshubEvent> EsportshubEvents { get; set; }

        public List<PlayerGames> PlayerGames { get; set; }

        [NotMapped]
        public int Id => GameId;

        [NotMapped]
        public Guid Guid => GameGuid;

        public override bool Equals(object obj)
        {
            Game game = (Game)obj;

            if (this.CompareEntities(obj))
                return game.GameId == this.GameId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}