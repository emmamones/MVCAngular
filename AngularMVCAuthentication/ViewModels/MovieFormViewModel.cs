using AngularMVCAuthentication.DataModel;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.ViewModels
{
    public class MovieFormViewModel
    {
        public int Id { get; set; }

        [Required( ErrorMessage ="Please enter movie's name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of arrival")] 
        public DateTime? ArrivalDate { get; set; }

        [Required]
        [Display(Name = "Date of release")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public string DirectorName { get; set; }

        [Required]
        public byte InStock { get; set; }

        [Required]
        [Display(Name = "Genre Type")]
        public int GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

    }
}