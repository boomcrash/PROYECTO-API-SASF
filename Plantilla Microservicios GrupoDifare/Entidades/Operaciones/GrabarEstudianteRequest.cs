using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;
using safeprojectname.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using GDifare.Utilitario.Comun;

namespace MicroserviciosGD1.Entidades.Operaciones
{
    public class GrabarEstudianteRequest : DifareBaseRequest
    {

        [JsonProperty("nombre_estudiante")]
        public string nombre_estudiante { get; set; }


        [JsonProperty("apellido_estudiante")]
        public string apellido_estudiante { get; set; }


        [JsonProperty("email_estudiante")]
        public string email_estudiante { get; set; }

        [JsonProperty("cedula")]
        public string cedula { get; set; }


        public override void IsValid()
        {

            if (string.IsNullOrWhiteSpace(nombre_estudiante) || string.IsNullOrWhiteSpace(apellido_estudiante) || string.IsNullOrWhiteSpace(email_estudiante) )
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_TEXTO);
            }

            base.IsValid();
        }
    }


    public class ModificarEstudianteRequest : DifareBaseRequest
    {

        [JsonProperty("id_estudiante")]
        public int id_estudiante { get; set; }

        [JsonProperty("nombre_estudiante")]
        public string nombre_estudiante { get; set; }


        [JsonProperty("apellido_estudiante")]
        public string apellido_estudiante { get; set; }


        [JsonProperty("email_estudiante")]
        public string email_estudiante { get; set; }

        [JsonProperty("cedula")]
        public string cedula { get; set; }


        public override void IsValid()
        {

            if (string.IsNullOrWhiteSpace(nombre_estudiante) || string.IsNullOrWhiteSpace(apellido_estudiante) || string.IsNullOrWhiteSpace(email_estudiante))
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_TEXTO);
            }
            if (id_estudiante <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }
}
