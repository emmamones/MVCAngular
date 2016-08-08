using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataModel
{
   public class Movie : CUserEntity
    { 
        public string Name { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public string DirectorName { get; set; }
    }
}
