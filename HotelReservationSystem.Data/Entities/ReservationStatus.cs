using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        Cancelled
    }
}
