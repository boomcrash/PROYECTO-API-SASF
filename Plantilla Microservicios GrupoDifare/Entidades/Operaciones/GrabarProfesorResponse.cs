using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Servicios;
using Newtonsoft.Json;

namespace safeprojectname.Entidades.Operaciones
{
    public class GrabarProfesorResponse : DifareBaseResponse
    {
        [JsonProperty("id_profesor")]
        public int id_profesor { get; set; }

        internal GrabarProfesorResponse()
            : base(true, ApiCodes.CODE_ERROR_API_00, ApiCodes.ERROR_API_00) { }

        internal GrabarProfesorResponse(string codigo, string mensaje)
            : base(false, codigo, mensaje) { }
    }

    public class GrabarCalificacionResponse : DifareBaseResponse
    {
        [JsonProperty("editado")]
        public int editado { get; set; }

        internal GrabarCalificacionResponse()
            : base(true, ApiCodes.CODE_ERROR_API_00, ApiCodes.ERROR_API_00) { }

        internal GrabarCalificacionResponse(string codigo, string mensaje)
            : base(false, codigo, mensaje) { }
    }


}