using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MicroserviciosGD1.Entidades.modelo
{
    public class Materia
    {
        [Column("id_materia")]
        [JsonProperty("id_materia")]
        public int? id_materia { get; set; }

        [Column("nombre_materia")]
        [JsonProperty("nombre_materia")]
        public string? nombre_materia { get; set; }

        [Column("estado")]
        [JsonProperty("estado")]
        public char? estado { get; set; }

        [Column("fecha_creacion")]
        [JsonProperty("fecha_creacion")]
        public DateTime? fecha_creacion { get; set; }

        [Column("fecha_actualizacion")]
        [JsonProperty("fecha_actualizacion")]
        public DateTime? fecha_actualizacion { get; set; }
    }

}
