using System.ComponentModel.DataAnnotations;

namespace PARCIAL1B.Models
{
    public class ElementosPorPlato
    {

        [Key]
        public int ElementoPorPlatoID { get; set; }

        public int EmpresaID { get; set; }

        public int PlatoID { get; set; }

        public int ElementoID { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; }


    }
}
