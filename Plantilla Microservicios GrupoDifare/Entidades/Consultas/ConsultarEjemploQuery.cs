using safeprojectname.Utils;
using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Consultas
{
    public class ConsultarEjemploQuery : DifareBaseRequest
    {
        [JsonProperty("idEjemplo")]
        public int IdEjemplo { get; set; }

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
            if (IdEjemplo <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }
}