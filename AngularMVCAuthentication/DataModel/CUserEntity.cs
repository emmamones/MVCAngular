using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.DataModel
{
    public abstract class CUserEntity : CModelBase
    {

        [Display(Name = "Created At")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Updated At")]
        public DateTime? Updated { get; set; }
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }


        public IEnumerable<ValidationResult> Validate()
        {
            var validationContext = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, true);
            return validationResults;
        }
    }
}