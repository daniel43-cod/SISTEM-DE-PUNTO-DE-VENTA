using SISTEMA_FROTEND.models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace SISTEMA_FROTEND.services
{
    public class ProductoService
    {
        private readonly HttpClient _httpClient;
        public ProductoService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44308/api/");
        }

        public async Task<List<Productos>> ListarProducto()
        {
            return await _httpClient.GetFromJsonAsync<List<Productos>>("Productos");
        }

        public async Task<List<Productos>> BuscarProducto(string texto)
        {
            return await _httpClient.GetFromJsonAsync<List<Productos>>($"Productos/buscar?texto={texto}");
        }

        public async Task<string> CrearProducto(Productos producto, decimal preciomayor, decimal preciomenor, string rutaImagen)
        {
            
            var contenido = new MultipartFormDataContent();

            contenido.Add(new StringContent(producto.nombre),"nombre");
            contenido.Add(new StringContent(producto.precio_compra.ToString()), "precio_compra");
            contenido.Add(new StringContent(producto.stock.ToString()),"stock");
            contenido.Add(new StringContent(producto.stock_minimo.ToString()),"stock_minimo");
            contenido.Add(new StringContent(producto.descripcion), "descripcion");
            contenido.Add(new StringContent(producto.codigo_barra),"codigo_barra");
            contenido.Add(new StringContent(producto.id_categoria.ToString()),"id_categoria");
            contenido.Add(new StringContent(preciomenor.ToString()), "precioMinorista"); // <- usa el parámetro recibido
            contenido.Add(new StringContent(preciomayor.ToString()), "precioMayorista"); // <- usa el otro parámetro

            //si el usuario crea el producto sin imagen se salta
            if (!string.IsNullOrEmpty(rutaImagen))
            {
                var bytes = File.ReadAllBytes(rutaImagen);
                contenido.Add(new ByteArrayContent(bytes), "imagen", Path.GetFileName(rutaImagen));
            }

            var response = await _httpClient.PostAsync("Productos/Crear", contenido);

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                return errorJson; // o parsea el JSON para sacar solo el "mensaje"
            }

            return null; // null significa que no hubo error
        }


    }
}
