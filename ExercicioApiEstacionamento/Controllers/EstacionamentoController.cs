using ExercicioApiEstacionamento.DTOs;
using ExercicioApiEstacionamento.Entidades;
using ExercicioApiEstacionamento.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ExercicioApiEstacionamento.Controllers
{
    [ApiController, Route("[controller]")]
    public class EstacionamentoController : ControllerBase
    {
        private readonly EstacionamentoService _estacionamentoService;
        private readonly VeiculoService _veiculoService;
        private readonly DiariaService _diariaService;

        public EstacionamentoController(EstacionamentoService estacionamentoService, VeiculoService veiculoService, DiariaService diariaService)
        {
            _estacionamentoService = estacionamentoService;
            _veiculoService = veiculoService;
            _diariaService = diariaService;

        }

        [HttpPost]
        public IActionResult Cadastrar(EstacionamentoDTO estacionamentoDTO)
        {
            estacionamentoDTO.Validar();

            if (!estacionamentoDTO.Valido)
                return BadRequest("Estacionamento informado inválido!");

            var est = new Estacionamento(nome: estacionamentoDTO.Nome, documento: estacionamentoDTO.Documento);

            return Created("", _estacionamentoService.CadastrarEstacionamento(est));
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

                _estacionamentoService.AdicionarVeiculo(id,veiculo, diaria);
                _veiculoService.Cadastrar(veiculo);
                _diariaService.Cadastrar(diaria);
                return Created("", _diariaService.IniciarDiaria(diaria.Id, veiculo));

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao adicionar veiculo: " + ex.Message);
            }

        }

        [HttpPost, Route("{id}/finalizarDiaria/{placa}")]
        public IActionResult FinalizarDiaria(Guid id,string placa)
        {

            return Created("", _estacionamentoService.FinalizarDiaria(id, placa));
           
        }

        [HttpGet, Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_estacionamentoService.Get(id));

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_estacionamentoService.Get());

        }


    }
}
