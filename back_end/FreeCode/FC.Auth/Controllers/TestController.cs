using System.Web.Http;

namespace FC.Auth.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Authorize]
        [Route("result")]
        [HttpGet]
        public IHttpActionResult Test()
        {
            return Ok("Test Api is success");
        }
    }
}
