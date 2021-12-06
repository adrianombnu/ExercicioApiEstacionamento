using System.Collections.Generic;

namespace ExercicioApiEstacionamento.DTOs
{
    public class EstacionamentoDTO : Validator
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        

        public override void Validar()
        {
            Valido = true;  

            if(Nome is null || Nome.Length > 150)
                Valido = false;
            
            if(Documento is null || Documento.Length > 14)
                Valido=false;
                       
        }

    }
}
