using System.Net;
using Hikkaba.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hikkaba.Web.Controllers.Mvc
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            _logger.LogInformation($"Unexpected Status Code: {statusCode}, OriginalPath: {reExecute.OriginalPath}");
            return View(statusCode);
        }

        [Route("Error/Details")]
        public IActionResult Details()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;
            return View(exception);
        }

        [Route("Error/Details/{message}")]
        public IActionResult Details(string message)
        {
            var exception = new HttpResponseException(HttpStatusCode.BadRequest, message);
            return View(exception);
        }
    }
}