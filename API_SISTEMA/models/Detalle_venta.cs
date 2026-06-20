using System.ComponentModel.DataAnnotations;

namespace API_SISTEMA.models
{
    public class Detalle_venta
    {
        [Key]
        public int id_detalle_venta {  get; set; }
        public int id_venta { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
        public decimal subtotal { get; set; }

   
    }
}
