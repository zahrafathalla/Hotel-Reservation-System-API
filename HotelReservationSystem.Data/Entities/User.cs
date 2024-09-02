using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string  Displayname { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
