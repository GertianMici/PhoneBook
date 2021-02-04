using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.Models
{
    public class User
    {
        public User()
        {
            UserPhones = new List<UserPhones>();
        }
        public int Id { get; set; }
        [Required]
        public string  FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public virtual IEnumerable<UserPhones> UserPhones { get; set; }
    }
}
