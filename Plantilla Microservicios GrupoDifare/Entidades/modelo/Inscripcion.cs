using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MicroserviciosGD1.Entidades.modelo
{
    public class Inscripcion
    {
        [Column("id_inscripcion")]
        [JsonProperty("id_inscripcion")]
        public int? id_inscripcion { get; set; }

        [Column("id_curso")]
        [JsonProperty("id_curso")]
        public int? id_curso { get; set; }

        [Column("id_estudiante")]
        [JsonProperty("id_estudiante")]
        public int? id_estudiante { get; set; }

        [Column("fecha_inscripcion")]
        [JsonProperty("fecha_inscripcion")]
        public DateTime? fecha_inscripcion { get; set; }

        [Column("estado")]
        [JsonProperty("estado")]
        public char? estado { get; set; }

        [Column("fecha_actualizacion")]
        [JsonProperty("fecha_actualizacion")]
        public DateTime? fecha_actualizacion { get; set; }
    }

    public class detalle_inscripcion
    {
        [Column("id_detalle_inscripcion")]
        [JsonProperty("id_detalle_inscripcion")]
        public int id_detalle_inscripcion { get; set; }

        [Column("id_inscripcion")]
        [JsonProperty("id_inscripcion")]
        public int id_inscripcion { get; set; }

        [Column("calificacion")]
        [JsonProperty("calificacion")]
        public float calificacion { get; set; }

        [Column("aprobado")]
        [JsonProperty("aprobado")]
        public bool aprobado { get; set; }
    }

}
