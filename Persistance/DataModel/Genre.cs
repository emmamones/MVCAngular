using System.ComponentModel.DataAnnotations;

namespace Persistance.DataModel
{
   public class Genre:CUserEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        public string Description { get; set; }

    }
}
