using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _3laFein.Reprsentaion.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPut("location")]
        public async Task<IActionResult> UpdateLocation([FromBody] LocationUpdateDto dto)
        {
            // 1. Get the ID of the user currently logged in
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // 2. Fetch the user object
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            // 3. Create the Spatial Point (Using NTS logic)
            // SRID 4326 is the standard for GPS (WGS 84)
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            // Note: Coordinates are (Longitude, Latitude)
            user.Location = geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude));

            // 4. Save changes
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(new { message = "Location updated successfully" });

            return BadRequest("Could not update location");
        }
    }
}
