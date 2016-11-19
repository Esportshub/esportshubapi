using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EsportshubApi.Models.Entities;

namespace EsportshubApi.Models.Entities
{
    public class AccountValidator : IApplicationUserValidator
    {
        public void Validate(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}