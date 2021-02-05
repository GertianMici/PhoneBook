using System.ComponentModel.DataAnnotations;
namespace PhoneBook.Models.ViewModels
{
    public class PostPhoneTypesVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
