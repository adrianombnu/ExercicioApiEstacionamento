using ExercicioApiEstacionamento.DTOs;
using ExercicioApiEstacionamento.Entidades;
using ExercicioApiEstacionamento.Enumerados;
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

            try
            {
                var est = new Estacionamento(nome: estacionamentoDTO.Nome, documento: estacionamentoDTO.Documento);

                return Created("", _estacionamentoService.CadastrarEstacionamento(est));

            }catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar estacionamento: " + ex.Message);
            }

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
                var diaria = new Diaria(DateTime.Now, veiculo, veiculoDTO.Diaria.DiariaAdquirida, veiculoDTO.Diaria.DuchaAdquirida);

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
            try
            {
                return Created("", _estacionamentoService.FinalizarDiaria(id, placa));

            }catch (Exception ex)
            {
                return BadRequest("Erro ao finalizar diária: " + ex.Message);
            }
           
        }

        [HttpPost, Route("{idDiaria}/pagamento/")]
        public IActionResult FinalizarPagamento(Guid idDiaria, PagamentoDTO pagamentoDTO)
        {
            pagamentoDTO.Validar();

            if (!pagamentoDTO.Valido)
                return BadRequest("Pagamento inválido!");

            try
            {
                switch (pagamentoDTO.FormaPagamento)
                {
                    case EFormaPagamento.Boleto:
                        {
                            var formaPagamento = new Boleto(DateTime.Now, pagamentoDTO.PagamentoBoleto.Valor);

                            return Created("", _diariaService.FinalizarPagamento(idDiaria, formaPagamento));

                        }

                    case EFormaPagamento.CartaoCredito:
                        {
                            var formaPagamento = new CartaoDebito(pagamentoDTO.PagamentoCartaoCredito.NomeDoCartao,
                                                                  pagamentoDTO.PagamentoCartaoCredito.NumeroCartao,
                                                                  pagamentoDTO.PagamentoCartaoCredito.CodigoCvc,
                                                                  pagamentoDTO.PagamentoCartaoCredito.Valor);

                            return Created("", _diariaService.FinalizarPagamento(idDiaria, formaPagamento));

                        }

                    case EFormaPagamento.CartaoDebito:
                        {
                            var formaPagamento = new CartaoDebito(pagamentoDTO.PagamentoCartaoDebito.NomeDoCartao,
                                                                  pagamentoDTO.PagamentoCartaoDebito.NumeroCartao,
                                                                  pagamentoDTO.PagamentoCartaoDebito.CodigoCvc,
                                                                  pagamentoDTO.PagamentoCartaoDebito.Valor);

                            return Created("", _diariaService.FinalizarPagamento(idDiaria, formaPagamento));

                        }

                    case EFormaPagamento.Pix:
                        {
                            if (pagamentoDTO.PagamentoPix.ChavePix != null)
                            {
                                var formaPagamento = new Pix(pagamentoDTO.PagamentoPix.ChavePix, pagamentoDTO.PagamentoPix.Valor);

                                return Created("", _diariaService.FinalizarPagamento(idDiaria, formaPagamento));
                            }
                            else
                            {
                                var formaPagamento = new Pix(pagamentoDTO.PagamentoPix.CodigoBanco,
                                                             pagamentoDTO.PagamentoPix.CodigoAgencia,
                                                             pagamentoDTO.PagamentoPix.NumeroConta,
                                                             pagamentoDTO.PagamentoPix.Valor);

                                return Created("", _diariaService.FinalizarPagamento(idDiaria, formaPagamento));
                            }

                        }

                    default:
                        return BadRequest("Tipo de pagamento inválido!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao efetuar o pagamento: " + ex.Message);
            }
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
