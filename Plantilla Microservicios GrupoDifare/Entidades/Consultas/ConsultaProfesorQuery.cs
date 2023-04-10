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
    public class ConsultaProfesorQuery : DifareBaseRequest
    {
        //Este es el valor que recibo el json y debe estar tal como lo puse en el modelo de profesor
        [JsonProperty("id_profesor")]
        //Aquí se recibe el valor que se envia en el json y se le asigna a IdProfesor
        public int id_profesor { get; set; }

        //verifico que el id sea mayor que cero

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
            if (id_profesor <= 0)
            {
                //Este mensaje ejemplos se encuentra ubicado en útiles y puede ser modificado o incluso puede ser cambiado 
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }
}
