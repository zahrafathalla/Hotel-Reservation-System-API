﻿using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using HotelReservationSystem.Service.Services.OfferServices;
using HotelReservationSystem.Service.Services.OfferServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{

    [Authorize(Policy = "AdminOrStaffPolicy")]
    public class OfferController : BaseController
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }


        [HttpPost]
        public async Task<ActionResult<OfferToReturnDto>> AddOffer(OfferDto offerDto)
        {
            var Offer = await _offerService.AddOfferAsync(offerDto);
            if (Offer == null)
                return BadRequest(new ApiResponse(400));
            return Ok(Offer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OfferToReturnDto>> UpdateOffer(int id, OfferDto offerDto)
        {
            var Offer = await _offerService.UpdateOfferAsync(id, offerDto);
            if (Offer == null)
                return BadRequest(new ApiResponse(400));
            return Ok(Offer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteOffer(int id)
        {
            return Ok(await _offerService.DeleteOfferAsync(id));
        }
    }
}
