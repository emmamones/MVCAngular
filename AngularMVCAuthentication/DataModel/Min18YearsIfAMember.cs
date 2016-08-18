using AngularMVCAuthentication.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMVCAuthentication.DataModel
{
   public class Min18YearsIfAMember:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           var customer=(CustomerFromViewModel) validationContext.ObjectInstance;

            if (customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1)
                return ValidationResult.Success;
            else
            {
                if (customer.BirthDate==null) 
                    return new ValidationResult("BirthDate is required");
              

                if ((DateTime.Today.Year - customer.BirthDate.Value.Year) < 18)
                    return new ValidationResult("User must be 18 years Old");

                return ValidationResult.Success;
            }

        }
    }
}
