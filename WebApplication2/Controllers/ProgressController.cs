using Microsoft.AspNetCore.Mvc;
using WebApplication2.modle;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgressController : ControllerBase
    {


        private readonly ILogger<ProgressController> _logger;
        private readonly IMemoryCacheService _cacheService;
        public ProgressController(ILogger<ProgressController> logger, IMemoryCacheService cacheService)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpPost]
        public void UploadProgress([FromBody] Progress progress)
        {

            if (progress == null || progress.Name == null) throw new ArgumentNullException("progress");

            if (_cacheService.DoesNameExist(progress.Name)) throw new ArgumentException("name alredy exists");

            _cacheService.SaveProgress(progress);
        }
        [HttpGet]
        public Progress GetProgress([FromQuery] string name)
        {
            return _cacheService.GetProgressByName(name);
        }
        [HttpPut]
        public void UpdateProgress([FromBody]Progress progress) {
            _cacheService.SaveProgress(progress);
        }
        [HttpDelete("{name}")]
        public void Delete(string name) {
            if(name == null) throw new ArgumentNullException("name");

            if (_cacheService.DoesNameExist(name))
            {
                _cacheService.DeleteValue(name);
            }
            else
            {
                throw new ArgumentException("name does not exist");
            }
        }
    }
}
