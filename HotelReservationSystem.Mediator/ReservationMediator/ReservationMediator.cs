using AutoMapper;
using HotelReservationSystem.Service.Services.ReservationService;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using HotelReservationSystem.Service.Services.RoomService;

namespace HotelReservationSystem.Mediator.ReservationMediator
{
    public class ReservationMediator:IReservationMediator
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public ReservationMediator(IReservationService reservationService, IRoomService roomService, IMapper mapper)
        {
            _reservationService = reservationService;
            _roomService = roomService; 
            _mapper = mapper;
        }
        public async Task<ReservationToReturnDto> UpdateReservationAsync(int id, ReservationDto reservationDto)
        {
           var room =  await _roomService.GetRoomByIDAsync(reservationDto.RoomId);
            var reservation = await _reservationService.UpdateReservationAsync(id,reservationDto,room.Price);
          return  _mapper.Map<ReservationToReturnDto>(reservation);
        }
    }
}
