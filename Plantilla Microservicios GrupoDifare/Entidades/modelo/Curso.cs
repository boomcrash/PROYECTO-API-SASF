using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MicroserviciosGD1.Entidades.modelo
{
    public class Curso
    {
        [Column("id_curso")]
        [JsonProperty("id_curso")]
        public int? id_curso { get; set; }

        [Column("nombre_curso")]
        [JsonProperty("nombre_curso")]
        public string? nombre_curso { get; set; }

        [Column("descripcion_curso")]
        [JsonProperty("descripcion_curso")]
        public string? descripcion_curso { get; set; }

        [Column("estado")]
        [JsonProperty("estado")]
        public char? estado { get; set; }

        [Column("fecha_creacion")]
        [JsonProperty("fecha_creacion")]
        public DateTime? fecha_creacion { get; set; }

        [Column("fecha_actualizacion")]
        [JsonProperty("fecha_actualizacion")]
        public DateTime? fecha_actualizacion { get; set; }

        [Column("id_profesor")]
        [JsonProperty("id_profesor")]
        public int? id_profesor { get; set; }

        [Column("cupos_disponibles")]
        [JsonProperty("cupos_disponibles")]
        public int? cupos_disponibles { get; set; }
    }

}
