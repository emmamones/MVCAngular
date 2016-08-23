using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public DateTime? ArrivalDate { get; set; }
        public string DirectorName { get; set; }

        [Required]
        public byte InStock { get; set; }
          
        [Required]
        public int GenreId { get; set; }
    }
}