using System.ComponentModel.DataAnnotations;

namespace Persistance.DataModel
{
    public class MembershipType:CUserEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}