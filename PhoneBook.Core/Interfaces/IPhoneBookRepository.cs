using PhoneBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.Interfaces
{
    public interface IPhoneBookRepository
    {
        /// <summary>
        /// Gets an user by id, from the specified file type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        GetPhoneBook GetUser(int id, int fileType);
        /// <summary>
        /// Gets all users ordered by first name or lastname ,desc or asc, from the specified filetype
        /// </summary>
        /// <param name="firstnameOrder"></param>
        /// <param name="ascOrder"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        IEnumerable<GetPhoneBook> GetUsersOrdered(bool firstnameOrder, bool ascOrder, int fileType);
        /// <summary>
        /// Saves data to specified filetype
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        GetPhoneBook PostUser(PhoneBookVM vm, int fileType);
        /// <summary>
        /// Updates user to specified filetype
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        GetPhoneBook PutUser(PhoneBookVM vm, int fileType);
        /// <summary>
        /// Deletes an user from the specified filetype
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        bool DeleteUser(int id, int fileType);
        /// <summary>
        /// Gets all phone types from specified filetype
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        IEnumerable<PhoneTypesVM> GetPhoneTypes(int fileType);
        /// <summary>
        /// Adds a new phone number to the specified filetype
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        GetPhoneBook AddPhoneNumber(PhoneBookVM vm, int fileType);
        /// <summary>
        /// Adds a new phone type to specified filetype
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        bool AddPhoneTypesVM(PostPhoneTypesVM vm, int fileType);
    }
}
