using AngularMVCAuthentication.ViewModels;
using Persistance.DataModel;
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

            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
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
