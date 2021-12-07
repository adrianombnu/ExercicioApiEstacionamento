﻿using ExercicioApiEstacionamento.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExercicioApiEstacionamento.Servicos
{
    public class DiariaService
    {
        private readonly List<Diaria> _diarias;
        
        public DiariaService()
        {
            _diarias ??= new List<Diaria>();
            
        }

        public Diaria Cadastrar(Diaria diaria)
        {
            _diarias.Add(diaria);
            return diaria;
        }

        public Diaria IniciarDiaria(Guid id, Veiculo veiculo)
        {
            var diaria = _diarias.SingleOrDefault(u => u.Id == id);

            if (diaria is null)
                throw new Exception("Diaria não encontrada!");

            diaria.AdicionarVeiculo(veiculo);
            
            return diaria;

        }

        public IEnumerable<Diaria> Get() => _diarias;
    }
}
