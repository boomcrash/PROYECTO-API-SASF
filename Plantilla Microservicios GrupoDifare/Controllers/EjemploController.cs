using safeprojectname.Datos;
using safeprojectname.Entidades.Consultas;
using safeprojectname.Entidades.Operaciones;
using GDifare.Utilitario.Comun;
using GDifare.Utilitario.Log;
using GDifare.Utilitario.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace safeprojectname.Controllers
{
    [Route("gdifare/api/modulo/proyecto/v1")]
    [ApiController]
    public class EjemploController : DifareApiController
    {
        #region Miembros privados del controlador

        private readonly IMapeoDatosEjemplo mapeoDatosEjemplo;

        #endregion

        #region Constructores del controlador

        public EjemploController(
            IMapeoDatosEjemplo _mapeoDatosEjemplo,
            ILogHandler _logHandler)
            : base(_logHandler)
        {
            mapeoDatosEjemplo = _mapeoDatosEjemplo;
        }

        #endregion

        #region Operaciones del controlador

        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("consultara")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Ejemplo>> Consultara(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ConsultarEjemploQuery query)
        {
            try
            {
                // Inicialización de registro en Elasticsearch
                InitLog(CONSUMER, REFERENCE_ID, query.IdEjemplo.ToString());

                // Validaciones de parámetros de entrada
                query.IsValid();

                // Ejecución de la operación de datos
                var ejemplo = new Ejemplo();
                await Task.Factory.StartNew(() =>
                {
                    ejemplo = mapeoDatosEjemplo.Obtener(query.IdEjemplo);
                });


                return Ok(ejemplo);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }

        // GET gdifare/api/modulo/proyecto/v1/listar
        [HttpGet("listar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Ejemplo>>> Listar(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarEjemplosQuery query)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                query.IsValid();

                // Ejecución de la operación de datos
                var ejemplos = new PagedCollection<Ejemplo>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    ejemplos = mapeoDatosEjemplo.ObtenerListado(query);
                });

                return Ok(ejemplos);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }

        // POST gdifare/api/modulo/proyecto/v1/grabar
        [HttpPost("grabar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> Grabar(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarEjemploRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarEjemploResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosEjemplo.Grabar(request);
                });

                return Created(string.Empty, response);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }

        // PUT gdifare/api/modulo/proyecto/v1/modificar
        [HttpPut("modificar")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> Modificar(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarEjemploRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarEjemploResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosEjemplo.Grabar(request);
                });

                return Accepted(response);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }

        #endregion
    }
}