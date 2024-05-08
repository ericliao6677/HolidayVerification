using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Holiday.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _service;
        public HolidayController(IHolidayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryHolidayRequest request)
        {
            var result = await _service.GetAsync(request);
            return Ok(result);
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetByDate([FromRoute] DateTime date)
        {
            var result = await _service.GetByDateAsync(date);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PostHolidayRequest request)
        {
            var result = await _service.InsertAsync(request);
            return Ok(result);
        }
        


    }
}
