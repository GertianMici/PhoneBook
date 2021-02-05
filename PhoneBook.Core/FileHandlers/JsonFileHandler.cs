using PhoneBook.Core.Constant;
using PhoneBook.Models.Models;
using PhoneBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneBook.Core.FileHandlers
{
    public class JsonFileHandler : IFileHandler
    {
        private readonly string _userFile;
        private readonly string _phoneTypesFile;
        private readonly string _userPhonesFile;
        public JsonFileHandler()
        {
            _userFile = Path.Combine(Constants.DatabasePath, "userFile.json");
            _phoneTypesFile = Path.Combine(Constants.DatabasePath, "phoneTypesFile.json");
            _userPhonesFile = Path.Combine(Constants.DatabasePath, "userPhonesFile.json");
        }
        public uint FileType => 0;
        public async Task<List<PhoneTypes>> GetPhoneTypes()
        {
            var phoneType = await ReadFromFile(_phoneTypesFile);
            if (string.IsNullOrEmpty(phoneType))
                return new List<PhoneTypes>();

            List<PhoneTypes> phoneTypesList = JsonSerializer.Deserialize<List<PhoneTypes>>(phoneType);
            return phoneTypesList;
        }
        public async Task<List<PhoneTypesVM>> GetPhoneTypesVM()
        {
            var phoneType = await ReadFromFile(_phoneTypesFile);
            if (string.IsNullOrEmpty(phoneType))
                return new List<PhoneTypesVM>();

            List<PhoneTypesVM> phoneTypesList = JsonSerializer.Deserialize<List<PhoneTypesVM>>(phoneType);
            return phoneTypesList;
        }
        public async Task<User> GetSpecificUser(int id)
        {

            var userjson = await ReadFromFile(_userFile);
            if (string.IsNullOrEmpty(userjson))
                return null;
            User user = JsonSerializer.Deserialize<List<User>>(userjson).FirstOrDefault(x => x.Id == id);
            return user;

        }
        public async Task<List<UserPhones>> GetUserPhones()
        {
            var userPhone = await ReadFromFile(_userPhonesFile);
            if (string.IsNullOrEmpty(userPhone))
                return new List<UserPhones>();
            List<UserPhones> userPhones = JsonSerializer.Deserialize<List<UserPhones>>(userPhone);
            return userPhones;
        }
        public async Task<List<UserPhones>> GetUserPhonesForUser(int id)
        {
            var userPhone = await ReadFromFile(_userPhonesFile);
            if (string.IsNullOrEmpty(userPhone))
                return new List<UserPhones>();
            List<UserPhones> userPhones = JsonSerializer.Deserialize<List<UserPhones>>(userPhone).Where(x => x.UserId == id).ToList();
            return userPhones;
        }
        public async Task<List<User>> GetUsers()
        {
            var user = await ReadFromFile(_userFile);
            if (string.IsNullOrEmpty(user))
                return new List<User>();
            List<User> userList = JsonSerializer.Deserialize<List<User>>(user);
            return userList;
        }
        public async Task WriteUsers(List<User> userList)
        {
            string newUser = JsonSerializer.Serialize(userList);
            await WriteToFile(_userFile, newUser);
        }
        public async Task WriteUserPhones(List<UserPhones> userPhones)
        {
            var newUserPhones = JsonSerializer.Serialize(userPhones);
            await WriteToFile(_userPhonesFile, newUserPhones);
        }

        public async Task WritePhoneTypesVM(List<PhoneTypesVM> vm)
        {
            string newnphonetype = JsonSerializer.Serialize(vm);
            await WriteToFile(_phoneTypesFile, newnphonetype);
        }
        public static async Task<string> ReadFromFile(string filePath)
        {
            if (File.Exists(filePath))
                return await File.ReadAllTextAsync(filePath);
            else
                return string.Empty;
        }
        public static async Task WriteToFile(string filepath, string toWrite)
        {
            await File.WriteAllTextAsync(filepath, toWrite);
        }
    }
}
