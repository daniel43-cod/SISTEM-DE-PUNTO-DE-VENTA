using API_SISTEMA.data;
using API_SISTEMA.models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace API_SISTEMA.services
{
    public class VentaService
    {
        private readonly SistemaDbContext _context;

        public VentaService(SistemaDbContext context)
        {
            _context = context;
            
        }

        public async Task<List<Ventas>> ListarVentes()
        {
            return await _context.ventas.ToListAsync();
        }

        

        public async Task<Ventas> CrearVentas(Ventas ventas)
        {
            /*/consulta a la base de datos :V
            var UsuarioExistente = await _context.usuarios.FirstOrDefaultAsync(u => u.usuario == usuario.usuario || u.telefono == usuario.telefono);

            if (UsuarioExistente != null)
            {
                if (UsuarioExistente.usuario == usuario.usuario)
                    throw new InvalidOperationException("ya existe un usuario registrado con este nombre de usuario, por favor ingresa otro");

                if (UsuarioExistente.usuario == usuario.correo)
                    throw new InvalidOperationException("ya existe un usuario registrado con este correo por favor intenta con otro Correo Electronico");

                throw new InvalidOperationException("ya existe un usuario registrado con el mismo numero de telefono, por favor ingresa otro numero");
            }*/

            _context.ventas.Add(ventas);
            await _context.SaveChangesAsync();
            return ventas;
        }


    }
}
