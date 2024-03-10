using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1B.models;
using Microsoft.EntityFrameworkCore;
using PARCIAL1B.Models;
using Microsoft.Extensions.Hosting;


namespace PARCIAL1B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {

        private readonly platosContext _platosContexto;


        public PlatosController(platosContext platosContexto)
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
            List<Platos> listadoPlatos = (from p in _platosContexto.platos
                                           select p).ToList();

            if (listadoPlatos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPlatos);
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
            Platos? platos = (from p in _platosContexto.platos
                               where p.PlatoID == id
                               select p).FirstOrDefault();

            if (platos == null)
            {
                return NotFound();
            }
            return Ok(platos);
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
            Platos? platos = (from p in _platosContexto.platos
                               where p.DescripcionPlato.Contains(filtro)
                               select p).FirstOrDefault();

            if (platos == null)
            {
                return NotFound();
            }
            return Ok(platos);
        }




        /// <summary>
        /// EndPoint que crea los registros de todos los equipos existentes 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]

        public IActionResult GuadarPlatos([FromBody] Platos platos)
        {

            try
            {

                _platosContexto.platos.Add(platos);
                _platosContexto.SaveChanges();
                return Ok(platos);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }





        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPlatos(int id, [FromBody] Platos platosModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            //al cual alteraremos alguna propiedad
            Platos? platosActual = (from p in _platosContexto.platos
                                     where p.PlatoID == id
                                     select p).FirstOrDefault();

            //Verificamos que exista el registro segun su ID
            if (platosActual == null)
            { return NotFound(); }

            //Si se encuentra el registro, se alteran los campos modificables 
            platosActual.PlatoID = platosModificar.PlatoID;
            platosActual.EmpresaID = platosModificar.EmpresaID;
            platosActual.GrupoID = platosModificar.GrupoID;
            platosActual.NombrePlato = platosModificar.NombrePlato;
            platosActual.DescripcionPlato = platosModificar.DescripcionPlato;
            platosActual.Precio = platosModificar.Precio;

            //Se marca el registro como modificado en el contexto
            //y se envia la modificacion a la base de datos

            _platosContexto.Entry(platosActual).State = EntityState.Modified;
            _platosContexto.SaveChanges();

            return Ok(platosModificar);
        }






        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarPlatos(int id)
        {
            Platos? platos = (from p in _platosContexto.platos
                               where p.PlatoID == id
                               select p).FirstOrDefault();

            if (platos == null)
            {
                return NotFound();


                _platosContexto.platos.Attach(platos);
                _platosContexto.platos.Remove(platos);
                _platosContexto.SaveChanges();

            }
            return Ok(platos);
        }






    }

}

