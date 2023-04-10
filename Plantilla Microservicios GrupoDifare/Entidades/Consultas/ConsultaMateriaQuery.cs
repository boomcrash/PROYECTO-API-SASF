using GDifare.Utilitario.Comun.Exceptions;
using GDifare.Utilitario.Comun;
using Newtonsoft.Json;
using safeprojectname.Utils;

namespace MicroserviciosGD1.Entidades.Consultas
{

    //mi codigo: 
    /* 
        Empiezo por crear una clase que extiende de DifareBaseRequest, esta clase es la que 
        verificara si el request es valido o no, si no es valido, lanzara una excepcion
     */
    public class ConsultaMateriaQuery : DifareBaseRequest
    {
        [JsonProperty("id_Materia")]
        public int id_Materia { get; set; }



        public bool IsValid(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        
        public override void IsValid()
        {
            if (id_Materia <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }
}
