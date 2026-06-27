using System;
using System.Collections.Generic;
using System.Text;

namespace SISTEMA_FROTEND.DTOs
{
    public class DetalleDTOs
    {
       
            public int id_detalle_venta { get; set; }
            public int id_venta { get; set; }
            public int id_producto { get; set; }
            public string? nombre_producto { get; set; }
            public int cantidad { get; set; }
            public decimal precio { get; set; }
            public decimal? descuento { get; set; }
            public decimal subtotal { get; set; }
        
    }
}
