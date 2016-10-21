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
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name="Genre")]
        [Required]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }


        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public DateTime? ArrivalDate { get; set; }

        public string DirectorName { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        public byte NumberInStock { get; set; }

        [Display(Name = "Number in Stock")]
        public byte NumberAvailable { get; set; }

    }
}
