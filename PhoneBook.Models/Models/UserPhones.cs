using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.Models
{
    public class UserPhones
    {
        public UserPhones()
        {
            User = new User();
            PhoneType = new PhoneTypes();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public int PhoneTypeId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Number { get; set; }
        public User User { get; set; }
        public PhoneTypes PhoneType { get; set; }
    }
}
