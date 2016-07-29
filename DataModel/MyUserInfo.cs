using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class MyUserInfo:CModelBase
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string  FullName { get { return FirstName +" " +LastName; } }

        public virtual List<EventoExterno> Eventos { get; set; }
    }
}
