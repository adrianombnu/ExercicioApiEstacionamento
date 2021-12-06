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

        [HttpPost, Route("{id}/veiculo")]
        public IActionResult AdicionarVeiculo(Guid id, VeiculoDTO veiculoDTO)
        {
            veiculoDTO.Validar();

            if (!veiculoDTO.Valido)
                return BadRequest("Veiculo inválido!");

            try
            {
                var veiculo = new Veiculo(veiculoDTO.Placa, veiculoDTO.Modelo, veiculoDTO.Cor, veiculoDTO.TipoVeiculo);
                var diaria = new Diaria(veiculoDTO.Diaria.DataHoraInicio, veiculo, veiculoDTO.Diaria.DiariaAdquirida, veiculoDTO.Diaria.DuchaAdquirida);

                veiculo.Diaria = diaria;

                _estacionamentoService.AdicionarVeiculo(id, veiculo, diaria);
                _veiculoService.Cadastrar(veiculo);
                _diariaService.Cadastrar(diaria);
                return Created("", _diariaService.IniciarDiaria(diaria.Id, veiculo));

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao adicionar veiculo: " + ex.Message);
            }

        }

        /*
        [HttpGet, Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_diariaService.Get(id));

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_diariaService.Get());

        }
        */

    }

}
