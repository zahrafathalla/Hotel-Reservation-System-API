using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
