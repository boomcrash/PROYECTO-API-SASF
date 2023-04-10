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


    public interface IMapeoDatosMateria
    {

        Materia Obtener(int id_Materia);
        PagedCollection<Materia> ObtenerListado(ListarMateriaQuery query);

        PagedCollection<Materia> ConsultarMateriasByCurso(ListarMateriasByCursoQuery query);

        GrabarMateriaResponse Grabar(GrabarMateriaRequest request);

        GrabarMateriaResponse GrabarMateriaCurso(GrabarMateriaCursoRequest request);
    }

    public class MapeoDatosMateria : MapeoDatosBase, IMapeoDatosMateria
    {

        public MapeoDatosMateria(ISqlServer _sqlServer)  : base(_sqlServer) { }

        Materia IMapeoDatosMateria.Obtener(int id_Materia)
        {
            return ObtenerMaterias(id_Materia);
        }

        public Materia ObtenerMaterias(int id_Materia)
        {

            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Materia");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarPorId");
            SqlServer.AddParameter("@id", SqlDbType.Int, id_Materia);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            if (dataSet.Tables.Count == 0) return null;
            if (dataSet.Tables[0].Rows.Count == 0) return null;
            var dataset = dataSet;
            var Materia = JsonConvert.DeserializeObject<Materia[]>(JsonConvert.SerializeObject(dataSet.Tables[0]))?.FirstOrDefault();


            return Materia;
        }


        PagedCollection<Materia> IMapeoDatosMateria.ObtenerListado(ListarMateriaQuery query)
        {
            return ObtenerListadoProfesor(query);
        }

        private PagedCollection<Materia> ObtenerListadoProfesor(ListarMateriaQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@estado", SqlDbType.VarChar, query.estado);
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Materia");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Consultar");
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Materias = JsonConvert.DeserializeObject<Materia[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Materia>(Materias, totalRegistros, query.Limit);
        }



        //especifico:

        PagedCollection<Materia> IMapeoDatosMateria.ConsultarMateriasByCurso(ListarMateriasByCursoQuery query)
        {
            return getMateriasByCurso(query);
        }

        private PagedCollection<Materia> getMateriasByCurso(ListarMateriasByCursoQuery query)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@id_curso", SqlDbType.VarChar, query.id_curso);
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Especifico");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "ConsultarMateriasByCurso");
            SqlServer.AddParameter("@limiteInicial", SqlDbType.Int, query.Offset);
            SqlServer.AddParameter("@limiteFinal", SqlDbType.Int, query.Limit);

            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);

            var totalRegistros = Convert.ToInt32(dataSet.Tables[0].Rows[0]["total_registros"]);
            var Materias = JsonConvert.DeserializeObject<Materia[]>(JsonConvert.SerializeObject(dataSet.Tables[1]));

            return new PagedCollection<Materia>(Materias, totalRegistros, query.Limit);
        }


        //operaciones:

        GrabarMateriaResponse IMapeoDatosMateria.Grabar(GrabarMateriaRequest request)
        {
            var result = GrabarMateria(request);

            return new GrabarMateriaResponse()
            {
                id_materia = result

            };

        }
        private int GrabarMateria(GrabarMateriaRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "Materia");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Insertar");
            SqlServer.AddParameter("@nombre_materia", SqlDbType.VarChar, request.nombre_materia);
            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            var id = (int)dataSet.Tables[0].Rows[0]["Column1"];

            return id;
        }





        GrabarMateriaResponse IMapeoDatosMateria.GrabarMateriaCurso(GrabarMateriaCursoRequest request)
        {
            var result = GrabarMateriaCursoI(request);

            return new GrabarMateriaResponse()
            {
                id_materia = result

            };

        }
        private int GrabarMateriaCursoI(GrabarMateriaCursoRequest request)
        {
            // Se establecen los parámetros del procedimiento a ejecutar
            SqlServer.AddParameter("@tabla", SqlDbType.VarChar, "MatCurso");
            SqlServer.AddParameter("@accion", SqlDbType.VarChar, "Insertar");
            SqlServer.AddParameter("@id_curso", SqlDbType.Int, request.id_curso);
            SqlServer.AddParameter("@id_materia", SqlDbType.Int, request.id_materia);

            // Se realiza la consulta a la base de datos
            var dataSet = SqlServer.ExecuteProcedure(StringHandler.procedure);
            var id = (int)dataSet.Tables[0].Rows[0]["Column1"];

            return id;
        }
    }
}
