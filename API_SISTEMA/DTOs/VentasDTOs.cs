using API_SISTEMA.models;
using System.ComponentModel.DataAnnotations;

namespace API_SISTEMA.DTOs
{
    public class VentasDTOs
    {
        public int? id_cliente { get; set; }
        public ClienteDTOs? clienteNuevo { get; set; }

        public int? id_usuario { get; set; }
        public string tipo_venta { get; set; } = "";
        public string origen { get; set; } = "";

        //public string tipo_cliente { get; set; } = string.Empty;

        public decimal monto_pagado { get; set; }
        public string metodo_pago { get; set; } = string.Empty;
        public string? observacion_pago { get; set; }
        public decimal descuento { get; set; }
        public decimal impuesto { get; set; }
        public string? nit {  get; set; }
        public decimal saldo_pendiente { get; set; }
        public string estado_venta { get; set; } = string.Empty;


        public PagoCrearDTO pago {  get; set; }
        public List<DetalleDTOs> detalles { get; set; } = new();


        


    }
}
