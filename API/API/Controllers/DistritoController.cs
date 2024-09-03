using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class DistritoController : ControllerBase
    {
        public DistritoController() { }

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