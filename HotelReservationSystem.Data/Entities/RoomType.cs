using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public enum RoomType
    {
        [EnumMember(Value = "Single")]
        Single,

        [EnumMember(Value = "Double")]
        Double,

        [EnumMember(Value = "Triple")]
        Triple,

        [EnumMember(Value = "Suite")]
        Suite


    }
}
