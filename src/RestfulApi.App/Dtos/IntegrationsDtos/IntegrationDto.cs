using System;
using RestfulApi.App.Dtos.SocialMediaDtos;

namespace RestfulApi.App.Dtos.IntegrationsDtos
{
    public class IntegrationDto
    {
        public Guid IntegrationGuid { get; set; }
        public SocialMediaDto SocialMedia { get; set; }

    }
}