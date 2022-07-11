using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application;
using System.Threading.Tasks;

namespace Rbac.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        [HttpGet]
        public async Task<FileContentResult> CaptchaAsync([FromServices] ICodeService _captcha)
        {
            var code = await _captcha.GenerateRandomCaptchaAsync();
            var result = await _captcha.GenerateCaptchaImageAsync(code);
            return File(result.CaptchaMemoryStream.ToArray(), "image/png");
        }

    }
}
