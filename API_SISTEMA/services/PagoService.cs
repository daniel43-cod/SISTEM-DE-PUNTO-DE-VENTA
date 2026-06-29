using API_SISTEMA.data;
using API_SISTEMA.DTOs;
using API_SISTEMA.models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace API_SISTEMA.services
{
    public class PagoService
    {

        private readonly SistemaDbContext _context;

        public PagoService(SistemaDbContext context)
        {
            _context = context;
        }

        public async Task<Pagos> RegistrarPago(PagosDTO dto)
        {
            var venta = await _context.ventas
                .FirstOrDefaultAsync(v => v.id_ventas == dto.id_venta);

            if (venta == null)
                throw new Exception("La venta no existe.");

            if (dto.monto <= 0)
                throw new Exception("El monto del pago debe ser mayor a 0.");

            if (dto.monto > venta.saldo_pendiente)
                throw new Exception("El pago no puede ser mayor al saldo pendiente.");

            var pago = new Pagos
            {
                id_venta = dto.id_venta,
                id_usuario = dto.id_usuario,
                monto = dto.monto,
                medoto_pago = dto.medoto_pago,
                observacion = dto.observacion,
                fecha_pago = DateTime.Now
            };

            _context.pagos.Add(pago);

            venta.saldo_pendiente -= dto.monto;

            if (venta.saldo_pendiente == 0)
                venta.estado_venta = "Pagada";
            else
                venta.estado_venta = "Pendiente";

            await _context.SaveChangesAsync();

            return pago;
        }

    }
}
