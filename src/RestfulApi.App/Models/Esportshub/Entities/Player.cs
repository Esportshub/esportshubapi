using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RestfulApi.App.Extensions.Entities;
using RestfulApi.App.Models.Esportshub.Builders.PlayerBuilders;
using RestfulApi.App.Models.Esportshub.Entities.Mappings;
using RestfulApi.App.Models.Identity.Entities;

namespace RestfulApi.App.Models.Esportshub.Entities
{
    public class Player : IEsportshubEntity
    {
        public Player() { }

        public static PlayerBuilder Builder()
        {
            return new PlayerBuilder(new Player());
        }

        public int PlayerId { get; private set; }

        public Guid PlayerGuid { get; set; }

        public string Nickname { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string AccountForeignKey { get; set; }

        public List<Player> Followers { get; set; }

        public List<PlayerGames> PlayerGames { get; set; }

        public List<PlayerTeams> PlayerTeams { get; set; }
        public List<Integration> Integrations { get; set; }
        public List<Activity> Activities { get; set; }

        public List<PlayerGroups> PlayerGroups { get; set; }

        [NotMapped]
        int IEsportshubEntity.Id => PlayerId;

        public override bool Equals (object obj)
        {
            Player objPlayer = (Player)obj;

            if (this.CompareEntities(obj))
                return objPlayer.PlayerId == this.PlayerId;

            return false;
        }

        /**@TODO: Test if this hashcode works in hashmap */
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
