using ExercicioApiEstacionamento.Enumerados;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace ExercicioApiEstacionamento.Entidades
{
    public class Diaria : EntidadeBase
    {
        
        public Diaria(DateTime dataHoraInicio, Veiculo veiculo, bool diariaAdquirida,
                      bool duchaAdquirida) : base(Guid.NewGuid())
        {
            DataHoraInicio = dataHoraInicio;
            Veiculo = veiculo;
            DiariaAdquirida = diariaAdquirida;
            DuchaAdquirida = duchaAdquirida;

        }

        const decimal AbaixoQuinzeMinutosCarro = 2;
        const decimal AcimaQuinzeMinutosCarro = 10;
        const decimal DiariaCarro = 50;
        const decimal DuchaCarro = 65;

        const decimal AbaixoQuinzeMinutosMoto = 2;
        const decimal AcimaQuinzeMinutosMoto = 10;
        const decimal DiariaMoto = 50;
        const int TempoLimite = 15;

        public DateTime DataHoraInicio { get; private set; }
        public DateTime DataHoraFim { get; private set; }
        public Veiculo Veiculo { get; private set; }
        public bool DiariaAdquirida { get; private set; }
        public bool DuchaAdquirida { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public bool DiariaFinalizada { get; private set; }

        public void AtualizarDiaria(DateTime horaFim, Startup.MinhasConfiguracoes options)
        {
            DataHoraFim = horaFim;
            DiariaFinalizada = true;

            TimeSpan ts = DataHoraFim - DataHoraInicio;

            if (Veiculo.TipoVeiculo == ETipoVeiculo.Carro)
            {
                if (DiariaAdquirida)
                    ValorDiaria = options.DiariaCarro;
                else if (DuchaAdquirida)
                    ValorDiaria = options.DuchaCarro;
                else if (ts.TotalMinutes < options.TempoLimite)
                    ValorDiaria = options.AbaixoQuinzeMinutosCarro;
                else
                    ValorDiaria = options.AcimaQuinzeMinutosCarro;
            }
            else
            {
                if (DiariaAdquirida)
                    ValorDiaria = options.DiariaMoto;
                else if (ts.TotalMinutes < options.TempoLimite)
                    ValorDiaria = options.AbaixoQuinzeMinutosMoto;
                else ValorDiaria = options.AcimaQuinzeMinutosMoto;

            }

        }
        public void AdicionarVeiculo(Veiculo veiculo)
        {
            Veiculo = veiculo;

        }

    }
}
