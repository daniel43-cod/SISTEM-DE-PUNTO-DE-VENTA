using System;
using System.Collections.Generic;
using System.Text;

namespace SISTEMA_FROTEND.DTOs
{
    public class VentaDTOs
    {
        public class VentaDTO
        {
            public int id_ventas { get; set; }
            public string? nombre_cliente { get; set; }
            public decimal total { get; set; }
            public DateTime fecha_venta { get; set; }
        }
    }
}
