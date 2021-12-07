using ExercicioApiEstacionamento.DTOs;
using ExercicioApiEstacionamento.Entidades;
using ExercicioApiEstacionamento.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ExercicioApiEstacionamento.Controllers
{

    [ApiController, Route("[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly VeiculoService _veiculoService;
        private readonly DiariaService _diariaService;
        private readonly EstacionamentoService _estacionamentoService;

        public VeiculoController(VeiculoService veiculoService, DiariaService diariaService, EstacionamentoService estacionamentoService)
        {
            _veiculoService = veiculoService;
            _diariaService = diariaService;
            _estacionamentoService = estacionamentoService;
        }
               
    }

}
