using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataModel
{
   public class Rental : CUserEntity
    {  
        [Required]
        public Customer Customer { get; set; } 

        [Required]
        public Movie Movie { get; set; }
        public DateTime? RentDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        public string TimeStamp { get; set; }
    }
}
