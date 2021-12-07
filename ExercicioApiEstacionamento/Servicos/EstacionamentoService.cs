using ExercicioApiEstacionamento.Entidades;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExercicioApiEstacionamento.Servicos
{
    public class EstacionamentoService
    {
        private readonly List<Estacionamento> _estacionamento;
        private readonly Startup.MinhasConfiguracoes _settigns;

        public EstacionamentoService(IOptions<Startup.MinhasConfiguracoes> options)
        {
            _estacionamento ??= new List<Estacionamento>();
            _settigns = options.Value;
        }

        public Estacionamento CadastrarEstacionamento(Estacionamento estacionamento)
        {
            var est = _estacionamento.Where(u => u.Documento == estacionamento.Documento).SingleOrDefault();

            if (est is not null)
                throw new Exception("Estacionamento já cadastrado!");

            _estacionamento.Add(estacionamento);
            return estacionamento;
        }

        public IEnumerable<Estacionamento> Get() => _estacionamento;

        public Estacionamento Get(Guid id)
        {
            var est = _estacionamento.Where(u => u.Id == id).SingleOrDefault();

            if (est == null)
                throw new Exception("Estacionamento não encontrado!");

            return est;

        }

        public Estacionamento AdicionarVeiculo(Guid id, Veiculo veiculo, Diaria diaria)
        {
            var est = _estacionamento.SingleOrDefault(u => u.Id == id);

            if (est is null)
                throw new Exception("Estacionamento não encontrado!");

            est.AdicionarVeiculo(veiculo, diaria);
            return est;
        }

        public string FinalizarDiaria(Guid id, string placa)
        {
            var est = _estacionamento.SingleOrDefault(u => u.Id == id);

            if (est is null)
                throw new Exception("Estacionamento não encontrado!");

            return est.FinalizarDiaria(placa, _settigns);
            
        }

    }
}
