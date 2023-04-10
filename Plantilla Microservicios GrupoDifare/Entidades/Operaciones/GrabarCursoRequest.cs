using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;
using safeprojectname.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using GDifare.Utilitario.Comun;

namespace MicroserviciosGD1.Entidades.Operaciones
{
    public class GrabarCursoRequest : DifareBaseRequest
    {

        [JsonProperty("nombre_curso")]
        public string nombre_curso { get; set; }


        [JsonProperty("descripcion_curso")]
        public string descripcion_curso { get; set; }


        [JsonProperty("id_profesor")]
        public int id_profesor { get; set; }

        [JsonProperty("cupos_disponibles")]
        public int cupos_disponibles { get; set; }

        [JsonProperty("fecha_finalizacion")]
        public DateTime fecha_finalizacion { get; set; }


        public override void IsValid()
        {

            if (string.IsNullOrWhiteSpace(nombre_curso) || string.IsNullOrWhiteSpace(descripcion_curso) )
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_TEXTO);
            }
            if (id_profesor <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            if (fecha_finalizacion == DateTime.MinValue)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_FECHA);
            }
            
            base.IsValid();
        }
    }

    public class ModificarCursoRequest : DifareBaseRequest
    {
        [JsonProperty("id_curso")]
        public int id_curso { get; set; }

        [JsonProperty("nombre_curso")]
        public string nombre_curso { get; set; }


        [JsonProperty("descripcion_curso")]
        public string descripcion_curso { get; set; }


        [JsonProperty("id_profesor")]
        public int id_profesor { get; set; }

        [JsonProperty("cupos_disponibles")]
        public int cupos_disponibles { get; set; }

        [JsonProperty("fecha_finalizacion")]
        public DateTime fecha_finalizacion { get; set; }


        public override void IsValid()
        {

            if (string.IsNullOrWhiteSpace(nombre_curso) || string.IsNullOrWhiteSpace(descripcion_curso))
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_TEXTO);
            }
            if (id_profesor <= 0|| id_curso<=0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            if (fecha_finalizacion == DateTime.MinValue)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_FECHA);
            }

            base.IsValid();
        }
    }
}
