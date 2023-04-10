using safeprojectname.Utils;
using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Operaciones
{
    public class GrabarEjemploRequest: DifareBaseRequest
    {
        [JsonProperty("idEjemplo")]
        public int IdEjemplo { get; set; }

        [JsonProperty("campoRequerimiento")]
        public string CampoRequerimiento { get; set; }

        public override void IsValid()
        {
            if (IdEjemplo <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            if (string.IsNullOrWhiteSpace(CampoRequerimiento))
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_TEXTO);
            }

            base.IsValid();
        }
    }
}