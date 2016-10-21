using AngularMVCAuthentication.DataModel;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.ViewModels
{
    public class RentalFormViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [Required( ErrorMessage ="Please enter Customer name")]
        [StringLength(255)]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Date of Renturn")] 
        public DateTime? ReturnDate { get; set; }

        
        [Display(Name = "Date of Rent")]
        public DateTime? RentDate { get; set; }

        [Required]
        [Display(Name = "Movies")]
        public int[] MoviesId { get; set; }
               
        public IEnumerable<Movie> Movies { get; set; }

    }
}