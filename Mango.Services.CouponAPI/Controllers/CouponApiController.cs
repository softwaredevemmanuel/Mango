using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto  _response;
        private IMapper _mapper;
        public CouponApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();  
            
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var coupons = await _db.Coupons.ToListAsync();
                _response.Result = _mapper.Map<List <CouponDto>> (coupons);
                _response.Message = "Retrieved Successfully";
                return Ok(_response);


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);

            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var coupons = await _db.Coupons.FirstAsync(u=>u.CouponId==id);
                
                _response.Result = _mapper.Map <CouponDto> (coupons);
                _response.Message = "Retrieved Successfully";


                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpGet("GetByCode/{code}")]

        public async Task<IActionResult> Get(string code)
        {
            try
            {
                var coupons = await _db.Coupons.FirstAsync(u => u.CouponCode == code);

                _response.Result = _mapper.Map<CouponDto>(coupons);
                _response.Message = "Retrieved Successfully";


                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(obj);
                await _db.SaveChangesAsync();

                var mappedCouponDto = _mapper.Map<CouponDto>(obj);

                var responseDto = new ResponseDto
                {
                    IsSuccess = true,
                    Message = "Coupon created successfully",
                    Result = mappedCouponDto
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                var responseDto = new ResponseDto
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}"
                };

                return StatusCode(500, responseDto);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] UpdateCouponDto couponDto)
        {
            try
            {
                // Find the coupon by its ID
                Coupon existingCoupon = await _db.Coupons.FindAsync(id);

                // If the coupon doesn't exist, return a 404 Not Found response
                if (existingCoupon == null)
                {
                    return NotFound("Coupon not found");
                }

                // Map properties from UpdateCouponDto to the existingCoupon object
                _mapper.Map(couponDto, existingCoupon);

                // Update the existing coupon in the DbSet
                _db.Coupons.Update(existingCoupon);

                // Save changes asynchronously
                await _db.SaveChangesAsync();

                // Map the updated coupon back to UpdateCouponDto
                var mappedCouponDto = _mapper.Map<UpdateCouponDto>(existingCoupon);

                var responseDto = new ResponseDto
                {
                    IsSuccess = true,
                    Message = "Coupon updated successfully",
                    Result = mappedCouponDto
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                var responseDto = new ResponseDto
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}"
                };

                return StatusCode(500, responseDto);
            }
        }


        [HttpDelete]
        public ResponseDto DeleteCoupon(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();
            
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
                

            }
            return _response;
        }

    }
}
