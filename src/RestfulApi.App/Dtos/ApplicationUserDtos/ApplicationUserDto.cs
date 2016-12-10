using System;
using RestfulApi.App.Dtos.PlayerDtos;

namespace RestfulApi.App.Dtos.ApplicationUserDtos
{
    public class ApplicationUserDto
    {
        public string ApplicationDtoUserId { get; set; }

        public Guid ApplicationUserDtoGuid { get; set; }

        public string Salt { get; set; }

        public PlayerDto PlayerDto { get; set; }

        public bool Verified { get; set; }

        public string Checksum { get; set; }

        public string Password { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}