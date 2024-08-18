using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.RoomFacilityService
{
    public class RoomFacilityService : IRoomFacilityService
    {
        private readonly IunitOfWork _unitOfWork;

        public RoomFacilityService(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddOrUpdateFacilitiesToRoomAsync(int roomId, List<int> facilityIds)
        {
            var roomFacilityRepo = _unitOfWork.Repository<RoomFacility>();

            var existingFacilities = await roomFacilityRepo.GetAsync(rf => rf.RoomId == roomId && rf.IsDeleted ==false);
            foreach (var facility in existingFacilities)
            {
                roomFacilityRepo.Delete(facility);
            }

            var facilities = await _unitOfWork.Repository<Facility>()
                      .GetAsync(f => facilityIds.Contains(f.Id));

            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(roomId);
            foreach (var facility in facilities)
            {
                room.RoomFacilities.Add(new RoomFacility
                {
                    RoomId = room.Id,
                    FacilityId = facility.Id
                });
            }

            _unitOfWork.Repository<Room>().Update(room);
            await _unitOfWork.CompleteAsync();
        }


        public async Task RemoveFacilityFromRoomAsync(int roomId, List<int> facilityIds)
        {
            var roomFacilityRepo = _unitOfWork.Repository<RoomFacility>();
            var existingFacilities = await roomFacilityRepo
                            .GetAsync(rf => rf.RoomId == roomId && facilityIds.Contains(rf.FacilityId));

            foreach (var roomFacility in existingFacilities)
            {
                roomFacilityRepo.Delete(roomFacility);
            }

            await _unitOfWork.CompleteAsync();
        }


    }
}
