using System;

namespace ExercicioApiEstacionamento.DTOs
{
    public class PagamentoBoletoDTO : Validator
    {
        public DateTime DataVencimento { get;  set; }
        public decimal Valor { get;  set; }

        public override void Validar()
        {
            Valido = true;

            if (Valor <= 0)
                Valido = false;

        }
    }
}
