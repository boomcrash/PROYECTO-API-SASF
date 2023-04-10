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
    public class ConsultaInscripcionQuery : DifareBaseRequest
    {
        [JsonProperty("id_Inscripcion")]
        public int id_Inscripcion { get; set; }


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
            if (id_Inscripcion <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }

    public class ConsultaTotalInscripcionQuery : DifareBaseRequest
    {
        [JsonProperty("id_curso")]
        public int id_curso { get; set; }


        public override void IsValid()
        {
            if (id_curso <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }

    public class ConsultaCalificacionEstudianteCursoQuery : DifareBaseRequest
    {
        [JsonProperty("id_curso")]
        public int id_curso { get; set; }

        [JsonProperty("id_estudiante")]
        public int id_estudiante { get; set; }


        public override void IsValid()
        {
            if (id_curso <= 0 || id_estudiante<=0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }
}
