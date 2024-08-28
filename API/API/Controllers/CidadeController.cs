using Business.Services.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeService cidadeService) 
        {
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _cidadeService.GetAll());
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(Cidade cidade)
        {
            try
            {
                await _cidadeService.Cadastrar(cidade);
                return Ok();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

    }
}
