using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.Dtos
{
    public class NewRentalDto
    {
        public string CustomerId { get; set; }
        public int [] MovieIds { get; set; }
        public DateTime? ReturnDate { get; set; }

        public IEnumerable<MovieDto> Movies { get; set; }

    }
}