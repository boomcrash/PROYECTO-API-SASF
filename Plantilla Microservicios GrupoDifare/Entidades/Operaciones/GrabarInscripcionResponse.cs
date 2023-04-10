using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Servicios;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Operaciones
{
    public class GrabarInscripcionResponse : DifareBaseResponse
    {
        [JsonProperty("id_inscripcion")]
        public int id_inscripcion { get; set; }

        internal GrabarInscripcionResponse()
            : base(true, ApiCodes.CODE_ERROR_API_00, ApiCodes.ERROR_API_00) { }

        internal GrabarInscripcionResponse(string codigo, string mensaje)
            : base(false, codigo, mensaje) { }
    }
}