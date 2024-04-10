namespace Mango.Services.CouponAPI.Models.Dto
{
    public class UpdateCouponDto
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
