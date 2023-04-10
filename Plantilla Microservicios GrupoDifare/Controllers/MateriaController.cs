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
    public class MateriaController : DifareApiController
    {
        private readonly IMapeoDatosMateria mapeoDatosMateria;
      
        public MateriaController(IMapeoDatosMateria _mapeoDatosMateria,ILogHandler _logHandler) : base(_logHandler)
        {
            mapeoDatosMateria = _mapeoDatosMateria;
        }


        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("consultarMateriaById/{id_Materia:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Materia>> consultarMateriaById(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            int id_Materia)
        {
            try
            {

                var query = new ConsultaMateriaQuery { id_Materia = id_Materia };
                var result = query.IsValid(query.id_Materia);

                if (result)
                {
                    InitLog(CONSUMER, REFERENCE_ID, query.id_Materia.ToString());

                    query.IsValid();

                    var Materia = new Materia();
                    await Task.Factory.StartNew(() =>
                    {
                        Materia = mapeoDatosMateria.Obtener(query.id_Materia);
                    

                    });
                    Console.WriteLine(Materia);
                    return Ok(Materia);
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
        [HttpGet("listarMaterias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Materia>>> listarMaterias(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarMateriaQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Materias = new PagedCollection<Materia>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Materias = mapeoDatosMateria.ObtenerListado(query);
                });

                return Ok(Materias);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }



        //especifico:  listarMateriasByCurso

        //LISTADO - PAGINACION:
        [HttpGet("listarMateriasByCurso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Materia>>> listarMateriasByCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarMateriasByCursoQuery query)
        {
            try
            {
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                query.IsValid();

                var Materias = new PagedCollection<Materia>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    Materias = mapeoDatosMateria.ConsultarMateriasByCurso(query);
                });

                return Ok(Materias);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }


        //operaciones:

        // POST gdifare/api/modulo/proyecto/v1/grabar
        [HttpPost("grabarMateria")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> grabarMateria(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarMateriaRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarMateriaResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosMateria.Grabar(request);
                });

                return Created(string.Empty, response);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }





        //operaciones:

        // POST gdifare/api/modulo/proyecto/v1/grabar
        [HttpPost("AsignarMateriaToCurso")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> AsignarMateriaToCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarMateriaCursoRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarMateriaResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosMateria.GrabarMateriaCurso(request);
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
