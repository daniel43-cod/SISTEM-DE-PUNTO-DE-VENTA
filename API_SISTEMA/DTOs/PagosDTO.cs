namespace API_SISTEMA.DTOs
{
    public class PagosDTO
    {
        public int id_venta { get; set; }
        public int id_usuario { get; set; }
        public decimal monto { get; set; }
        public string medoto_pago { get; set; }
        public string observacion { get; set; }
        public DateTime fecha_pago { get; set; }

    }
}
