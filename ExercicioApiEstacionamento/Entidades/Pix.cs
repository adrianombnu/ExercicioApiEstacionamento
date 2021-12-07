using ExercicioApiEstacionamento.Enumerados;
using System;

namespace ExercicioApiEstacionamento.Entidades
{
    public class Pix : Pagamento
    {
        public string ChavePix { get; private set; }
        public int CodigoBanco { get; private set; }
        public int CodigoAgencia { get; private set; }
        public int NumeroConta { get; private set; }
        //public decimal Valor { get; private set; }

        public Pix(string chavePix, decimal valor)
        {
            ChavePix = chavePix;
            Valor = valor;
            FormaPagamento = EFormaPagamento.Pix;

            Validar();
        }

        public Pix(int codigoBanco, int codigoAgencia, int numeroConta, decimal valor)
        {
            CodigoBanco = codigoBanco;
            CodigoAgencia = codigoAgencia;
            NumeroConta = numeroConta;
            Valor = valor;
            FormaPagamento = EFormaPagamento.Pix;

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

            //Vaida Chave Pix

        }
    }
}
