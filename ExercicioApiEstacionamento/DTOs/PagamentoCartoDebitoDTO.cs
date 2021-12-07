using System;

namespace ExercicioApiEstacionamento.DTOs
{
    public class PagamentoCartoDebitoDTO : Validator
    {
        public string NomeDoCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string CodigoCvc { get; set; }
        public decimal Valor { get; set; }

        public override void Validar()
        {
            Valido = true;

            if (Valor <= 0)
            {
                Valido = false;
                throw new Exception ("Deve ser informado um valor.");
            }

            if (CodigoCvc.Length != 3)
            {
                Valido = false;
                throw new Exception("Código CVC inválido.");
            }

            if (NumeroCartao.Length < 11)
            {
                Valido = false;
                throw new Exception("Numero do cartão inválido.");
            }
        }
    }
}
