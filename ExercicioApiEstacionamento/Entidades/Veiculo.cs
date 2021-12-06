using ExercicioApiEstacionamento.Enumerados;
using System;

namespace ExercicioApiEstacionamento.Entidades
{
    public class Veiculo : EntidadeBase
    {
        public Veiculo(string placa,
                       string modelo,
                       string cor,
                       ETipoVeiculo tipoVeiculo) : base(Guid.NewGuid())
        {
            Placa = placa;
            Modelo = modelo;
            Cor = cor;
            TipoVeiculo = tipoVeiculo;

        }

        public string Placa { get; private set; }
        public string Modelo { get; private set; }
        public string Cor { get; private set; }
        public Diaria Diaria { get; set; }
        public ETipoVeiculo TipoVeiculo { get; private set; }

        public void IniciarDiaria(Diaria diaria)
        {
            Diaria = diaria;

        }

        public override string ToString()
        {
            return @"Resumo diaria:" +
                    "\nPlaca: " + Placa +
                    "\nHora de entrada: " + Diaria.DataHoraInicio +
                    "\nHora de saída: " + Diaria.DataHoraFim +
                    "\nTotal da diaria: " + Diaria.ValorDiaria;
        }
    }
}
