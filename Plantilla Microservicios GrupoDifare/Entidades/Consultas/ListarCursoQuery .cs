using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;
using safeprojectname.Utils;
using System.Text.RegularExpressions;

namespace MicroserviciosGD1.Entidades.Consultas
{
    public class ListarCursoQuery : PagedViewRequest
    {
        [JsonProperty("estado")]
        public char estado { get; set; } = 'E';

        public override void IsValid()
        {
            Regex regex = new Regex(@"^[AI]$");

            if (estado == 'E')
            {
                this.estado = 'A';
            }
            else
            {
                if (!regex.IsMatch(this.estado.ToString()))
                {
                    throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ESTADO);
                }
            }

            base.IsValid();
        }

    }

    public class ListarRegistradosByEstudianteQuery : PagedViewRequest
    {
        [JsonProperty("@id_estudiante")]
        public int id_estudiante { get; set; }

        public override void IsValid()
        {
            if (id_estudiante <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }

    }

    public class ListarCursosByprofesorQuery : PagedViewRequest
    {
        [JsonProperty("@id_profesor")]
        public int id_profesor { get; set; }

        public override void IsValid()
        {
            if (id_profesor <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }

    }
}
