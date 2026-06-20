using API_SISTEMA.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SISTEMA.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosControllercs : ControllerBase
    {

        private readonly ProductoService _Service;

        public ProductosControllercs(ProductoService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarProductos()
        {
            var listar = await _Service.ListarProductos();
            return Ok(listar);
        }

    }
}
