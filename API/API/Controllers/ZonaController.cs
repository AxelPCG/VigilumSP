using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ZonaController : ControllerBase
    {
        public ZonaController() { }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}