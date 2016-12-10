using System;
using RestfulApi.App.Dtos.IntegrationsDtos;

namespace RestfulApi.App.Dtos.SocialMediaDtos
{
    public class SocialMediaDto
    {
        public int SocialMediaId { get; set; }

        public Guid SocialMediaGuid { get; set; }

        public string Name { get; set; }

        public IntegrationDto Integration { get; set; }
    }
}