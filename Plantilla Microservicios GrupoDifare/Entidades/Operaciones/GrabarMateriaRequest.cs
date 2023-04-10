using GDifare.Utilitario.Comun.Exceptions;
using Newtonsoft.Json;
using safeprojectname.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using GDifare.Utilitario.Comun;

namespace MicroserviciosGD1.Entidades.Operaciones
{
    public class GrabarMateriaRequest : DifareBaseRequest
    {

        [JsonProperty("nombre_materia")]
        public string nombre_materia { get; set; }



        public override void IsValid()
        {

            if (string.IsNullOrWhiteSpace(nombre_materia))
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_TEXTO);
            }

            base.IsValid();
        }
    }


    public class GrabarMateriaCursoRequest : DifareBaseRequest
    {

        [JsonProperty("id_curso")]
        public int id_curso { get; set; }
        

        [JsonProperty("id_materia")]
        public int id_materia { get; set; }

        public override void IsValid()
        {

            if (id_materia<=0||id_curso<=0)
            {
                throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
            }

            base.IsValid();
        }
    }
}
