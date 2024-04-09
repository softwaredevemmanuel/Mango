using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
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
        public CouponApiController(AppDbContext db)
        {
            _db = db;
            
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var coupons = await _db.Coupons.ToListAsync();

                return Ok(coupons); // Returns a 200 OK response with the list of coupons
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Returns a 500 Internal Server Error response
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var coupons = await _db.Coupons.FirstAsync(u=>u.CouponId==id);
                if (coupons == null)
                {
                    return NotFound(); // Returns a 404 Not Found response if the coupon with the specified id is not found
                }

                return Ok(coupons); // Returns a 200 OK response with the list of coupons
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Returns a 500 Internal Server Error response
            }
        }
    }
}
