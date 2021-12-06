using ExercicioApiEstacionamento.Entidades;
using ExercicioApiEstacionamento.Enumerados;

namespace ExercicioApiEstacionamento.DTOs
{
    public class VeiculoDTO : Validator
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public DiariaDTO Diaria { get; set; }
        public ETipoVeiculo TipoVeiculo { get; set; }

        public override void Validar()
        {
            Valido = true;

            if (Placa is null || Placa.Length != 8)
                Valido = false;

            if (Modelo is null || Modelo.Length > 150)
                Valido = false;

            if (Cor is null || Cor.Length > 100)
                Valido = false;

            if(TipoVeiculo != ETipoVeiculo.Carro && 
               TipoVeiculo != ETipoVeiculo.Moto)
                Valido = false;

            //Diaria.Validar();

        }

    }
}
