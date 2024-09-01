using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.FeedBackService.Dtos
{
    public class FeedBackDto
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "The message is too long")]
        public string Text { get; set; }
        [Required]
        [Range(1, 6, ErrorMessage = "Rating must be between 1 and 6.")]
        public int Rating { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
