using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataModel
{
   public class Customer:CUserEntity
    {
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        public virtual MembershipType TypeOfSubscription { get; set; }
        public int MembershipTypeId { get; set; }
    }
}
