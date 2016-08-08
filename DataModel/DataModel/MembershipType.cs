namespace Persistance.DataModel
{
    public class MembershipType:CUserEntity
    {
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

    }
}