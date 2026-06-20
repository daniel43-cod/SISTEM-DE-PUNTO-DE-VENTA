using API_SISTEMA.models;
using Microsoft.EntityFrameworkCore;

namespace API_SISTEMA.data
{
    public class SistemaDbContext :DbContext
    {

       
        //constructor de la clase
        public SistemaDbContext(
            DbContextOptions<SistemaDbContext> options)
            : base(options) 
        {
        }

        //funcion del dbset?
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Detalle_venta> detalle_Ventas { get; set; }
        public DbSet<Inventario_movimiento> inventario_Movimientos { get; set; }
        public DbSet<Pagos> pagos { get; set; }
        public DbSet<Producto_precio> precio_productos { get; set; }
        public DbSet<Productos> productos { get; set; }
        public DbSet<Proveedores> proveedores { get; set;}
        public DbSet<Rol> rols { get; set; }    
        public DbSet<Rol_permisocs> rol_Permisocs { get; set; }
        public DbSet<Tabla_permiso> tabla_Permisos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Ventas> ventas { get; set; }



        //mapear las tablas en SQLserver
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Categoria>().ToTable("categoria");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().ToTable("cliente");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>().ToTable("productos");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Detalle_venta>().ToTable("detalle_venta");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("usuario");
            base.OnModelCreating(modelBuilder);
        }

}}
