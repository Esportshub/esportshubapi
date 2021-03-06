﻿using System;
using RestfulApi.App.Dtos.PlayerDtos;

namespace RestfulApi.App.Dtos.ActivitiesDtos
{
    public class ActivityDto
    {
        public Guid ActivityGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PlayerDto Player { get; set; }

    }
}