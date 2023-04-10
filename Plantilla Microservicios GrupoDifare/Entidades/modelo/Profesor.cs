using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroserviciosGD1.Entidades.modelo
{

    public class Profesor

    {
        //mi codigo: 


        // el valor de column es el nombre como esta en tu tabla en la base de datos
        [Column("id_profesor")]
        //Este valor llamado json property es el nombre de cómo va a recibir a través del archivo json en el servicio swager 
        [JsonProperty("id_profesor")]
        // el valor de aqui abajo es como quieres representarlo en el back edn .net
        public int? id_profesor { get; set; }

        /*NOTA: (IdProfesor y id_profesor de column) ES COMO UN PUENTE PARA SABER COMO SE LLAMA EN LA BASE DE DATOS Y COMO SE LLAMA EN EL BACKEND,
         de esta manera si al recibir datos de la base de datos se llama diferente a como se llama en el backend
         esta va a entender que cuando envies o recibas IdProfesor , en realidad estas trabajando en la bd con
         id_profesor*/

        /*NOTA 2: (IdProfesor y id_profesor de JsonProperty)  Es como un puente para saber cómo va a recibir los parámetros en el swagger y a quién
        se lo va a agregar que en este caso sería a IdProfesor*/

        [Column("nombre_profesor")]
        [JsonProperty("nombre_profesor")]
        public string? nombre_profesor { get; set; }

        [Column("apellido_profesor")]
        [JsonProperty("apellido_profesor")]
        public string? apellido_profesor { get; set; }

        [Column("email_profesor")]
        [JsonProperty("email_profesor")]
        public string? email_profesor { get; set; }

        [Column("estado")]
        [JsonProperty("estado")]
        public string? estado { get; set; }
        //estado char

        [Column("fecha_creacion")]
        [JsonProperty("fecha_creacion")]
        public DateTime? fecha_creacion { get; set; }

        [Column("fecha_actualizacion")]
        [JsonProperty("fecha_actualizacion")]
        public DateTime? fecha_actualizacion { get; set; }

    }
}
