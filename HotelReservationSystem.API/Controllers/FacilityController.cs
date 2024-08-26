using HotelReservationSystem.Service.Services.FacilityService;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{

    public class FacilityController : BaseController
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpPost]
        public async Task<ActionResult<FacilityDto>> AddFacility(FacilityDto facilityDto)
        {
            var facility = await _facilityService.CreateFacilityAsync(facilityDto);
            if (facility == null)
                return BadRequest();
            return Ok(facility);
        }

    }
}
