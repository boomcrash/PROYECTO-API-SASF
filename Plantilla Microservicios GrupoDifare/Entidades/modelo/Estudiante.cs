using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MicroserviciosGD1.Entidades.modelo
{
    public class Estudiante
    {
        [Column("id_estudiante")]
        [JsonProperty("id_estudiante")]
        public int? id_estudiante { get; set; }

        [Column("nombre_estudiante")]
        [JsonProperty("nombre_estudiante")]
        public string? nombre_estudiante { get; set; }

        [Column("apellido_estudiante")]
        [JsonProperty("apellido_estudiante")]
        public string? apellido_estudiante { get; set; }

        [Column("email_estudiante")]
        [JsonProperty("email_estudiante")]
        public string? email_estudiante { get; set; }

        [Column("estado")]
        [JsonProperty("estado")]
        public char Estado { get; set; }

        [Column("fecha_creacion")]
        [JsonProperty("fecha_creacion")]
        public DateTime? fecha_creacion { get; set; }

        [Column("fecha_actualizacion")]
        [JsonProperty("fecha_actualizacion")]
        public DateTime? fecha_actualizacion { get; set; }

        [Column("cedula")]
        [JsonProperty("cedula")]
        public string? cedula { get; set; }
        

    }
}
