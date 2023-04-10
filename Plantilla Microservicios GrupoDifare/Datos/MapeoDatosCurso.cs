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


    public interface IMapeoDatosCurso
    {

        Curso Obtener(int id_Curso);
        PagedCollection<Curso> ObtenerListado(ListarCursoQuery query);

        int ConsultarCuposByCurso(int id_curso);

        PagedCollection<Curso> ListarRegistradosByEstudiante(ListarRegistradosByEstudianteQuery query);

        PagedCollection<Curso> ListarFinalizadosByEstudiante(ListarRegistradosByEstudianteQuery query);

        PagedCollection<Curso> ListarCursosAprobadosByEstudiante(ListarRegistradosByEstudianteQuery query);

        PagedCollection<Curso> ListarCursosReprobadosByEstudiante(ListarRegistradosByEstudianteQuery query);

        PagedCollection<Curso> ListarCursosByProfesor(ListarCursosByprofesorQuery query);

        GrabarCursoResponse Grabar(GrabarCursoRequest request);

        GrabarCursoResponse Modificar(ModificarCursoRequest request);



    }

    public class MapeoDatosCurso : MapeoDatosBase, IMapeoDatosCurso
    {


        public MapeoDatosCurso(ISqlServer _sqlServer)  : base(_sqlServer) { }

        Curso IMapeoDatosCurso.Obtener(int id_Curso)
        {
            return ObtenerCursos(id_Curso);
        }

        public Curso ObtenerCursos(int id_Curso)
        {

            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Curso");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarPorId");
            SqlServer.AddParameter("@id", SqlDbType.Int, id_Curso);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            if (dataSet.Tables.Count == 0) return null;
            if (dataSet.Tables[0].Rows.Count == 0) return null;
            var dataset = dataSet;
            var Curso = JsonConvert.DeserializeObject<Curso[]>(JsonConvert.SerializeObject(dataSet.Tables[0]))?.FirstOrDefault();


            return Curso;
        }


        PagedCollection<Curso> IMapeoDatosCurso.ObtenerListado(ListarCursoQuery query)
        {
            return ObtenerListadoProfesor(query);
        }

        private PagedCollection<Curso> ObtenerListadoProfesor(ListarCursoQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@estado", SqlDbType.VarChar, query.estado);
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Curso");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Consultar");
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Cursos = JsonConvert.DeserializeObject<Curso[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Curso>(Cursos, totalRegistros, query.Limit);
        }



        //especificos:
        int IMapeoDatosCurso.ConsultarCuposByCurso(int id_curso)
        {
            return ConsultarCupos(id_curso);
        }

        public int ConsultarCupos(int id_curso)
        {

            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarCuposDisponibles");
            SqlServer.AddParameter("@id_curso", SqlDbType.Int, id_curso);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            if (dataSet.Tables.Count == 0) return 0;
            if (dataSet.Tables[0].Rows.Count == 0) return 0;
            var dataset = dataSet;
            var totalInscritos = (int)dataSet.Tables[0].Rows[0]["cupos_disponibles"];

            return totalInscritos;

        }


        //ListarRegistradosByEstudiante
        PagedCollection<Curso> IMapeoDatosCurso.ListarRegistradosByEstudiante(ListarRegistradosByEstudianteQuery query)
        {
            return ObtenerListadoEstudiantesRegistrados(query);
        }

        private PagedCollection<Curso> ObtenerListadoEstudiantesRegistrados(ListarRegistradosByEstudianteQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarRegistradosByEstudiante");
            SqlServer.AddParameter("@id_estudiante", SqlDbType.Int, query.id_estudiante);
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Cursos = JsonConvert.DeserializeObject<Curso[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Curso>(Cursos, totalRegistros, query.Limit);
        }





        //ListarFinalizadosByEstudiante
        PagedCollection<Curso> IMapeoDatosCurso.ListarFinalizadosByEstudiante(ListarRegistradosByEstudianteQuery query)
        {
            return ListarFinalizados(query);
        }

        private PagedCollection<Curso> ListarFinalizados(ListarRegistradosByEstudianteQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarFinalizadosByEstudiante");
            SqlServer.AddParameter("@id_estudiante", SqlDbType.Int, query.id_estudiante);
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Cursos = JsonConvert.DeserializeObject<Curso[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Curso>(Cursos, totalRegistros, query.Limit);
        }


        //ListarCursosAprobadosByEstudiante
        PagedCollection<Curso> IMapeoDatosCurso.ListarCursosAprobadosByEstudiante(ListarRegistradosByEstudianteQuery query)
        {
            return ListarAprobados(query);
        }

        private PagedCollection<Curso> ListarAprobados(ListarRegistradosByEstudianteQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarCursosAprobados");
            SqlServer.AddParameter("@id_estudiante", SqlDbType.Int, query.id_estudiante);
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Cursos = JsonConvert.DeserializeObject<Curso[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Curso>(Cursos, totalRegistros, query.Limit);
        }



        //ListarCursosReprobadosByEstudiante
        PagedCollection<Curso> IMapeoDatosCurso.ListarCursosReprobadosByEstudiante(ListarRegistradosByEstudianteQuery query)
        {
            return ListarReprobados(query);
        }

        private PagedCollection<Curso> ListarReprobados(ListarRegistradosByEstudianteQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarCursosReprobados");
            SqlServer.AddParameter("@id_estudiante", SqlDbType.Int, query.id_estudiante);
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Cursos = JsonConvert.DeserializeObject<Curso[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Curso>(Cursos, totalRegistros, query.Limit);
        }


        //ListarCursosByProfesor
        PagedCollection<Curso> IMapeoDatosCurso.ListarCursosByProfesor(ListarCursosByprofesorQuery query)
        {
            return ListarCursosProfesor(query);
        }

        private PagedCollection<Curso> ListarCursosProfesor(ListarCursosByprofesorQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarCursosByProfesor");
            SqlServer.AddParameter("@id_profesor", SqlDbType.Int, query.id_profesor);
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Cursos = JsonConvert.DeserializeObject<Curso[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Curso>(Cursos, totalRegistros, query.Limit);
        }






        //operaciones: 
        GrabarCursoResponse IMapeoDatosCurso.Grabar(GrabarCursoRequest request)
        {
            var result = GrabarCurso(request);

            return new GrabarCursoResponse()
            {
                id_curso = result

            };

        }
        private int GrabarCurso(GrabarCursoRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Curso");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Insertar");
            SqlServer.AddParameter("@nombre_curso", SqlDbType.VarChar, request.nombre_curso);
            SqlServer.AddParameter("@descripcion_curso", SqlDbType.VarChar, request.descripcion_curso);
            SqlServer.AddParameter("@id_profesor", SqlDbType.Int, request.id_profesor);
            SqlServer.AddParameter("@cupos_disponibles", SqlDbType.Int, request.cupos_disponibles);
            SqlServer.AddParameter("@fecha_finalizacion", SqlDbType.DateTime, request.fecha_finalizacion);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            var id = (int)dataSet.Tables[0].Rows[0]["Column1"];

            return id;
        }



        //modificar:
        GrabarCursoResponse IMapeoDatosCurso.Modificar(ModificarCursoRequest request)
        {
            // Ejemplo: Se valida si existe el registro
            if (request.id_curso > 0)
            {
                var ejemplo = ObtenerCursos(request.id_curso);

                // Si no existe se genera una respuesta de operación no exitosa.
                if (ejemplo == null)
                {
                    return new GrabarCursoResponse(
                        MensajesEjemplos.CODE_ERROR_VAL_01,
                        MensajesEjemplos.ERROR_VAL_01,
                        request.id_curso);
                }
            }

            // Se graba el registro
            var id_Curso = ModificarCurso(request);

            return new GrabarCursoResponse()
            {
                id_curso = id_Curso
            };
        }

        private int ModificarCurso(ModificarCursoRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Curso");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Modificar");
            SqlServer.AddParameter("@nombre_curso", SqlDbType.VarChar, request.nombre_curso);
            SqlServer.AddParameter("@descripcion_curso", SqlDbType.VarChar, request.descripcion_curso);
            SqlServer.AddParameter("@id_profesor", SqlDbType.Int, request.id_profesor);
            SqlServer.AddParameter("@cupos_disponibles", SqlDbType.Int, request.cupos_disponibles);
            SqlServer.AddParameter("@fecha_finalizacion", SqlDbType.DateTime, request.fecha_finalizacion);
            SqlServer.AddParameter("@id", SqlDbType.Int, request.id_curso);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            return int.Parse(dataSet.Tables[0].Rows[0]["Column1"].ToString());
        }
    }
}
