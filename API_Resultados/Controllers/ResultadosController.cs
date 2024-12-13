using System;
using System.Collections.Generic;
using System.Web.Http;
using Entities;
using BLL;

namespace API_Resultados.Controllers
{
    [RoutePrefix("api/Resultados")]
    public class ResultadosController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateResultado(ResultadosEvaluaciones resultado)
        {
            if (resultado == null)
                return BadRequest("El resultado no puede ser nulo.");

            var resultadosLogic = new ResultadosLogic();

            try
            {
                var createdResultado = resultadosLogic.Create(resultado);
                return Created($"api/Resultados/{createdResultado.ResultadoID}", createdResultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el resultado: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var resultadosLogic = new ResultadosLogic();
            var result = resultadosLogic.Delete(id);

            if (result)
                return Ok("Resultado eliminado correctamente.");
            else
                return NotFound();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var resultadosLogic = new ResultadosLogic();

            try
            {
                var resultados = resultadosLogic.RetrieveAll();
                if (resultados == null || resultados.Count == 0)
                    return NotFound();

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var resultadosLogic = new ResultadosLogic();

            try
            {
                var resultado = resultadosLogic.RetrieveById(id);
                if (resultado == null)
                    return NotFound();

                var response = new
                {
                    resultado.ResultadoID,
                    resultado.EvaluacionID,
                    resultado.EstudianteID,
                    resultado.Calificacion,
                    resultado.FechaCalificacion
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateResultado(int id, ResultadosEvaluaciones resultado)
        {
            if (resultado == null)
                return BadRequest("El resultado no puede ser nulo.");

            if (id != resultado.ResultadoID)
                return BadRequest("El ID del resultado no coincide con el ID de la URL.");

            var resultadosLogic = new ResultadosLogic();

            try
            {
                var updated = resultadosLogic.Update(resultado);
                if (updated)
                    return Ok("Resultado actualizado correctamente.");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el resultado: {ex.Message}");
            }
        }
    }
}