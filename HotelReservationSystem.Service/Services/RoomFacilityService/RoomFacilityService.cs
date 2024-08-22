using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;

namespace HotelReservationSystem.Service.Services.RoomFacilityService
{
    public class RoomFacilityService : IRoomFacilityService
    {
        private readonly IunitOfWork _unitOfWork;

        public RoomFacilityService(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddFacilitiesToRoomAsync(int roomId, List<int> facilityIds)
        {
            var facilities = await _unitOfWork.Repository<Facility>()
               .GetAsync(f => facilityIds.Contains(f.Id));

            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(roomId);
            if (room == null)
            { 
                throw new InvalidOperationException($"Room with ID {roomId} does not exist.");
            }

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


        //public async Task AddOrUpdateFacilitiesToRoomAsync(int roomId, List<int> facilityIds)
        //{
        //    var roomFacilityRepo = _unitOfWork.Repository<RoomFacility>();

        //    var existingFacilities = await roomFacilityRepo.GetAsync(rf => rf.RoomId == roomId && rf.IsDeleted ==false);
        //    foreach (var facility in existingFacilities)
        //    {
        //        roomFacilityRepo.Delete(facility);
        //    }

        //    var facilities = await _unitOfWork.Repository<Facility>()
        //              .GetAsync(f => facilityIds.Contains(f.Id));

        //    var room = await _unitOfWork.Repository<Room>().GetByIdAsync(roomId);
        //    foreach (var facility in facilities)
        //    {
        //        room.RoomFacilities.Add(new RoomFacility
        //        {
        //            RoomId = room.Id,
        //            FacilityId = facility.Id
        //        });
        //    }

        //    _unitOfWork.Repository<Room>().Update(room);
        //    await _unitOfWork.CompleteAsync();
        //}


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

        public async Task UpdateFacilitiesInRoomAsync(int roomId, List<int> facilityIds)
        {
            var roomFacilityRepo = _unitOfWork.Repository<RoomFacility>();

            var existingFacilities = await roomFacilityRepo
                .GetAsync(rf => rf.RoomId == roomId && rf.IsDeleted == false);
 
            foreach (var existingFacility in existingFacilities)
            {
                if (!facilityIds.Contains(existingFacility.FacilityId))
                {
                    existingFacility.IsDeleted = true;
                }
                else
                {
                    existingFacility.IsDeleted = false;
                    facilityIds.Remove(existingFacility.FacilityId);
                }

                roomFacilityRepo.Update(existingFacility);
            }
            await AddFacilitiesToRoomAsync(roomId, facilityIds);

            await _unitOfWork.CompleteAsync();
        }
    }
    
}
