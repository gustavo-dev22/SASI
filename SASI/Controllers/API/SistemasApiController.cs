using Microsoft.AspNetCore.Mvc;
using SASI.Dominio.Repositories;

namespace SASI.Controllers.API
{
    [ApiController]
    [Route("api/sistemas")]
    public class SistemasApiController : Controller
    {
        private readonly ISistemaRepository _sistemaRepository;

        public SistemasApiController(ISistemaRepository sistemaRepository)
        {
            _sistemaRepository = sistemaRepository;
        }

        [HttpGet("{idSistema}")]
        public async Task<IActionResult> ObtenerPorCodigo(int idSistema)
        {
            try
            {
                var sistema = await _sistemaRepository.ObtenerPorId(idSistema);

                if (sistema == null)
                {
                    return NotFound(new
                    {
                        exito = false,
                        mensaje = "Sistema no encontrado."
                    });
                }

                return Ok(new
                {
                    exito = true,
                    mensaje = "Sistema encontrado.",
                    datos = new
                    {
                        sistema.IdSistema,
                        sistema.Codigo,
                        sistema.Nombre,
                        sistema.Activo
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    exito = false,
                    mensaje = "Error al consultar el sistema.",
                    error = ex.Message
                });
            }
        }
    }
}
