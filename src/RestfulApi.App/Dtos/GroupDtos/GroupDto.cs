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

            public int RolesDtoId { get;set; }

            public RolesPermission Permission { get; set; }

            public Guid RolesDtoGuid { get; set; }

            public  DateTime Created { get; set; }

            public DateTime Updated { get; set; }
        }

        public enum Visibilties
        {
            Public,
            Private
        }



        public int GroupDtoId { get; set; }

        public Guid GroupDtoGuid { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public RolesDto RoleDto { get; set;  }

        public Visibilties Visibilty { get; set; }

        public List<PlayerGroupsDto> PlayerGroupsDtos { get; set; }

        public List<GroupEventDto> GroupEventsDtos { get; set; }

    }
}