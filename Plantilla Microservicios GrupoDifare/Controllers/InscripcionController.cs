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
    public class InscripcionController : DifareApiController
    {
        private readonly IMapeoDatosInscripcion mapeoDatosInscripcion;
      
        public InscripcionController(IMapeoDatosInscripcion _mapeoDatosInscripcion,ILogHandler _logHandler) : base(_logHandler)
        {
            mapeoDatosInscripcion = _mapeoDatosInscripcion;
        }


        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("consultarInscripcionById/{id_Inscripcion:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Inscripcion>> consultarInscripcionById(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            int id_Inscripcion)
        {
            try
            {


                var query = new ConsultaInscripcionQuery { id_Inscripcion = id_Inscripcion };
                var result = query.IsValid(query.id_Inscripcion);

                if (result)
                {

                    InitLog(CONSUMER, REFERENCE_ID, query.id_Inscripcion.ToString());

                    query.IsValid();

                    var Inscripcion = new Inscripcion();
                    await Task.Factory.StartNew(() =>
                    {
                        Inscripcion = mapeoDatosInscripcion.Obtener(query.id_Inscripcion);
                    
                    });
                    Console.WriteLine(Inscripcion);
                    return Ok(Inscripcion);
                }else
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
        [HttpGet("listarInscripcions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Inscripcion>>> listarInscripcions(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarInscripcionQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Inscripcions = new PagedCollection<Inscripcion>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Inscripcions = mapeoDatosInscripcion.ObtenerListado(query);
                });

                return Ok(Inscripcions);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }






        //especifico:
        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("consultarTotalInscritosByCurso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> consultarTotalInscritosByCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ConsultaTotalInscripcionQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, query.id_curso.ToString());

                query.IsValid();

                var total = 0;
                await Task.Factory.StartNew(() =>
                {
                    total = mapeoDatosInscripcion.ConsultarTotalInscritosByCurso(query.id_curso);
                });
                Console.WriteLine(total);
                return Ok(total);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }





        
        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("ConsultarCalificacionEstudianteByCurso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<double>> ConsultarCalificacionEstudianteByCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ConsultaCalificacionEstudianteCursoQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, query.id_curso.ToString());
                InitLog(CONSUMER, REFERENCE_ID, query.id_estudiante.ToString());

                query.IsValid();

                double calificacion = 0;
                await Task.Factory.StartNew(() =>
                {
                    calificacion = mapeoDatosInscripcion.ConsultaCalificacionEstudianteCurso(query.id_curso,query.id_estudiante);
                });
                Console.WriteLine(calificacion);
                return Ok(calificacion);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }




        //LISTADO - PAGINACION:   ListarEstudiantesInscritosByCurso
        [HttpGet("ListarEstudiantesInscritosByCurso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Curso>>> ListarEstudiantesInscritosByCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarEstudiantesInscritosByCursoQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Estudiantes = new PagedCollection<Estudiante>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Estudiantes = mapeoDatosInscripcion.ListarEstudiantesInscritosByCurso(query);
                });

                return Ok(Estudiantes);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }


        //operaciones:

        // POST gdifare/api/modulo/proyecto/v1/grabar
        [HttpPost("grabarInscripcion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> grabarInscripcion(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarInscripcionRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarInscripcionResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosInscripcion.Grabar(request);
                });

                return Created(string.Empty, response);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }
    }
}
