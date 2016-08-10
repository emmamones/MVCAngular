using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataModel
{
   public class Customer:CUserEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        public virtual MembershipType MembershipType { get; set; }
        public int MembershipTypeId { get; set; }
    }
}
