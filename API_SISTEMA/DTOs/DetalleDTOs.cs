using System.ComponentModel.DataAnnotations;

namespace API_SISTEMA.DTOs
{
    public class DetalleDTOs
    {
        public int id_detalle_venta { get; set; }
        public int id_venta { get; set; }
        public int id_producto { get; set; }

        public string nombre_producto { get; set; } = string.Empty;
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal? descuento { get; set; }
        public decimal subtotal { get; set; }

    }
}
