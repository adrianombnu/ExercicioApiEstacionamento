using System;

namespace ExercicioApiEstacionamento.Entidades
{
    public class EntidadeBase
    {
        protected EntidadeBase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
