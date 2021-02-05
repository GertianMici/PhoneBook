using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.ViewModels
{
    public class PhoneNumberVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public int PhoneTypeId { get; set; }
    }
}
