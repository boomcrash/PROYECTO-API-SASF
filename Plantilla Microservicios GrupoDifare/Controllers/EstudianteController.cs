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
    public class EstudianteController : DifareApiController
    {
        private readonly IMapeoDatosEstudiante mapeoDatosEstudiante;
      
        public EstudianteController(IMapeoDatosEstudiante _mapeoDatosEstudiante,ILogHandler _logHandler) : base(_logHandler)
        {
            mapeoDatosEstudiante = _mapeoDatosEstudiante;
        }


        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("consultarEstudianteById/{id_estudiante:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Estudiante>> consultarEstudianteById(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            int id_estudiante)
        {
            try
            {

                var query = new ConsultaEstudianteQuery { id_estudiante = id_estudiante };
                var result = query.IsValid(query.id_estudiante);

                if (result)
                {

                    InitLog(CONSUMER, REFERENCE_ID, query.id_estudiante.ToString());

                    query.IsValid();

                    var Estudiante = new Estudiante();
                    await Task.Factory.StartNew(() =>
                    {
                        Estudiante = mapeoDatosEstudiante.Obtener(query.id_estudiante);
                    

                    });
                    Console.WriteLine(Estudiante);
                    return Ok(Estudiante);
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
        [HttpGet("listarEstudiantes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Estudiante>>> listarEstudiantes(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarEstudianteQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var estudiantes = new PagedCollection<Estudiante>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    estudiantes = mapeoDatosEstudiante.ObtenerListado(query);
                });

                return Ok(estudiantes);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }







        //operaciones:

        // POST gdifare/api/modulo/proyecto/v1/grabar
        [HttpPost("grabarEstudiante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> grabarEstudiante(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarEstudianteRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarEstudianteResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosEstudiante.Grabar(request);
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
