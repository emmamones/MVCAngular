using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataModel
{
   public class Movie : CUserEntity
    { 
        public string Name { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        public DateTime? ArrivalDate { get; set; }
        public string DirectorName { get; set; }

        [Required]
        public byte InStock { get; set; }

        public virtual Genre Genre { get; set; }

        public int GenreId { get; set; }
    }
}
