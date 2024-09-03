using Business.Services.Interfaces;
using DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class OcorrenciaController : ControllerBase
    {
        private readonly IOcorrenciaService _correnciaService;

        public OcorrenciaController(IOcorrenciaService correnciaService)
        {
            _correnciaService = correnciaService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _correnciaService.GetAll());
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Salvar([FromBody]OcorrenciaViewModel model)
        {
            try
            {
                await _correnciaService.Salvar(model);

                return Ok();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
