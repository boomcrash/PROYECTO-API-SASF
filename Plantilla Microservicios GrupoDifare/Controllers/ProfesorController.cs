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
using safeprojectname.Entidades.Operaciones;
using MicroserviciosGD1.Entidades.Operaciones;
using GDifare.Utilitario.Comun.Exceptions;
using safeprojectname.Utils;

namespace MicroserviciosGD1.Controllers
{
    [Route("gdifare/api/modulo/proyecto/v1")]
    [ApiController]
    //Aquí abajo encontró el dolor puede dar algún tipo de error si es que alguno se agrega nada como un get ni un post
    public class ProfesorController : DifareApiController
    {
        //Llamo a la interfaz de profesor que ya había generado antes 
        private readonly IMapeoDatosProfesor mapeoDatosProfesor;

      
        //Instancia del constructor del controlador el cual es el que hace todo el tema de consola de logs 
        public ProfesorController(IMapeoDatosProfesor _mapeoDatosProfesor,ILogHandler _logHandler) : base(_logHandler)
        {
            mapeoDatosProfesor = _mapeoDatosProfesor;
        }

        //Perfecto si llegaste hasta aquí quiere decir que ya has logrado casi todo y ahora sólo te toca
        //generar los métodos para poder verlos en el swagger 

        // GET gdifare/api/modulo/proyecto/v1/consultar
        [HttpGet("consultarProfesorById/{id_profesor:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        //Arriba simplemente como controlar cualquier status code y abajo es algo que ya
        //hemos visto antes donde comenzamos a crear nuestra función asíncrona 

        //NOTA: El parámetro [FromHeader] string REFERENCE_ID, no se debe utilizar ni hacer caso
        // El parámetro [FromHeader] string CONSUMER, no se debe utilizar ni hacer caso
        //Estos parámetros son específicos para poder evidenciar los logs 

        //demás especificamos en el 'ActionResult' que me va a retornar un objeto profesor 
        public async Task<ActionResult<Profesor>> consultarProfesorById(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
           int id_profesor)
        {
            try
            {



                var query = new ConsultaProfesorQuery { id_profesor = id_profesor };
                var result = query.IsValid(query.id_profesor);

                if (result)
                {
                    // Inicialización de registro en Elasticsearch
                    //Query es un objeto de consulta profesor Y si recordamos esta clase recibiar un atributo 
                    //desde el json y lo guardaba en IdProfesor.
                    //Es por eso que abajo le mandamos ese parámetro .
                    InitLog(CONSUMER, REFERENCE_ID, query.id_profesor.ToString());

                    // Verificamos a través de la función que estaba en consulta profesor sí el id ingresado es válido 
                    query.IsValid();

                    // Creamos un objeto de tipo profesor que estaba en el modelo 
                    var profesor = new Profesor();
                    //Le decimos hey espera a que se realice la función a obtener sí provenía de la clase mapeo
                    //datos profesor , Para poder guardarlo en un objeto ya que en este caso la función obtener
                    //sólo me retornaba una fila porque es una consulta de un profesor por ID 
                    await Task.Factory.StartNew(() =>
                    {
                        profesor = mapeoDatosProfesor.Obtener(query.id_profesor);
                    

                    });
                    Console.WriteLine(profesor);
                    // Si todo fue correcto va a retornar el objeto profesor 
                    return Ok(profesor);
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
        [HttpGet("listarProfesores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Profesor>>> listarProfesores(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarProfesorQuery query)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                query.IsValid();

                // Ejecución de la operación de datos
                var profesores = new PagedCollection<Profesor>(null, 0, 0);
                await Task.Factory.StartNew(() =>
                {
                    profesores = mapeoDatosProfesor.ObtenerListado(query);
                });

                return Ok(profesores);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
        }




        //operaciones:

        // POST gdifare/api/modulo/proyecto/v1/grabar
        [HttpPost("grabarProfesor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> grabarProfesor(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarProfesorRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarProfesorResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosProfesor.Grabar(request);
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
        [HttpPost("asignarCalificacionEstudianteByCurso")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GrabarEjemploResponse>> asignarCalificacionEstudianteByCurso(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] GrabarProfesorAsignarCalificacionRequest request)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                InitLog(CONSUMER, REFERENCE_ID, string.Empty);

                // Validaciones de parámetros de entrada
                request.IsValid();

                // Ejecución de la operación de datos
                var response = new GrabarCalificacionResponse();
                await Task.Factory.StartNew(() =>
                {
                    response = mapeoDatosProfesor.GrabarCalificacion(request);
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
