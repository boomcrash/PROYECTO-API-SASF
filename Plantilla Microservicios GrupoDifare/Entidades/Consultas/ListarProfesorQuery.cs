using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;
using safeprojectname.Utils;
using System.Text.RegularExpressions;

namespace MicroserviciosGD1.Entidades.Consultas
{
    public class ListarProfesorQuery : PagedViewRequest
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
}
