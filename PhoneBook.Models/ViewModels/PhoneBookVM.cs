using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.ViewModels
{
    public class PhoneBookVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public List<PhoneNumberVM> PhoneNumbers { get; set; }
    }
}
