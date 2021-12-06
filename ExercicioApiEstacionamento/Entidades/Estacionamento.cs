using System;
using System.Collections.Generic;
using System.Linq;

namespace ExercicioApiEstacionamento.Entidades
{
    public class Estacionamento : EntidadeBase
    {
        private List<Veiculo> _veiculo;
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public IReadOnlyList<Veiculo> Veiculo => _veiculo;

        public Estacionamento(string nome, string documento) : base(Guid.NewGuid())
        {
            Nome = nome;
            Documento = documento;
            _veiculo ??= new List<Veiculo>();
        }

        
        public void AdicionarVeiculo(Veiculo veiculo, Diaria diaria)
        {
            if (veiculo is null)
                throw new Exception("Não foi iniciado um veiculo");

            if (diaria is null)
                throw new Exception("Não foi iniciado uma diaria");

            veiculo.IniciarDiaria(new Diaria(DateTime.Now, veiculo, diaria.DiariaAdquirida, diaria.DuchaAdquirida));

            _veiculo.Add(veiculo);

        }

        public void FinalizarDiaria(string placa)
        {
            var veiculo = _veiculo.SingleOrDefault(c => c.Placa == placa);

            if (veiculo is null)
                throw new Exception("Veiculo não encontrado");

            veiculo.Diaria.AtualizarDiaria(DateTime.Now.AddMinutes(30));
            //veiculo.Diaria.AtualizarDiaria(DateTime.Now);

        }

        public string GerarTicket(string placa)
        {
            var veiculo = _veiculo.SingleOrDefault(c => c.Placa == placa);

            if (veiculo is null)
                throw new Exception("Veiculo não encontrado");

            return veiculo.ToString();

        }

    }
}

