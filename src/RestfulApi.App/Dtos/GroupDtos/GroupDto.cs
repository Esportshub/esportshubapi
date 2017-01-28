using System;
using System.Collections.Generic;
using RestfulApi.App.Dtos.EventsDtos;
using RestfulApi.App.Dtos.PlayerDtos;

namespace RestfulApi.App.Dtos.GroupDtos
{
    public class GroupDto
    {
        public class RolesDto
        {
            public enum RolesPermission
            {
                Admin,
                Member
            }

            public int RolesId { get;set; }

            public RolesPermission Permission { get; set; }

            public Guid RolesGuid { get; set; }

            public  DateTime Created { get; set; }

            public DateTime Updated { get; set; }
        }

        public enum Visibilties
        {
            Public,
            Private
        }



        public int GroupId { get; set; }

        public Guid GroupGuid { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public RolesDto Role { get; set;  }

        public Visibilties Visibilty { get; set; }

        public List<PlayerGroupsDto> PlayerGroups { get; set; }

        public List<EsportshubEventDto> EsportshubEvents { get; set; }

    }
}