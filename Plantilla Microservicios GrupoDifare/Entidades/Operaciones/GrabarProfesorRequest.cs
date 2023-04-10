using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;
using safeprojectname.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using GDifare.Utilitario.Comun;

namespace MicroserviciosGD1.Entidades.Operaciones
{
    public class GrabarProfesorRequest : DifareBaseRequest
    {

        [JsonProperty("nombre_profesor")]
        public string nombre_profesor { get; set; }


        [JsonProperty("apellido_profesor")]
        public string apellido_profesor { get; set; }


        [JsonProperty("email_profesor")]
        public string email_profesor { get; set; }

        [JsonProperty("cedula")]
        public string cedula { get; set; }


        public override void IsValid()
        {

            if (string.IsNullOrWhiteSpace(nombre_profesor)|| string.IsNullOrWhiteSpace(apellido_profesor) || string.IsNullOrWhiteSpace(email_profesor) )
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_TEXTO);
            }

            base.IsValid();
        }
    }

    public class GrabarProfesorAsignarCalificacionRequest : DifareBaseRequest
    {

        [JsonProperty("id_curso")]
        public int id_curso { get; set; }


        [JsonProperty("id_estudiante")]
        public int id_estudiante { get; set; }


        [JsonProperty("calificacion")]
        public float calificacion { get; set; }



        public override void IsValid()
        {

            if (id_curso<=0|| id_estudiante <= 0 || calificacion <= 0 )
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }
}
