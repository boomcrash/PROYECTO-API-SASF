using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;
using safeprojectname.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using GDifare.Utilitario.Comun;

namespace MicroserviciosGD1.Entidades.Operaciones
{
    public class GrabarInscripcionRequest : DifareBaseRequest
    {

        [JsonProperty("id_curso")]
        public int id_curso { get; set; }

        [JsonProperty("id_estudiante")]
        public int id_estudiante { get; set; }



        public override void IsValid()
        {
            if (id_curso <= 0|| id_estudiante <= 0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }


            base.IsValid();
        }
    }
}
