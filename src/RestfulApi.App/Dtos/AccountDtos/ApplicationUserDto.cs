using System;
using RestfulApi.App.Dtos.PlayerDtos;

namespace RestfulApi.App.Dtos.AccountDtos
{
    public class ApplicationUserDto
    {
  public string ApplicationUserId { get;  set; }

        public Guid ApplicationUserGuid { get;  set; }

        public string Salt { get; set; }

        public PlayerDto Player { get; set; }

        public bool Verified { get; set; }

        public string Checksum { get; set; }

        public string Password { get; set; }

        public DateTime Created { get;  set; }

        public DateTime Updated { get;  set; }    }
}