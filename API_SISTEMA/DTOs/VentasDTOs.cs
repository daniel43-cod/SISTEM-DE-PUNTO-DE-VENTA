using API_SISTEMA.models;
using System.ComponentModel.DataAnnotations;

namespace API_SISTEMA.DTOs
{
    public class VentasDTOs
    {
        public int? id_cliente { get; set; }
        public ClienteDTOs? clienteNuevo { get; set; }

        public int? id_usuario { get; set; }
        public string tipo_venta { get; set; } = string.Empty;
        public string origen { get; set; } = string.Empty;

        public decimal descuento { get; set; }
        public decimal impuesto { get; set; }
        public decimal saldo_pendiente { get; set; }
        public string estado_venta { get; set; } = string.Empty;
        public List<DetalleDTOs> detalles { get; set; } = new();
        


    }
}
