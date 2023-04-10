using GDifare.Utilitario.BaseDatos;
using MicroserviciosGD1.Entidades.modelo;
using safeprojectname.Utils;
using System.Data;
using GDifare.Utilitario.Comun;
using System;
using Newtonsoft.Json;
using System.Linq;
using safeprojectname.Entidades.Consultas;
using MicroserviciosGD1.Entidades.Consultas;
using safeprojectname.Datos;
using MicroserviciosGD1.Entidades.Operaciones;
using safeprojectname.Entidades.Operaciones;

namespace MicroserviciosGD1.Datos
{
    
    //mi codigo:


    public interface IMapeoDatosEstudiante
    {

        Estudiante Obtener(int id_Estudiante);
        PagedCollection<Estudiante> ObtenerListado(ListarEstudianteQuery query);

        GrabarEstudianteResponse Grabar(GrabarEstudianteRequest request);

        GrabarEstudianteResponse Modificar(ModificarEstudianteRequest request);

    }

    public class MapeoDatosEstudiante : MapeoDatosBase, IMapeoDatosEstudiante
    {

        public MapeoDatosEstudiante(ISqlServer _sqlServer)  : base(_sqlServer) { }

        Estudiante IMapeoDatosEstudiante.Obtener(int id_Estudiante)
        {
            return ObtenerEstudiantes(id_Estudiante);
        }

        public Estudiante ObtenerEstudiantes(int id_Estudiante)
        {

            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Estudiante");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarPorId");
            SqlServer.AddParameter("@id", SqlDbType.Int, id_Estudiante);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            if (dataSet.Tables.Count == 0) return null;
            if (dataSet.Tables[0].Rows.Count == 0) return null;
            var dataset = dataSet;
            var Estudiante = JsonConvert.DeserializeObject<Estudiante[]>(JsonConvert.SerializeObject(dataSet.Tables[0]))?.FirstOrDefault();


            return Estudiante;
        }


        PagedCollection<Estudiante> IMapeoDatosEstudiante.ObtenerListado(ListarEstudianteQuery query)
        {
            return ObtenerListadoProfesor(query);
        }

        private PagedCollection<Estudiante> ObtenerListadoProfesor(ListarEstudianteQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@estado", SqlDbType.VarChar, query.estado);
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Estudiante");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Consultar");
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Estudiantes = JsonConvert.DeserializeObject<Estudiante[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Estudiante>(Estudiantes, totalRegistros, query.Limit);
        }





        //operaciones: 
        GrabarEstudianteResponse IMapeoDatosEstudiante.Grabar(GrabarEstudianteRequest request)
        {
            var result = GrabarEstudiante(request);

            return new GrabarEstudianteResponse()
            {
                id_estudiante = result

            };

        }
        private int GrabarEstudiante(GrabarEstudianteRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Estudiante");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Insertar");
            SqlServer.AddParameter("@nombre_estudiante", SqlDbType.VarChar, request.nombre_estudiante);
            SqlServer.AddParameter("@email_estudiante", SqlDbType.VarChar, request.email_estudiante);
            SqlServer.AddParameter("@apellido_estudiante", SqlDbType.VarChar, request.apellido_estudiante);
            SqlServer.AddParameter("@cedula", SqlDbType.VarChar, request.cedula);
            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            var id = (int)dataSet.Tables[0].Rows[0]["Column1"];

            return id;
        }






        //modificar:
        GrabarEstudianteResponse IMapeoDatosEstudiante.Modificar(ModificarEstudianteRequest request)
        {
            // Ejemplo: Se valida si existe el registro
            if (request.id_estudiante > 0)
            {
                var ejemplo = ObtenerEstudiantes(request.id_estudiante);

                // Si no existe se genera una respuesta de operación no exitosa.
                if (ejemplo == null)
                {
                    return new GrabarEstudianteResponse(
                        MensajesEjemplos.CODE_ERROR_VAL_01,
                        MensajesEjemplos.ERROR_VAL_01,
                        request.id_estudiante);
                }
            }

            // Se graba el registro
            var id_Estudiante = ModificarEstudiante(request);

            return new GrabarEstudianteResponse()
            {
                id_estudiante = id_Estudiante
            };
        }

        private int ModificarEstudiante(ModificarEstudianteRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Estudiante");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Modificar");
            SqlServer.AddParameter("@nombre_estudiante", SqlDbType.VarChar, request.nombre_estudiante);
            SqlServer.AddParameter("@email_estudiante", SqlDbType.VarChar, request.email_estudiante);
            SqlServer.AddParameter("@apellido_estudiante", SqlDbType.VarChar, request.apellido_estudiante);
            SqlServer.AddParameter("@cedula", SqlDbType.VarChar, request.cedula);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            return int.Parse(dataSet.Tables[0].Rows[0]["Column1"].ToString());
        }
    }
}