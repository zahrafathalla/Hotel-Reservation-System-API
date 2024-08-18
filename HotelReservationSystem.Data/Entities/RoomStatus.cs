using System.Runtime.Serialization;

namespace HotelReservationSystem.Data.Entities
{
    public enum RoomStatus
    {
        [EnumMember(Value = "Available")]
        Available,

        [EnumMember(Value = "Occupied")]
        Occupied,
    }
}
