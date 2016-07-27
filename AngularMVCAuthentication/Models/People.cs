using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.Models
{
    public class People
    {
        public int PeopleId { get; set; }
        public string FirstName { get; set; }
        
        public bool Retired { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}