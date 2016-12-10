using System;
using RestfulApi.App.Dtos.IntegrationsDtos;

namespace RestfulApi.App.Dtos.SocialMediaDtos
{
    public class SocialMediaDto
    {
        public int SocialMediaDtoId { get; set; }

        public Guid SocialMediaDtoGuid { get; set; }

        public IntegrationDto IntegrationDto { get; set; }

        public string Name { get; set; }
    }
}