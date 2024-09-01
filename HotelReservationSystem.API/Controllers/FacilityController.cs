using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FacilityService;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using HotelReservationSystem.Service.Services.Helper.ResulteViewModel;
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


        [ProducesResponseType(typeof(FacilityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("GetAllFacility")]
        public async Task<ActionResult<Pagination<FacilityDto>>> GetAllFacility([FromQuery] SpecParams Params)
        {
            var facility = await _facilityService.GetAllFacilitiesAsync(Params);
            if (facility == null) return BadRequest(new ApiResponse(400));
            var count = await _facilityService.GetCount(Params);
            return Ok(new Pagination<FacilityDto>(Params.PageSize, Params.PageIndex, count, facility));
        }

        [ProducesResponseType(typeof(FacilityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPut("UpdateFacility")]
        public async Task<ActionResult<FacilityDto>> UpdateFacility(int id, FacilityDto facilityDto)
        {
            var facility = await _facilityService.UpdateFacilityAsync(id, facilityDto);
            if (facility == null) return NotFound(new ApiResponse(404));
            return Ok(facility);
        }

        [ProducesResponseType(typeof(FacilityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpPost("AddFacility")]
        public async Task<ActionResult<FacilityDto>> AddFacility(FacilityDto facilityDto)
        {
            var facility = await _facilityService.CreateFacilityAsync(facilityDto);
            if (facility == null)
                return BadRequest();
            return Ok(facility);
        }


        [ProducesResponseType(typeof(FacilityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("GetFacilityBy{id}")]
        public async Task<ActionResult<FacilityDto>> GetFacilityById(int id)
        {
            var facility = await _facilityService.GetFacilitiesByIdAsync(id);
            if (facility == null) return BadRequest(new ApiResponse(400));
            return Ok(facility);
        }


        [HttpDelete("DeleteFacility{id}")]
        public async Task<ActionResult<bool>> DeleteFacility(int id)
        {
            return Ok(await _facilityService.DeleteFacilityAsync(id));

        }

    }
}
