using safeprojectname.Entidades.Consultas;
using safeprojectname.Entidades.Operaciones;
using safeprojectname.Utils;
using GDifare.Utilitario.BaseDatos;
using GDifare.Utilitario.Comun;
using System;
using System.Data;
using Nest;

namespace safeprojectname.Datos
{
    public interface IMapeoDatosEjemplo  
    {
        Ejemplo Obtener(int idEjemplo);
        PagedCollection<Ejemplo> ObtenerListado(ListarEjemplosQuery query);
        GrabarEjemploResponse Grabar(GrabarEjemploRequest request);
    }

    public class MapeoDatosEjemplo : MapeoDatosBase, IMapeoDatosEjemplo
    {
        #region Constructores de la clase

        public MapeoDatosEjemplo(ISqlServer _sqlServer)
            : base(_sqlServer) { }

        #endregion

        #region Implementación de la interface

        GrabarEjemploResponse IMapeoDatosEjemplo.Grabar(GrabarEjemploRequest request)
        {
            // Ejemplo: Se valida si existe el registro
            if (request.IdEjemplo > 0)
            {
                var ejemplo = ObtenerEjemplo(request.IdEjemplo);

                // Si no existe se genera una respuesta de operación no exitosa.
                if (ejemplo == null)
                {
                    return new GrabarEjemploResponse(
                        MensajesEjemplos.CODE_ERROR_VAL_01,
                        MensajesEjemplos.ERROR_VAL_01);
                }
            }

            // Se graba el registro
            var idEjemplo = GrabarEjemplo(request);

            return new GrabarEjemploResponse()
            {
                IdEjemplo = idEjemplo
            };
        }

        Ejemplo IMapeoDatosEjemplo.Obtener(int idEjemplo)
        {
            return ObtenerEjemplo(idEjemplo);
        }

        PagedCollection<Ejemplo> IMapeoDatosEjemplo.ObtenerListado(ListarEjemplosQuery query)
        {
            return ObtenerListadoEjemplos(query);
        }

        #endregion

        #region Métodos de consulta de la clase

        private Ejemplo ObtenerEjemplo(int idEjemplo)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@i_accion", SqlDbType.Char, "C");
            SqlServer.AddParameter("@i_id_ejemplo", SqlDbType.Int, idEjemplo);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.ProcedureExample);

            if (dataSet.Tables.Count == 0) return null;
            if (dataSet.Tables[0].Rows.Count == 0) return null;

            var ejemplo = ConvertTo<Ejemplo>(dataSet.Tables[0]);

            // Se devuelve el objeto
            return ejemplo;
        }

        private PagedCollection<Ejemplo> ObtenerListadoEjemplos(ListarEjemplosQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@i_accion", SqlDbType.Char, "G");
            SqlServer.AddParameter("@i_campo_uno", SqlDbType.VarChar, query.CampoConsulta);
            SqlServer.AddParameter("@i_offset", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@i_limit", SqlDbType.Int, query.Limit);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.ProcedureExample);

            // Se genera la consulta paginada
            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var ejemplos = ConvertToList<Ejemplo>(dataSet.Tables[1]);

            // Se devuelve el objeto
            return new PagedCollection<Ejemplo>(ejemplos, totalRegistros, query.Limit);
        }

        #endregion

        #region Métodos de operaciones de la clase

        private int GrabarEjemplo(GrabarEjemploRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@i_accion", SqlDbType.Char, request.IdEjemplo.Equals(0) ? "I" : "M");
            SqlServer.AddParameter("@i_id_ejemplo", SqlDbType.Int, request.IdEjemplo);
            SqlServer.AddParameter("@i_campo_uno", SqlDbType.VarChar, request.CampoRequerimiento);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.ProcedureExample);

            return int.Parse(dataSet.Tables[0].Rows[0]["id_ejemplo"].ToString());
        }

        #endregion
    }

}