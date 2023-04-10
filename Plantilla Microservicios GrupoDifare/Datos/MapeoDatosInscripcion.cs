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


    public interface IMapeoDatosInscripcion
    {

        Inscripcion Obtener(int id_Inscripcion);
        PagedCollection<Inscripcion> ObtenerListado(ListarInscripcionQuery query);

        int ConsultarTotalInscritosByCurso(int id_curso);
        double ConsultaCalificacionEstudianteCurso(int id_curso, int id_estudiante);

        PagedCollection<Estudiante> ListarEstudiantesInscritosByCurso(ListarEstudiantesInscritosByCursoQuery query);

        GrabarInscripcionResponse Grabar(GrabarInscripcionRequest request);

    }

    public class MapeoDatosInscripcion : MapeoDatosBase, IMapeoDatosInscripcion
    {

        public MapeoDatosInscripcion(ISqlServer _sqlServer)  : base(_sqlServer) { }

        Inscripcion IMapeoDatosInscripcion.Obtener(int id_Inscripcion)
        {
            return ObtenerInscripcions(id_Inscripcion);
        }

        public Inscripcion ObtenerInscripcions(int id_Inscripcion)
        {

            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Inscripcion");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarPorId");
            SqlServer.AddParameter("@id", SqlDbType.Int, id_Inscripcion);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            if (dataSet.Tables.Count == 0) return null;
            if (dataSet.Tables[0].Rows.Count == 0) return null;
            var dataset = dataSet;
            var Inscripcion = JsonConvert.DeserializeObject<Inscripcion[]>(JsonConvert.SerializeObject(dataSet.Tables[0]))?.FirstOrDefault();


            return Inscripcion;
        }


        PagedCollection<Inscripcion> IMapeoDatosInscripcion.ObtenerListado(ListarInscripcionQuery query)
        {
            return ObtenerListadoProfesor(query);
        }

        private PagedCollection<Inscripcion> ObtenerListadoProfesor(ListarInscripcionQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@estado", SqlDbType.VarChar, query.estado);
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Inscripcion");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Consultar");
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Inscripcions = JsonConvert.DeserializeObject<Inscripcion[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Inscripcion>(Inscripcions, totalRegistros, query.Limit);
        }



        //especificos:
        int IMapeoDatosInscripcion.ConsultarTotalInscritosByCurso(int id_curso)
        {
            return ConsultarTotal(id_curso);
        }

        public int ConsultarTotal(int id_curso)
        {

            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarTotalInscritosByCurso");
            SqlServer.AddParameter("@id_curso", SqlDbType.Int, id_curso);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            if (dataSet.Tables.Count == 0) return 0;
            if (dataSet.Tables[0].Rows.Count == 0) return 0;
            var dataset = dataSet;
            var totalInscritos = (int)dataSet.Tables[0].Rows[0]["total_inscritos"];

            return totalInscritos;

        }


        //ConsultaCalificacionEstudianteCurso:
        double IMapeoDatosInscripcion.ConsultaCalificacionEstudianteCurso(int id_curso,int id_estudiante)
        {
            return ConsultarCalificacion(id_curso, id_estudiante);
        }

        public double ConsultarCalificacion(int id_curso, int id_estudiante)
        {

            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarCalificacionEstudianteByCurso");
            SqlServer.AddParameter("@id_curso", SqlDbType.Int, id_curso);
            SqlServer.AddParameter("@id_estudiante", SqlDbType.Int, id_estudiante);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            if (dataSet.Tables.Count == 0) return 0;
            if (dataSet.Tables[0].Rows.Count == 0) return 0;
            var dataset = dataSet;
            var calificacion = (double)dataSet.Tables[0].Rows[0]["calificacion"];

            return calificacion;

        }



        //ListarEstudiantesInscritosByCurso
        PagedCollection<Estudiante> IMapeoDatosInscripcion.ListarEstudiantesInscritosByCurso(ListarEstudiantesInscritosByCursoQuery query)
        {
            return ListarInscritosByCurso(query);
        }

        private PagedCollection<Estudiante> ListarInscritosByCurso(ListarEstudiantesInscritosByCursoQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarIncritosByCurso");
            SqlServer.AddParameter("@id_curso", SqlDbType.Int, query.id_curso);
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var estudiantes = JsonConvert.DeserializeObject<Estudiante[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Estudiante>(estudiantes, totalRegistros, query.Limit);
        }


        //operaciones:

        GrabarInscripcionResponse IMapeoDatosInscripcion.Grabar(GrabarInscripcionRequest request)
        {
            var result = GrabarInscripcion(request);

            return new GrabarInscripcionResponse()
            {
                id_inscripcion = result
            };

        }
        private int GrabarInscripcion(GrabarInscripcionRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Inscripcion");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Insertar");
            SqlServer.AddParameter("@id_curso", SqlDbType.Int, request.id_curso);
            SqlServer.AddParameter("@id_estudiante", SqlDbType.Int, request.id_estudiante);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);


            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Inscripcion");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Aux");

            // Se realiza la consulta a la base de datos
            var dataSet2 = SqlServer.ExecuteProcedure(StringHandler.procedure);
            var id = (int)dataSet2.Tables[0].Rows[0]["Column1"];

            //var id = (int)dataSet.Tables[0].Rows[0]["Column1"];


            if (id > 0)
            {
                SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "DetalleInscripcion");
                SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Insertar");
                SqlServer.AddParameter("@id", SqlDbType.Int, id);
                var dataSet3= SqlServer.ExecuteProcedure(StringHandler.procedure);
            }

            return id;
        }
    }
}
