using API_SISTEMA.data;
using API_SISTEMA.DTO;
using API_SISTEMA.models;
using Microsoft.EntityFrameworkCore;

namespace API_SISTEMA.services
{
    public class ProductoService
    {
        private readonly SistemaDbContext _context;

        public ProductoService(SistemaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Productos>> ListarProductos()
        {
            return await _context.productos.ToListAsync();
        }


        public async Task<List<Productos>> BuscarPorNombre(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return new List<Productos>();
            }

            var textoLower = texto.ToLower();

            return await _context.productos
                .Where(p => p.nombre.ToLower().Contains(textoLower))
                .ToListAsync();
        }

        //Ingresar producto

        public async Task<Productos> CrearProducto(productocrear productoDto, decimal precioMinorista, decimal precioMayorista, IFormFile imagen)
        {
            var producto = new Productos
            {
                codigo_barra = productoDto.codigo_barra,
                nombre = productoDto.nombre,
                descripcion = productoDto.descripcion,
                id_categoria = productoDto.id_categoria,
                precio_compra = productoDto.precio_compra,
                stock = productoDto.stock,
                stock_minimo = productoDto.stock_minimo,
                impuesto = productoDto.impuesto,
                fecha_creacion = DateTime.Now
            };

            if (imagen != null && imagen.Length > 0)
            {
                //genere el nombre de  la imagen
                string nombreArchivo = $"{Guid.NewGuid()}_{imagen.FileName}";
              //ruta donde se guarda la imagen
                string rutaCarpeta = Path.Combine("wwwroot", "imagenes", "productos");
                //construye la ruta completa
                string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                // crea la carpeta si no existe
                Directory.CreateDirectory(rutaCarpeta);

                // guarda el archivo en el disco
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                producto.imagen = nombreArchivo; // solo se guarda el nombre del archivo, no la imagen completa
            }
            //guarda en la base de datos
            _context.productos.Add(producto);
            await _context.SaveChangesAsync();

            var precioMin = new Producto_precio
            {
                id_producto = producto.id_producto,
                tipo_cliente = "minorista",
                precio = precioMinorista,
                estado = true
            };

            var precioMay = new Producto_precio
            {
                id_producto = producto.id_producto,
                tipo_cliente = "mayorista",
                precio = precioMayorista,
                estado = true
            };

            _context.producto_precios.Add(precioMin);
            _context.producto_precios.Add(precioMay);
            await _context.SaveChangesAsync();

           

            return producto;
        }
    }
}
