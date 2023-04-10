using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Servicios;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Operaciones
{
    public class GrabarMateriaResponse : DifareBaseResponse
    {
        [JsonProperty("id_materia")]
        public int id_materia { get; set; }

        internal GrabarMateriaResponse()
            : base(true, ApiCodes.CODE_ERROR_API_00, ApiCodes.ERROR_API_00) { }

        internal GrabarMateriaResponse(string codigo, string mensaje)
            : base(false, codigo, mensaje) { }
    }
}