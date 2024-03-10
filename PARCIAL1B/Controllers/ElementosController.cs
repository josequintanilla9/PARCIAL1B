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
    public class ElementosController : ControllerBase
    {

        private readonly platosContext _platosContexto;


        public ElementosController(platosContext platosContexto)
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
            List<Elementos> listadoElementos = (from E in _platosContexto.elementos
                                          select E).ToList();

            if (listadoElementos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoElementos);
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
            Elementos? elementos = (from E in _platosContexto.elementos
                              where E.ElementoID == id
                              select E).FirstOrDefault();

            if (elementos == null)
            {
                return NotFound();
            }
            return Ok(elementos);
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
            Elementos? elementos = (from E in _platosContexto.elementos
                              where E.Elemento.Contains(filtro)
                              select E).FirstOrDefault();

            if (elementos == null)
            {
                return NotFound();
            }
            return Ok(elementos);

        }






        /// <summary>
        /// EndPoint que crea los registros de todos los equipos existentes 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]

        public IActionResult GuadarElementos([FromBody] Elementos elementos)
        {

            try
            {

                _platosContexto.elementos.Add(elementos);
                _platosContexto.SaveChanges();
                return Ok(elementos);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }




        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarElementos(int id, [FromBody] Elementos elementosModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteraremos alguna propiedad
            Elementos? elementosActual = (from E in _platosContexto.elementos
                                    where E.ElementoID == id
                                    select E).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (elementosActual == null)
            { return NotFound(); }

            //Si se encuentra el registro, se alteran los campos modificables 
            elementosActual.ElementoID = elementosModificar.ElementoID;
            elementosActual.EmpresaID = elementosModificar.EmpresaID;
            elementosActual.Elemento = elementosModificar.Elemento;
            elementosActual.CantidadMinima = elementosModificar.CantidadMinima;
            elementosActual.UnidadMedida = elementosModificar.UnidadMedida;
            elementosActual.Costo = elementosModificar.Costo;
            elementosActual.Estado = elementosModificar.Estado;

            //Se marca el registro como modificado en el contexto
            //y se envia la modificacion a la base de datos

            _platosContexto.Entry(elementosActual).State = EntityState.Modified;
            _platosContexto.SaveChanges();

            return Ok(elementosModificar);
        }







        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarElementos(int id)
        {
            Elementos? elementos = (from E in _platosContexto.elementos
                              where E.ElementoID == id
                              select E).FirstOrDefault();

            if (elementos == null)
            {
                return NotFound();


                _platosContexto.elementos.Attach(elementos);
                _platosContexto.elementos.Remove(elementos);
                _platosContexto.SaveChanges();

            }
            return Ok(elementos);
        }





    }
}
