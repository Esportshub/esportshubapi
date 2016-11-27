using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EsportshubApi.Models.Entities;
using RestfulApi.Extensions.Entities;
using RestfulApi.Models.Esportshub.Entities.Group.Builder;
using RestfulApi.Models.Esportshub.Entities.GroupPlayer;
using RestfulApi.Models.Esportshub.Entities.mappings;

namespace RestfulApi.Models.Esportshub.Entities.Group
{
    public class Group : EsportshubEntity
    {

        private Group() { }

        public static GroupBuilder Build()
        {
            return new GroupBuilder(new Group());
        }

        [NotMapped]
        public int Id => GroupId;

        public int GroupId { get; private set; }

        public Guid GroupGuid { get; set; }

        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }

        public Role Role { get; set;  }

        public Visibilty Visibilty { get; set; }

        public List<PlayerGroups> PlayerGroups { get; set; }

        public List<GroupEvent> GroupEvents { get; set; }

        public override bool Equals(object obj)
        {
            Group group = (Group)obj;

            if (this.CompareEntities(obj))
                return group.GroupId == this.GroupId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}