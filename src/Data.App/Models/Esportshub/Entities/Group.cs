using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Data.App.Extensions.Entities;
using Data.App.Models.Esportshub.Builders.GroupBuilders;
using Data.App.Models.Esportshub.Entities.Events;
using Data.App.Models.Esportshub.Entities.Mappings;

namespace Data.App.Models.Esportshub.Entities
{
    public class Group : IEsportshubEntity
    {

        public class Roles
        {
            public enum RolesPermission
            {
                Admin,
                Member
            }

            public int RolesId { get; private set; }

            public RolesPermission Permission { get; set; }

            public Guid RolesGuid { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public  DateTime Created { get; private set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime Updated { get; private set; }
        }

        public enum Visibilties
        {
            Public,
            Private
        }

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

        public Roles Role { get; set;  }

        public Visibilties Visibilty { get; set; }

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