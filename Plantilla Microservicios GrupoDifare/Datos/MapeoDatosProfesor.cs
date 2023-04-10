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
using safeprojectname.Entidades.Operaciones;
using MicroserviciosGD1.Entidades.Operaciones;

namespace MicroserviciosGD1.Datos
{
    
    //mi codigo:

    //interfaz del metodo para mapear datos del profesor

    public interface IMapeoDatosProfesor
    {

        Profesor Obtener(int id_profesor);
        PagedCollection<Profesor> ObtenerListado(ListarProfesorQuery query);
        GrabarProfesorResponse Grabar(GrabarProfesorRequest request);

        GrabarCalificacionResponse GrabarCalificacion(GrabarProfesorAsignarCalificacionRequest request);

    }

    //clase donde se realiza las acciones del profesor,
    //extiende de MapeoDatosBase que es quien tiene toda la metodologia de conexion a sql server 
    //implementa IMapeoDatosProfesor que es el metodo que creamos arriba
    public class MapeoDatosProfesor : MapeoDatosBase, IMapeoDatosProfesor
    {

        //creamos el constructor, el mismo  que es el que hace la conexion a sql server
        public MapeoDatosProfesor(ISqlServer _sqlServer)  : base(_sqlServer) { }

        //aqui puede ser confuso, pero en realidad lo que estoy realizando es:
        //implementamos el metodo que creamos en la interfaz, agregando nuestras mas profundas necesidades
        Profesor IMapeoDatosProfesor.Obtener(int id_profesor)
        {
            return ObtenerProfesores(id_profesor);
        }

        //creamos un metodo , que es el que vamos a retornar en el metodo de arriba (nuestra interfaz)
        public Profesor ObtenerProfesores(int id_profesor)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            //en mi caso tabla y accion son parametros que recibe el procedimiento almacenado
            //como no son datos de ingresar por el susuario los pongo directamente
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Profesor");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarPorId");
            SqlServer.AddParameter("@id", SqlDbType.Int, id_profesor);

            // Se realiza la consulta a la base de datos
            //ProcedureExample es el nombre del procedimiento almacenado (como haciamos en NameStoreProcedures)
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            //se verifica si existen tablas resultantes de la consulta 
            if (dataSet.Tables.Count == 0) return null;
            //se verifica si existen datos en la tabla resultante de la consulta 
            if (dataSet.Tables[0].Rows.Count == 0) return null;
            //se convierte el contenido resultando en un objeto de tipo Profesor
            //(porque en este caso solo deberia retornar una fila)
            var dataset = dataSet;
            var profesor = JsonConvert.DeserializeObject<Profesor[]>(JsonConvert.SerializeObject(dataSet.Tables[0]))?.FirstOrDefault();


            // Se devuelve el objeto
            return profesor;
        }


        //paginacion Profesor
        PagedCollection<Profesor> IMapeoDatosProfesor.ObtenerListado(ListarProfesorQuery query)
        {
            return ObtenerListadoProfesor(query);
        }

        private PagedCollection<Profesor> ObtenerListadoProfesor(ListarProfesorQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@estado", SqlDbType.VarChar, query.estado);
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Profesor");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Consultar");
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            // Se genera la consulta paginada
            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var profesores = JsonConvert.DeserializeObject<Profesor[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            // Se devuelve el objeto
            return new PagedCollection<Profesor>(profesores, totalRegistros, query.Limit);
        }


        //operaciones


        GrabarProfesorResponse IMapeoDatosProfesor.Grabar(GrabarProfesorRequest request)
        {
            var result = GrabarProfesor(request);

            return new GrabarProfesorResponse()
            {
                id_profesor = result

            };

        }
        private int GrabarProfesor(GrabarProfesorRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar ,"Profesor");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Insertar");
            SqlServer.AddParameter("@nombre_profesor", SqlDbType.VarChar, request.nombre_profesor);
            SqlServer.AddParameter("@email_profesor", SqlDbType.VarChar, request.email_profesor);
            SqlServer.AddParameter("@apellido_profesor", SqlDbType.VarChar, request.apellido_profesor);
            SqlServer.AddParameter("@cedula", SqlDbType.VarChar, request.cedula);
            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            var id = (int)dataSet.Tables[0].Rows[0]["Column1"];

            return id;
        }








        GrabarCalificacionResponse IMapeoDatosProfesor.GrabarCalificacion(GrabarProfesorAsignarCalificacionRequest request)
        {
            var result = GrabarCalificacionEstudiante(request);

            return new GrabarCalificacionResponse()
            {
                editado = result

            };

        }
        private int GrabarCalificacionEstudiante(GrabarProfesorAsignarCalificacionRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "DetalleInscripcion");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Asignar");
            SqlServer.AddParameter("@id_curso", SqlDbType.Int, request.id_curso);
            SqlServer.AddParameter("@id_estudiante", SqlDbType.Int, request.id_estudiante);
            SqlServer.AddParameter("@calificacion", SqlDbType.Float, request.calificacion);
            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            var id = (int)dataSet.Tables[0].Rows[0]["Column1"];

            return id;
        }


        //hasta aqui ya esta terminado el codigo, ahora vamos a ver como se utiliza en el controlador (ve al controlador obtenerProfesores)
    }
}
