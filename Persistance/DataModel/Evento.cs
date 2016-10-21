
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Persistance.DataModel
{ 
    public class Evento : CUserEntity
    { 
        public string Title { get; set; }
        public System.DateTime? DateEvent { get; set; }
        public string Location { get; set; }
        public string URL { get; set; }

        [NotMapped]
        public string Organizer
        {
            get
            {
                return ApplicationUserName;
            }
        }

        public string Recommendation { get; set; }
        public string ApplicationUserName { get; set; }

    }

}
