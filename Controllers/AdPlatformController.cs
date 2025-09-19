using Microsoft.AspNetCore.Mvc;
using AdvertisingTask.Services;

namespace AdvertisingTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdPlatformController : ControllerBase
    {
        private readonly AdPlatformStorage _storage;

        public AdPlatformController(AdPlatformStorage storage)
        {
            _storage = storage;
        }

        [HttpPost("load-text")]
        public IActionResult LoadFromText([FromBody] List<string> lines)
        {
            if (lines == null || lines.Count == 0)
                return BadRequest("Список строк пуст или введен с ошибками, попробуйте ввести корректное значение, например: \"Яндекс.Директ:/ru\".");

            _storage.LoadFromLines(lines);
            return Ok("Данные загружены.");
        }



        // Метод поиска по локации
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("Локация не указана.");

            var result = _storage.Search(location.Trim());
            return Ok(result);
        }
    }
}
