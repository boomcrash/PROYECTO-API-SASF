using safeprojectname.Utils;
using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Consultas
{
    public class ListarEjemplosQuery : PagedViewRequest
    {
        [JsonProperty("campoConsulta")]
        public string CampoConsulta { get; set; }

        public override void IsValid()
        {
            if (string.IsNullOrWhiteSpace(CampoConsulta))
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_VAL_01);
            }

            base.IsValid();
        }
    }
}