using API_SISTEMA.data;
using API_SISTEMA.DTOs;
using API_SISTEMA.models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

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


        public async Task<Ventas> CrearVentas(VentasDTOs dto)
        {//begintransaccion desace todo si algo falla en el proceso
            using var transaccion = await _context.Database.BeginTransactionAsync();

            try
            {
                //guarda el id del cliente
                int? idCliente = dto.id_cliente;

                //verifica si el cliente ya existe

                if (idCliente == null && dto.clienteNuevo != null)
                {
                    var cliente = new Cliente
                    {
                        nombre = dto.clienteNuevo.nombre,
                        apellido = dto.clienteNuevo.apellido,
                        nit = dto.clienteNuevo.nit,
                        dpi = dto.clienteNuevo.dpi,
                        telefono = dto.clienteNuevo.telefono,
                        correo_electronico = dto.clienteNuevo.correo_electronico,
                        direccion = dto.clienteNuevo.direccion,
                        tipo_cliente = dto.clienteNuevo.tipo_cliente,
                        limite_Credito = dto.clienteNuevo.limite_Credito,
                        estado = true
                    };

                    _context.cliente.Add(cliente);
                    await _context.SaveChangesAsync();

                    //otiene el idgenerado
                    idCliente = cliente.id_Cliente;
                }

                var subtotal = dto.detalles.Sum(d => d.cantidad * d.precio);
                var total = subtotal - dto.descuento + dto.impuesto;

                var venta = new Ventas
                {
                    id_cliente = idCliente,
                    id_usuario = dto.id_usuario,
                    subtotal = subtotal,
                    descuento = dto.descuento,
                    impuesto = dto.impuesto,
                    total = total,
                    estado_venta=dto.estado_venta,
                    tipo_venta = dto.tipo_venta,
                    origen = dto.origen,
                    saldo_pendiente = dto.saldo_pendiente
                };

                _context.ventas.Add(venta);
                await _context.SaveChangesAsync();

                foreach (var item in dto.detalles)
                {
                    var detalle = new Detalle_venta
                    {
                        id_venta = venta.id_ventas,
                        id_producto = item.id_producto,
                        cantidad = item.cantidad,
                        precio = item.precio,
                        descuento = item.descuento,
                        subtotal = item.cantidad * item.precio - (item.descuento ?? 0)
                    };

                    _context.detalle_Ventas.Add(detalle);
                }

                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();

                return venta;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }


    }
}
