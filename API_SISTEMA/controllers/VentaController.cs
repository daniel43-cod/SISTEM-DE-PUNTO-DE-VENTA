using API_SISTEMA.data;
using API_SISTEMA.DTOs;
using API_SISTEMA.models;
using API_SISTEMA.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API_SISTEMA.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        private readonly VentaService _context;

        public VentaController(VentaService service)
        {
            _context = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarVentas()
        {
            var listar = await _context.ListarVentes();
            return Ok(listar);
        }




    }


}
