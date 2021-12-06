using ExercicioApiEstacionamento.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ExercicioApiEstacionamento.Controllers
{
    [ApiController, Route("[controller]")]
    public class DiariaController : ControllerBase
    {
        private readonly DiariaService _diariaService;
        
        public DiariaController(DiariaService diariaService)
        {
            _diariaService = diariaService;
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_diariaService.Get());
                        
        }
    }
}
