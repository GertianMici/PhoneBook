using System.Collections.Generic;

namespace PhoneBook.Models.Models
{
    public class PhoneTypes
    {
        public PhoneTypes()
        {
            UserPhones = new List<UserPhones>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<UserPhones> UserPhones { get; set; }
    }
}
