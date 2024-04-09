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
        public CouponApiController(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDto();  
            
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var coupons = await _db.Coupons.ToListAsync();
                _response.Result = coupons;
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
                _response.Result = coupons;
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
    }
}
