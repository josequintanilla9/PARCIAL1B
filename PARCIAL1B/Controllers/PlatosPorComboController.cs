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
    public class PlatosPorComboController : ControllerBase
    {



        private readonly platosContext _platosContexto;


        public PlatosPorComboController(platosContext platosContexto)
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
            List<PlatosPorCombo> listadoPlatosPorCombo = (from PlatosPorCombo in _platosContexto.platosPorCombo
                                          select PlatosPorCombo).ToList();

            if (listadoPlatosPorCombo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPlatosPorCombo);
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
            PlatosPorCombo? platosPorCombo = (from PlatosPorCombo in _platosContexto.platosPorCombo
                              where PlatosPorCombo.PlatoID == id
                              select PlatosPorCombo).FirstOrDefault();

            if (platosPorCombo == null)
            {
                return NotFound();
            }
            return Ok(platosPorCombo);
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
            PlatosPorCombo? platosPorCombo = (from PlatosPorCombo in _platosContexto.platosPorCombo
                              where PlatosPorCombo.Estado.Contains(filtro)
                              select PlatosPorCombo).FirstOrDefault();

            if (platosPorCombo == null)
            {
                return NotFound();
            }
            return Ok(platosPorCombo);
        }







        /// <summary>
        /// EndPoint que crea los registros de todos los equipos existentes 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]

        public IActionResult GuadarPlatosPorCombo([FromBody] PlatosPorCombo platosPorCombo)
        {

            try
            {

                _platosContexto.platosPorCombo.Add(platosPorCombo);
                _platosContexto.SaveChanges();
                return Ok(platosPorCombo);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }






        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPlatosPorCombo(int id, [FromBody] PlatosPorCombo platosPorComboModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteraremos alguna propiedad
            PlatosPorCombo? platosPorComboActual = (from PlatosPorCombo in _platosContexto.platosPorCombo
                                    where PlatosPorCombo.PlatosPorComboID == id
                                    select PlatosPorCombo).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (platosPorComboActual == null)
            { return NotFound(); }

            //Si se encuentra el registro, se alteran los campos modificables 
            platosPorComboActual.PlatosPorComboID = platosPorComboModificar.PlatosPorComboID;
            platosPorComboActual.EmpresaID = platosPorComboModificar.EmpresaID;
            platosPorComboActual.ComboID = platosPorComboModificar.ComboID;
            platosPorComboActual.PlatoID = platosPorComboModificar.PlatoID;
            platosPorComboActual.Estado = platosPorComboModificar.Estado;

            //Se marca el registro como modificado en el contexto
            //y se envia la modificacion a la base de datos

            _platosContexto.Entry(platosPorComboActual).State = EntityState.Modified;
            _platosContexto.SaveChanges();

            return Ok(platosPorComboModificar);
        }





        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarPlatosPorCombo(int id)
        {
            PlatosPorCombo? platosPorCombo = (from PlatosPorCombo in _platosContexto.platosPorCombo
                              where PlatosPorCombo.PlatosPorComboID == id
                              select PlatosPorCombo).FirstOrDefault();

            if (platosPorCombo == null)
            {
                return NotFound();


                _platosContexto.platosPorCombo.Attach(platosPorCombo);
                _platosContexto.platosPorCombo.Remove(platosPorCombo);
                _platosContexto.SaveChanges();

            }
            return Ok(platosPorCombo);
        }







    }
}
