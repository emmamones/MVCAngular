using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AngularMVCAuthentication.DataModel;

namespace AngularMVCAuthentication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    { 
        public string HomeTown { get; set; }
        public System.DateTime?  BirthDate { get; set; }
         
        public virtual ICollection<Evento> Eventos { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    
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
                return ApplicationUser.UserName;
            }
        }

        public string Recommendation { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
     
}