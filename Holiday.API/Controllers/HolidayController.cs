using Holiday.API.Domain.Request.Get;
using Holiday.API.Domain.Request.Post;
using Holiday.API.Domain.Request.Put;
using Holiday.API.Domain.Response;
using Holiday.API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResultResponse<QueryHolidayResponse>>))]
        public async Task<IActionResult> Get([FromQuery] QueryHolidayRequest request)
        {
            var result = await _service.GetAsync(request);
            return Ok(result);
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse<QueryHolidayResponse>))]
        public async Task<IActionResult> GetByDate([FromQuery, Required(ErrorMessage = "日期欄位必填")] DateTime date)
        {
            var result = await _service.GetByDateAsync(date);
            return Ok(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse<QueryHolidayResponse>))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse))]
        public async Task<IActionResult> Insert([FromBody] PostHolidayRequest request)
        {
            var result = await _service.InsertAsync(request);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeletebyIdAsync(id);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse))]
        public async Task<IActionResult> Put([FromBody] PutHolidayRequest request)
        {
            var result = await _service.UpdateAsync(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultResponse))]
        public async Task<IActionResult> InsertDataFromCsvFile(IFormFile file)
        {
            var result = await _service.ParseCsvFile(file);
            return Ok(result);
        }


    }
}
