using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Servicios;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Operaciones
{
    public class GrabarEstudianteResponse : DifareBaseResponse
    {
        [JsonProperty("id_estudiante")]
        public int id_estudiante { get; set; }

        internal GrabarEstudianteResponse()
            : base(true, ApiCodes.CODE_ERROR_API_00, ApiCodes.ERROR_API_00) { }

        internal GrabarEstudianteResponse(string codigo, string mensaje)
            : base(false, codigo, mensaje) { }

        internal GrabarEstudianteResponse(string codigo, string mensaje, int id_curso)
    : base(false, codigo, mensaje) { this.id_estudiante = id_estudiante; }
    }
}