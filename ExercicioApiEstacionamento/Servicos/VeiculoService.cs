using ExercicioApiEstacionamento.Entidades;
using System;
using System.Collections.Generic;

namespace ExercicioApiEstacionamento.Servicos
{
    public class VeiculoService
    {

        private readonly List<Veiculo> _veiculos;
        public VeiculoService()
        {
            _veiculos ??= new List<Veiculo>();
        }


        public Veiculo Cadastrar(Veiculo veiculos)
        {
            _veiculos.Add(veiculos);
            return veiculos;
        }

        

    }
}
