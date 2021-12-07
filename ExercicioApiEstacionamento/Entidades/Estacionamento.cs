using Microsoft.Extensions.Options;
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

            var carro = _veiculo.SingleOrDefault(c => c.Placa == veiculo.Placa);

            if (carro is not null && carro.Diaria.DiariaFinalizada == false)
                throw new Exception("Veiculo já está com uma diária em andamento.");

            veiculo.IniciarDiaria(diaria);

            _veiculo.Add(veiculo);

        }

        public string FinalizarDiaria(string placa, Startup.MinhasConfiguracoes options)
        {
            var veiculo = _veiculo.SingleOrDefault(c => c.Placa == placa && c.Diaria.DiariaFinalizada == false);

            if (veiculo is null)
                throw new Exception("Veiculo não encontrado");

            veiculo.Diaria.AtualizarDiaria(DateTime.Now.AddMinutes(30), options);
            return GerarTicket(veiculo.Id);
            

        }

        public string GerarTicket(Guid id)
        {
            var veiculo = _veiculo.SingleOrDefault(c => c.Id == id);

            if (veiculo is null)
                throw new Exception("Veiculo não encontrado");

            return veiculo.ToString();

        }

    }
}

