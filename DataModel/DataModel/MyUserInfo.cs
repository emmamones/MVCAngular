using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataModel
{
    public class MyUserInfo: CUserEntity
    {  
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string  FullName { get { return FirstName +" " +LastName; } }

        public virtual List<Evento> Eventos { get; set; }
    }
}
