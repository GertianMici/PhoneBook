using PhoneBook.Models.Models;
using PhoneBook.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Core.FileHandlers
{
    public interface IFileHandler
    {
        /// <summary>
        /// Handles file type by number 0-> JSON ,1 -> XML,2 ->Binary
        /// If not determined choose first
        /// </summary>
        uint FileType { get; }
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User data</returns>
        User GetSpecificUser(int id);
        /// <summary>
        /// Gets all numbers for an user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of UserPhones(class that contains user numbers</returns>
        List<UserPhones> GetUserPhonesForUser(int id);
        /// <summary>
        /// Gets all phone types ex home, work etc
        /// </summary>
        /// <returns></returns>
        List<PhoneTypes> GetPhoneTypes();
        /// <summary>
        /// Gets all phone types ex home, work etc
        /// </summary>
        /// <returns></returns>
        List<PhoneTypesVM> GetPhoneTypesVM();
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>List of users</returns>
        List<User> GetUsers();
        /// <summary>
        /// Gets all numbers that are in the phonebook
        /// </summary>
        /// <returns></returns>
        List<UserPhones> GetUserPhones();
        /// <summary>
        /// writes users to userFile
        /// </summary>
        /// <param name="userList"></param>
        void WriteUsers(List<User> userList);
        /// <summary>
        /// writes numbers to userPhones file
        /// </summary>
        /// <param name="userPhones"></param>
        void WriteUserPhones(List<UserPhones> userPhones);
        /// <summary>
        /// writes new phone types to file
        /// </summary>
        /// <param name="vm"></param>
        void WritePhoneTypesVM(List<PhoneTypesVM> vm);
    }
}
