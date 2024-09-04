using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.UserSpecifications
{
    public class UserSpecification :BaseSpecifications<User>
    {
        public UserSpecification(string email)
            :base(u=> u.Email == email) 
        {
            Includes.Add(u => u.Include(u => u.UserRoles).ThenInclude(u => u.Role));
        }
    }
}
