using System;

namespace ExercicioApiEstacionamento.DTOs
{
    public class PagamentoPixDTO : Validator
    {
        public string ChavePix { get; set; }
        public int CodigoBanco { get; set; }
        public int CodigoAgencia { get; set; }
        public int NumeroConta { get; set; }
        public decimal Valor { get; set; }

        public override void Validar()
        {
            Valido = true;

            if (Valor <= 0)
            {
                Valido = false;
                throw new Exception("Deve ser informado um valor.");
            }
        }
    }
}
