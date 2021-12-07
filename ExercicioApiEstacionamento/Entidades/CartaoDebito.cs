using ExercicioApiEstacionamento.Enumerados;
using System;

namespace ExercicioApiEstacionamento.Entidades
{
    public class CartaoDebito : Pagamento
    {
        public string NomeDoCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string CodigoCvc { get; private set; }
        //public decimal Valor { get; private set; }

        public CartaoDebito(string nomeDoCartao, string numeroCartao, string codigoCvc, decimal valor)
        {
            NomeDoCartao = nomeDoCartao;
            NumeroCartao = numeroCartao;
            CodigoCvc = codigoCvc;
            Valor = valor;
            FormaPagamento = EFormaPagamento.CartaoDebito;

            Validar();
        }

        public override void Validar()
        {
            Valido = true;

            if (Valor <= 0)
            {
                Valido = false;
                throw new Exception("Deve ser informado um valor.");
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

            //Valida limite
            
        }
    }
}
