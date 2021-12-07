using ExercicioApiEstacionamento.Enumerados;
using System;

namespace ExercicioApiEstacionamento.Entidades
{
    internal class Boleto : Pagamento
    {
        public DateTime DataVencimento { get; private set; }
        //public decimal Valor { get; private set; }

        public Boleto(DateTime dataVencimento, decimal valor)
        {
            DataVencimento = dataVencimento;
            Valor = valor;
            FormaPagamento = EFormaPagamento.Boleto;

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

            //Valida código de barras

        }
        
    }
}
