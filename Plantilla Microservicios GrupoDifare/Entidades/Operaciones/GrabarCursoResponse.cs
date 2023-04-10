using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Servicios;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Operaciones
{
    public class GrabarCursoResponse : DifareBaseResponse
    {
        [JsonProperty("id_curso")]
        public int id_curso { get; set; }

        internal GrabarCursoResponse()
            : base(true, ApiCodes.CODE_ERROR_API_00, ApiCodes.ERROR_API_00) { }

        internal GrabarCursoResponse(string codigo, string mensaje)
            : base(false, codigo, mensaje) { }

        internal GrabarCursoResponse(string codigo, string mensaje,int id_curso)
    :   base(false, codigo, mensaje) { this.id_curso = id_curso; }
    }
}