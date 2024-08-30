
namespace HotelReservationSystem.Service.Services.UserService.Dtos

{
    public class UserToReturnDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsSuccessed { get; set; } = true;


    }
}
