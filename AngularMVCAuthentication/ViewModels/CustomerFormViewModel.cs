using AngularMVCAuthentication.DataModel;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.ViewModels
{
    public class CustomerFromViewModel
    {
        public int Id { get; set; }

        [Required( ErrorMessage ="Please enter custmer's name")]
        [StringLength(255)]
        public string Name { get; set; }


        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        [Display(Name = "Membership Type")]
        public int MembershipTypeId { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }

    }
}