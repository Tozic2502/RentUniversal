using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Application.DTOs;

namespace RentUniversal.api.Controllers
{
    /// <summary>
    /// API controller for managing system license information.
    /// </summary>
    /// <remarks>
    /// This controller assumes a single global license instance.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _licenseService;

        public LicenseController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        /// <summary>
        /// Retrieves the current license.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<LicenseDTO>> GetLicense()
        {
            var license = await _licenseService.GetLicenseAsync();
            if (license == null) return NotFound();
            return Ok(license);
        }

        /// <summary>
        /// Updates the license information.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<LicenseDTO>> UpdateLicense([FromBody] License license)
        {
            var updated = await _licenseService.UpdateLicenseAsync(license);
            return Ok(updated);
        }
    }
}