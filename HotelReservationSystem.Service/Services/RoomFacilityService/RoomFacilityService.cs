using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.RoomFacilitySpecification;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;

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
            var roomFacilityRepo = _unitOfWork.Repository<RoomFacility>();

            var existingFacilities = await roomFacilityRepo.GetAsync(rf => rf.RoomId == roomId && rf.IsDeleted == false);

            var facilitiesToAdd = new List<int>();

            foreach (var facilityId in facilityIds)
            {
                if (!existingFacilities.Any(rf => rf.FacilityId == facilityId))
                {
                    facilitiesToAdd.Add(facilityId);
                }
            }

            var facilities = await _unitOfWork.Repository<Facility>()
                    .GetAsync(f => facilitiesToAdd.Contains(f.Id));

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

        public async Task<IEnumerable<Facility>> GetFacilitiesByRoomIdAsync(int roomId)
        {
            var spec = new RoomFacilitySpecification(roomId);

            var roomFacilities = await _unitOfWork.Repository<RoomFacility>()
                .GetAllWithSpecAsync(spec);

            var facilities = roomFacilities.Select(rf => rf.Facility);

            return facilities;
        }

        public async Task<bool> RemoveFacilityFromRoomAsync(int roomId)
        {
            var roomFacilityRepository = _unitOfWork.Repository<RoomFacility>();
            var existingFacilities = await roomFacilityRepository.GetAsync(rf => rf.RoomId == roomId);

            foreach (var facility in existingFacilities)
            {
                roomFacilityRepository.Delete(facility);
            }

            var result = await _unitOfWork.CompleteAsync();
            return result > 0;
        }
    }
}
