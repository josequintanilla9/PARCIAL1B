using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1B.models;
using PARCIAL1B.Models;
using Microsoft.Extensions.Hosting;

namespace PARCIAL1B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementosPorPlatoController : ControllerBase
    {


        private readonly platosContext _platosContexto;


        public ElementosPorPlatoController(platosContext platosContexto)
        {
            _platosContexto = platosContexto;


        }



        /// <summary>
        /// EndPoint que retorna el lisado de todos los platos existentes 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<ElementosPorPlato> listadoElementosPorPlato = (from El in _platosContexto.elementosPorPlato
                                                select El).ToList();

            if (listadoElementosPorPlato.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoElementosPorPlato);
        }



        /// <summary>
        /// EndPoint que retorna el lisado de todos los platos existentes 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            ElementosPorPlato? elementosPorPlato = (from El in _platosContexto.elementosPorPlato
                                    where El.ElementoPorPlatoID == id
                                    select El).FirstOrDefault();

            if (elementosPorPlato == null)
            {
                return NotFound();
            }
            return Ok(elementosPorPlato);

        }






        /// <summary>
        /// EndPoint que retorna los registros de todos los equipos existentes 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            ElementosPorPlato? elementosPorPlato = (from El in _platosContexto.elementosPorPlato
                                    where El.Estado.Contains(filtro)
                                    select El).FirstOrDefault();

            if (elementosPorPlato == null)
            {
                return NotFound();
            }
            return Ok(elementosPorPlato);

        }






        /// <summary>
        /// EndPoint que crea los registros de todos los equipos existentes 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]

        public IActionResult GuadarElementosPorPlato([FromBody] ElementosPorPlato elementosPorPlato)
        {

            try
            {

                _platosContexto.elementosPorPlato.Add(elementosPorPlato);
                _platosContexto.SaveChanges();
                return Ok(elementosPorPlato);


            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                return BadRequest(ex.Message);
            }



        }





        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarElementosPorPlato(int id, [FromBody] ElementosPorPlato elementosPorPlatoModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteraremos alguna propiedad
            ElementosPorPlato? elementosPorPlatoActual = (from El in _platosContexto.elementosPorPlato
                                          where El.ElementoPorPlatoID == id
                                          select El).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (elementosPorPlatoActual == null)
            { return NotFound(); }

            //Si se encuentra el registro, se alteran los campos modificables 
            elementosPorPlatoActual.ElementoPorPlatoID = elementosPorPlatoModificar.ElementoPorPlatoID;
            elementosPorPlatoActual.EmpresaID = elementosPorPlatoModificar.EmpresaID;
            elementosPorPlatoActual.PlatoID = elementosPorPlatoModificar.PlatoID;
            elementosPorPlatoActual.ElementoID = elementosPorPlatoModificar.ElementoID;
            elementosPorPlatoActual.Cantidad = elementosPorPlatoModificar.Cantidad;
            elementosPorPlatoActual.Estado = elementosPorPlatoModificar.Estado;

            //Se marca el registro como modificado en el contexto
            //y se envia la modificacion a la base de datos

            _platosContexto.Entry(elementosPorPlatoActual).State = EntityState.Modified;
            _platosContexto.SaveChanges();

            return Ok(elementosPorPlatoModificar);
        }






        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarElementosPorPlato(int id)
        {
            ElementosPorPlato? elementosPorPlato = (from El in _platosContexto.elementosPorPlato
                                    where El.ElementoPorPlatoID == id
                                    select El).FirstOrDefault();

            if (elementosPorPlato == null)
            {
                return NotFound();


                _platosContexto.elementosPorPlato.Attach(elementosPorPlato);
                _platosContexto.elementosPorPlato.Remove(elementosPorPlato);
                _platosContexto.SaveChanges();

            }
            return Ok(elementosPorPlato);
        }






    }
}
