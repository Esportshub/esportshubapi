using System;
using System.ComponentModel.DataAnnotations;

namespace RestfulApi.App.ViewModels
{
    public class ActivityViewModel
    {
        public int  AcivityId { get; set; }

        public Guid ActivityGuid { get; set; }

        [Required(ErrorMessage = "Title Is Required")]
        [RegularExpression("^.{5,20}[a-z][A-Z]$",
        ErrorMessage = "Title must be between 5-20 character, and must contain a letter")]
        public string Title { get; set; }

    }
}