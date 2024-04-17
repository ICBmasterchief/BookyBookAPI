using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookyBook.Models;

public class UserDTOOut
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime? RegistrationDate { get; set; }
        [Required]
        public decimal PenaltyFee { get; set; } = 0;
        [JsonIgnore]
        public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
        [Required]
        public string Role { get; set; }
}

