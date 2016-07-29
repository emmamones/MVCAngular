using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{

    public class EventoExterno : CModelBase
    {
        public int EventoId { get; set; }
        public string Title { get; set; }
        public System.DateTime? Date { get; set; }

        public string Location { get; set; }
        public string URL { get; set; }

        [NotMapped]
        public string Organizer
        {
            get
            {
                return ApplicationUser.FullName;
            }
        }

        public string Recommendation { get; set; }
        public virtual MyUserInfo ApplicationUser { get; set; }
    }

}
