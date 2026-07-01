using System.ComponentModel.DataAnnotations;

namespace API_SISTEMA.models
{
    public class EstadoVenta
    {
        [Key]
        public int id_estado_venta { get; set; }
        public string descripcion { get; set; }
        public  bool estado { get; set; }
    }
}
