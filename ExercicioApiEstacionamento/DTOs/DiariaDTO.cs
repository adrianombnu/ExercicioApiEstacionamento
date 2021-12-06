using System;

namespace ExercicioApiEstacionamento.DTOs
{
    public class DiariaDTO : Validator
    {
        public DateTime DataHoraInicio { get; set; }
        //public DateTime DataHoraFim { get; set; }
        public VeiculoDTO Veiculo { get; set; }
        public bool DiariaAdquirida { get; set; }
        public bool DuchaAdquirida { get; set; }
        //public decimal ValorDiaria { get; set; }

        public override void Validar()
        {
            Valido = true;  

           // if(ValorDiaria <= 0)
            //    Valido = false;

        }

    }
}
