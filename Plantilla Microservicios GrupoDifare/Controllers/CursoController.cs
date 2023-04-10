using GDifare.Utilitario.Log;
using GDifare.Utilitario.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using safeprojectname.Datos;
using safeprojectname.Entidades.Consultas;
using System.Threading.Tasks;
using System;
using MicroserviciosGD1.Datos;
using MicroserviciosGD1.Entidades.Consultas;
using MicroserviciosGD1.Entidades.modelo;
using GDifare.Utilitario.Comun;
using MicroserviciosGD1.Entidades.Operaciones;
using safeprojectname.Entidades.Operaciones;
using GDifare.Utilitario.Comun.Exceptions;
using safeprojectname.Utils;

namespace MicroserviciosGD1.Controllers
{
    [Route("gdifare/api/modulo/proyecto/v1")]
    [ApiController]
    public class CursoController : DifareApiController
    {
        private readonly IMapeoDatosCurso mapeoDatosCurso;
      
        public CursoController(IMapeoDatosCurso _mapeoDatosCurso,ILogHandler _logHandler) : base(_logHandler)
        {
            mapeoDatosCurso = _mapeoDatosCurso;
        }


        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("consultarCursoById/{id_Curso:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Curso>> consultarCursoById(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            int id_Curso)
        {
            try
            {

                var query = new ConsultaCursoQuery {id_Curso=id_Curso };
                var result = query.IsValid(query.id_Curso);

                if (result)
                {
                    InitLog(CONSUMER, REFERENCE_ID, query.id_Curso.ToString());

                    query.IsValid();

                    var Curso = new Curso();
                    await Task.Factory.StartNew(() =>
                    {
                        Curso = mapeoDatosCurso.Obtener(query.id_Curso);


                    });
                    Console.WriteLine(Curso);
                    return Ok(Curso);
                }
                else
                {
                    throw new RequestException(MensajesEjemplos.CODE_ERROR_VAL_01, MensajesEjemplos.ERROR_ID);
                }

               
            }
            catch (RequestException error)
            {
                return BadRequest(error.Message);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }





        //LISTADO - PAGINACION:
        [HttpGet("listarCursos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Curso>>> listarCursos(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarCursoQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Cursos = new PagedCollection<Curso>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Cursos = mapeoDatosCurso.ObtenerListado(query);
                });

                return Ok(Cursos);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }




        //especifico:

        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("consultarCuposByCurso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> consultarCuposByCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ConsultaCuposCursoQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, query.id_curso.ToString());

                query.IsValid();

                var cupos = 0;
                await Task.Factory.StartNew(() =>
                {
                    cupos = mapeoDatosCurso.ConsultarCuposByCurso(query.id_curso);
                });
                Console.WriteLine(cupos);
                return Ok(cupos);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }



        //LISTADO - PAGINACION:   ObtenerListadoEstudiantesRegistrados
        [HttpGet("ObtenerListadoEstudiantesRegistrados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Curso>>> ObtenerListadoEstudiantesRegistrados(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarRegistradosByEstudianteQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Cursos = new PagedCollection<Curso>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Cursos = mapeoDatosCurso.ListarRegistradosByEstudiante(query);
                });

                return Ok(Cursos);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }



        //LISTADO - PAGINACION:   CursosFinzalizadosByEstudiante
        [HttpGet("CursosFinzalizadosByEstudiante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Curso>>> CursosFinzalizadosByEstudiante(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarRegistradosByEstudianteQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Cursos = new PagedCollection<Curso>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Cursos = mapeoDatosCurso.ListarFinalizadosByEstudiante(query);
                });

                return Ok(Cursos);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }




        //LISTADO - PAGINACION:   ListarCursosAprobadosByEstudiante
        [HttpGet("ListarCursosAprobadosByEstudiante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Curso>>> ListarCursosAprobadosByEstudiante(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarRegistradosByEstudianteQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Cursos = new PagedCollection<Curso>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Cursos = mapeoDatosCurso.ListarCursosAprobadosByEstudiante(query);
                });

                return Ok(Cursos);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }



        //LISTADO - PAGINACION:   ListarCursosReprobadosByEstudiante
        [HttpGet("ListarCursosReprobadosByEstudiante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Curso>>> ListarCursosReprobadosByEstudiante(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarRegistradosByEstudianteQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Cursos = new PagedCollection<Curso>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Cursos = mapeoDatosCurso.ListarCursosReprobadosByEstudiante(query);
                });

                return Ok(Cursos);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }



        //LISTADO - PAGINACION:   ListarCursosReprobadosByEstudiante
        [HttpGet("ListarCursosByProfesor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Curso>>> ListarCursosByProfesor(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarCursosByprofesorQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Cursos = new PagedCollection<Curso>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Cursos = mapeoDatosCurso.ListarCursosByProfesor(query);
                });

                return Ok(Cursos);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }




        //operaciones:

        // POST gdifare/api/modulo/proyecto/v1/grabar
        [HttpPost("grabarCurso")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> grabarCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarCursoRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarCursoResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosCurso.Grabar(request);
                });

                return Created(string.Empty, response);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }




        // PUT gdifare/api/modulo/proyecto/v1/modificar
        [HttpPut("modificarCurso")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> modificarCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] ModificarCursoRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarCursoResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosCurso.Modificar(request);
                });

                return Accepted(response);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }
    }
}
