using Microsoft.AspNetCore.Mvc;
using Application.Abstractions.Interfaces.Repositories;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowComputersController : ControllerBase
    {
        private readonly IFC_Repository _fcRepository;

        public FlowComputersController(IFC_Repository fcRepository)
        {
            _fcRepository = fcRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _fcRepository.GetAllAsync();
            return Ok(values);
        }
    }
}