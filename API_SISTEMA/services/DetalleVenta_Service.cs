using API_SISTEMA.data;
using API_SISTEMA.models;
using Microsoft.EntityFrameworkCore;

namespace API_SISTEMA.services
{
    public class DetalleVenta_Service
    {
        private readonly SistemaDbContext _context;

        public DetalleVenta_Service(SistemaDbContext context)
        {
            _context = context;

        }

        public async Task<List<Detalle_venta>> ListarVentes()
        {
            return await _context.detalle_Ventas.ToListAsync();
        }
    }
}
