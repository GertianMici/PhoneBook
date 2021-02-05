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
                var phoneType = File.ReadAllTextAsync(_phoneTypesFile).Result;
                if (string.IsNullOrEmpty(phoneType))
                    return new List<PhoneTypes>();
                List<PhoneTypes> phoneTypesList = JsonSerializer.Deserialize<List<PhoneTypes>>(phoneType);
                return phoneTypesList;
            }
            return new List<PhoneTypes>();
        }
        public List<PhoneTypesVM> GetPhoneTypesVM()
        {
            if (File.Exists(_phoneTypesFile))
            {
                var phoneType = File.ReadAllTextAsync(_phoneTypesFile).Result;
                if (string.IsNullOrEmpty(phoneType))
                    return new List<PhoneTypesVM>();
                List<PhoneTypesVM> phoneTypesList = JsonSerializer.Deserialize<List<PhoneTypesVM>>(phoneType);
                return phoneTypesList;
            }
            return new List<PhoneTypesVM>();
        }
        public User GetSpecificUser(int id)
        {
            if (File.Exists(_userFile))
            {
                var userjson = File.ReadAllTextAsync(_userFile).Result;
                if (string.IsNullOrEmpty(userjson))
                    return null;
                User user = JsonSerializer.Deserialize<List<User>>(userjson).FirstOrDefault(x => x.Id == id);
                return user;
            }
            return null;
        }
        public List<UserPhones> GetUserPhones()
        {
            if (File.Exists(_userPhonesFile))
            {
                var userPhone = File.ReadAllTextAsync(_userPhonesFile).Result;
                if (string.IsNullOrEmpty(userPhone))
                    return new List<UserPhones>();
                List<UserPhones> userPhones = JsonSerializer.Deserialize<List<UserPhones>>(userPhone);
                return userPhones;
            }
            return new List<UserPhones>();
        }
        public List<UserPhones> GetUserPhonesForUser(int id)
        {
            if (File.Exists(_userPhonesFile))
            {
                var userPhone = File.ReadAllTextAsync(_userPhonesFile).Result;
                if (string.IsNullOrEmpty(userPhone))
                    return new List<UserPhones>();
                List<UserPhones> userPhones = JsonSerializer.Deserialize<List<UserPhones>>(userPhone).Where(x => x.UserId == id).ToList();
                return userPhones;
            }
            return new List<UserPhones>();
        }
        public List<User> GetUsers()
        {
            if (File.Exists(_userFile))
            {

                var user = File.ReadAllTextAsync(_userFile).Result;
                if (string.IsNullOrEmpty(user))
                    return new List<User>();
                List<User> userList = JsonSerializer.Deserialize<List<User>>(user);
                return userList;
            }
            return new List<User>();
        }
        public void WriteUsers(List<User> userList)
        {
            string newUser = JsonSerializer.Serialize(userList);
            if (!File.Exists(_userFile))
                File.Create(_userFile);
            File.WriteAllTextAsync(_userFile, newUser).Wait();
        }
        public void WriteUserPhones(List<UserPhones> userPhones)
        {
            var newUserPhones = JsonSerializer.Serialize(userPhones);
            if (!File.Exists(_userPhonesFile))
                File.Create(_userPhonesFile);
            File.WriteAllTextAsync(_userPhonesFile,newUserPhones).Wait();
        }

        public void WritePhoneTypesVM(List<PhoneTypesVM> vm)
        {
            string newnphonetype = JsonSerializer.Serialize(vm);
            if (!File.Exists(_phoneTypesFile))
                File.Create(_phoneTypesFile);
            File.WriteAllTextAsync(_phoneTypesFile, newnphonetype).Wait();
        }
    }
}
