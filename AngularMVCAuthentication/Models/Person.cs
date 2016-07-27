using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        
        public bool Retired { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}