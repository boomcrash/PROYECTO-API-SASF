using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Servicios;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Operaciones
{
    public class GrabarEjemploResponse : DifareBaseResponse
    {
        [JsonProperty("idEjemplo")]
        public int IdEjemplo { get; set; }

        internal GrabarEjemploResponse()
            : base(true, ApiCodes.CODE_ERROR_API_00, ApiCodes.ERROR_API_00) { }

        internal GrabarEjemploResponse(string codigo, string mensaje)
            : base(false, codigo, mensaje) { }
    }
}