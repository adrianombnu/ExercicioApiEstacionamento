using ExercicioApiEstacionamento.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExercicioApiEstacionamento.Servicos
{
    public class EstacionamentoService
    {
        private readonly List<Estacionamento> _estacionamento;

        public EstacionamentoService()
        {
            _estacionamento ??= new List<Estacionamento>();
        }

        public Estacionamento CadastrarEstacionamento(Estacionamento estacionamento)
        {
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

        public Estacionamento FinalizarDiaria(Guid id, string placa)
        {
            var est = _estacionamento.SingleOrDefault(u => u.Id == id);

            if (est is null)
                throw new Exception("Estacionamento não encontrado!");

            est.FinalizarDiaria(placa);
            return est;
        }

    }
}
