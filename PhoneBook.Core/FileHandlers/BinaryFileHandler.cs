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
    public class BinaryFileHandler : IFileHandler
    {
        private readonly string _userFile;
        private readonly string _phoneTypesFile;
        private readonly string _userPhonesFile;
        public BinaryFileHandler()
        {
            _userFile = Path.Combine(Constants.DatabasePath, "userFile.bin");
            _phoneTypesFile = Path.Combine(Constants.DatabasePath, "phoneTypesFile.bin");
            _userPhonesFile = Path.Combine(Constants.DatabasePath, "userPhonesFile.bin");
        }
        public uint FileType => 2;

        public List<PhoneTypes> GetPhoneTypes()
        {
            string phoneType;
            if (File.Exists(_phoneTypesFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(_phoneTypesFile, FileMode.Open)))
                {
                    phoneType = reader.ReadString();
                }
                List<PhoneTypes> phoneTypesList = JsonSerializer.Deserialize<List<PhoneTypes>>(phoneType);
                return phoneTypesList;
            }
            return new List<PhoneTypes>();
        }

        public List<PhoneTypesVM> GetPhoneTypesVM()
        {
            string phoneType;
            if (File.Exists(_phoneTypesFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(_phoneTypesFile, FileMode.Open)))
                {
                    phoneType = reader.ReadString();
                }
                List<PhoneTypesVM> phoneTypesList = JsonSerializer.Deserialize<List<PhoneTypesVM>>(phoneType);
                return phoneTypesList;
            }
            return new List<PhoneTypesVM>();
        }

        public User GetSpecificUser(int id)
        {
            string userjson;
            if (File.Exists(_userFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(_userFile, FileMode.Open)))
                {
                    userjson = reader.ReadString();
                }
                User user = JsonSerializer.Deserialize<List<User>>(userjson).FirstOrDefault(x => x.Id == id);
                return user;
            }
            return null;
        }

        public List<UserPhones> GetUserPhones()
        {
            string userPhone;
            if (File.Exists(_userPhonesFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(_userPhonesFile, FileMode.Open)))
                {
                    userPhone = reader.ReadString();
                }
                List<UserPhones> userPhones = JsonSerializer.Deserialize<List<UserPhones>>(userPhone);
                return userPhones;
            }
            return new List<UserPhones>();
        }

        public List<UserPhones> GetUserPhonesForUser(int id)
        {
            string userPhone;
            if (File.Exists(_userPhonesFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(_userPhonesFile, FileMode.Open)))
                {
                    userPhone = reader.ReadString();
                }
                List<UserPhones> userPhones = JsonSerializer.Deserialize<List<UserPhones>>(userPhone).Where(x => x.UserId == id).ToList();
                return userPhones;
            }
            return new List<UserPhones>();
        }

        public List<User> GetUsers()
        {
            string userjson;
            if (File.Exists(_userFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(_userFile, FileMode.Open)))
                {
                    userjson = reader.ReadString();
                }
                List<User> userList = JsonSerializer.Deserialize<List<User>>(userjson);
                return userList;
            }
            return new List<User>();
        }
        public void WriteUsers(List<User> userList)
        {
            string newUser = JsonSerializer.Serialize(userList);
            using BinaryWriter writer = new BinaryWriter(File.Open(_userFile, FileMode.OpenOrCreate));
            writer.Write(newUser);

        }
        public void WriteUserPhones(List<UserPhones> userPhones)
        {
            var newUserPhones = JsonSerializer.Serialize(userPhones);
            using BinaryWriter writer = new BinaryWriter(File.Open(_userPhonesFile, FileMode.OpenOrCreate));
            writer.Write(newUserPhones);

        }

        public void WritePhoneTypesVM(List<PhoneTypesVM> vm)
        {
            string phonetypes = JsonSerializer.Serialize(vm);
            using BinaryWriter writer = new BinaryWriter(File.Open(_phoneTypesFile, FileMode.OpenOrCreate));
            writer.Write(phonetypes);
        }
    }
}
