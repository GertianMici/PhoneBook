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
        public List<PhoneTypes> GetPhoneTypes()
        {
            if (File.Exists(_phoneTypesFile))
            {
                var phoneType = File.ReadAllText(_phoneTypesFile);
                List<PhoneTypes> phoneTypesList = JsonSerializer.Deserialize<List<PhoneTypes>>(phoneType);
                return phoneTypesList;
            }
            return new List<PhoneTypes>();
        }
        public List<PhoneTypesVM> GetPhoneTypesVM()
        {
            if (File.Exists(_phoneTypesFile))
            {
                var phoneType = File.ReadAllText(_phoneTypesFile);
                List<PhoneTypesVM> phoneTypesList = JsonSerializer.Deserialize<List<PhoneTypesVM>>(phoneType);
                return phoneTypesList;
            }
            return new List<PhoneTypesVM>();
        }
        public User GetSpecificUser(int id)
        {
            if (File.Exists(_userFile))
            {
                var userjson = File.ReadAllText(_userFile);
                User user = JsonSerializer.Deserialize<List<User>>(userjson).FirstOrDefault(x => x.Id == id);
                return user;
            }
            return null;
        }
        public List<UserPhones> GetUserPhones()
        {
            if (File.Exists(_userPhonesFile))
            {
                var userPhone = File.ReadAllText(_userPhonesFile);
                List<UserPhones> userPhones = JsonSerializer.Deserialize<List<UserPhones>>(userPhone);
                return userPhones;
            }
            return new List<UserPhones>();
        }
        public List<UserPhones> GetUserPhonesForUser(int id)
        {
            if (File.Exists(_userPhonesFile))
            {
                var userPhone = File.ReadAllText(_userPhonesFile);
                List<UserPhones> userPhones = JsonSerializer.Deserialize<List<UserPhones>>(userPhone).Where(x => x.UserId == id).ToList();
                return userPhones;
            }
            return new List<UserPhones>();
        }
        public List<User> GetUsers()
        {
            if (File.Exists(_userFile))
            {

            var user = File.ReadAllText(_userFile);
            List<User> userList = JsonSerializer.Deserialize<List<User>>(user);
            return userList;
            }
            return new List<User>();
        }
        public void WriteUsers(List<User> userList)
        {
            string newUser = JsonSerializer.Serialize(userList);
            if (!File.Exists(_userFile))
                File.OpenWrite(_userFile);
            File.WriteAllTextAsync(_userFile, newUser);
        }
        public void WriteUserPhones(List<UserPhones> userPhones)
        {
            if (!File.Exists(_userPhonesFile))
                File.OpenWrite(_userPhonesFile);
            var newUserPhones = JsonSerializer.Serialize(userPhones);
            File.WriteAllTextAsync(newUserPhones, _userPhonesFile);
        }

        public void WritePhoneTypesVM(List<PhoneTypesVM> vm)
        {
            if (!File.Exists(_phoneTypesFile))
                File.OpenWrite(_phoneTypesFile);
            string newnphonetype = JsonSerializer.Serialize(vm);
            File.WriteAllTextAsync(_phoneTypesFile, newnphonetype);
        }
    }
}
