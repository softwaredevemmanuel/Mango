using Mango.Web.Models.Dto;
using Mango.Web.Service.IService;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
            
        }

        public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Apitype.POST,
                Data = couponDto,
                Url = CouponAPIBase + "api/coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Apitype.DELETE,
                Url = CouponAPIBase + "api/coupon" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Apitype.GET,
                Url = CouponAPIBase + "api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Apitype.GET,
                Url = CouponAPIBase + "api/GetByCode" + couponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Apitype.GET,
                Url = CouponAPIBase + "api/coupon" + id
            });
        }

        public async Task<ResponseDto?> UpdateCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Apitype.PUT,
                Data = id,
                Url = CouponAPIBase + "api/coupon" + id
            });
        }
    }
}
