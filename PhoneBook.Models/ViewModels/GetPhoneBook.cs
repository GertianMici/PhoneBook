using System.Collections.Generic;
namespace PhoneBook.Models.ViewModels
{
    public class GetPhoneBook
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<GetPhoneNumberVM> PhoneNumbers { get; set; }
    }
}
