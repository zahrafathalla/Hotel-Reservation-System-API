using System.Runtime.Serialization;

namespace HotelReservationSystem.Data.Entities
{
    public enum ReservationStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "CheckedIn")]
        CheckedIn,

        [EnumMember(Value = "CheckedOut")]
        CheckedOut,

        [EnumMember(Value = "Cancelled")]
        Cancelled,

        [EnumMember(Value = "Failed")]
        Failed
    }
}
