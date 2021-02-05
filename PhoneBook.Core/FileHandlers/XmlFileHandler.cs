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
using System.Xml;
using System.Xml.Serialization;

namespace PhoneBook.Core.FileHandlers
{
    public class XmlFileHandler : IFileHandler
    {
        private readonly string _userFile;
        private readonly string _phoneTypesFile;
        private readonly string _userPhonesFile;
        public uint FileType => 1;
        public XmlFileHandler()
        {
            _userFile = Path.Combine(Constants.DatabasePath, "userFile.xml");
            _phoneTypesFile = Path.Combine(Constants.DatabasePath, "phoneTypesFile.xml");
            _userPhonesFile = Path.Combine(Constants.DatabasePath, "userPhonesFile.xml");
        }
        public List<PhoneTypes> GetPhoneTypes()
        {
            if (File.Exists(_phoneTypesFile))
            {
                var phoneType = File.ReadAllTextAsync(_phoneTypesFile).Result;
                if (string.IsNullOrEmpty(phoneType))
                    return new List<PhoneTypes>();
                List<PhoneTypes> emp;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PhoneTypes>));
                using (StringReader textReader = new StringReader(phoneType))
                {
                    emp = (List<PhoneTypes>)xmlSerializer.Deserialize(textReader);
                }
                return emp;
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
                List<PhoneTypesVM> emp;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PhoneTypesVM>));
                using (StringReader textReader = new StringReader(phoneType))
                {
                    emp = (List<PhoneTypesVM>)xmlSerializer.Deserialize(textReader);
                }
                return emp;
            }
            return new List<PhoneTypesVM>();
        }

        public User GetSpecificUser(int id)
        {
            if (File.Exists(_userFile))
            {
                var phoneType = File.ReadAllTextAsync(_userFile).Result;
                if (string.IsNullOrEmpty(phoneType))
                    return null;
                List<User> emp;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
                using (StringReader textReader = new StringReader(phoneType))
                {
                    emp = (List<User>)xmlSerializer.Deserialize(textReader);
                }
                return emp.FirstOrDefault(x => x.Id == id);
            }
            return null;
        }

        public List<UserPhones> GetUserPhones()
        {
            if (File.Exists(_userPhonesFile))
            {
                var phoneType = File.ReadAllTextAsync(_userPhonesFile).Result;
                if (string.IsNullOrEmpty(phoneType))
                    return new List<UserPhones>();
                List<UserPhones> emp;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<UserPhones>));
                using (StringReader textReader = new StringReader(phoneType))
                {
                    emp = (List<UserPhones>)xmlSerializer.Deserialize(textReader);
                }
                return emp;
            }
            return new List<UserPhones>();
        }

        public List<UserPhones> GetUserPhonesForUser(int id)
        {
            if (File.Exists(_userPhonesFile))
            {
                var phoneType = File.ReadAllTextAsync(_userPhonesFile).Result;
                if (string.IsNullOrEmpty(phoneType))
                    return new List<UserPhones>();
                List<UserPhones> emp;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<UserPhones>));
                using (StringReader textReader = new StringReader(phoneType))
                {
                    emp = (List<UserPhones>)xmlSerializer.Deserialize(textReader);
                }
                return emp.Where(x => x.UserId == id).ToList();
            }
            return new List<UserPhones>();
        }

        public List<User> GetUsers()
        {
            if (File.Exists(_userFile))
            {
                var phoneType = File.ReadAllTextAsync(_userFile).Result;
                if (string.IsNullOrEmpty(phoneType))
                    return new List<User>();
                List<User> emp;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
                using (StringReader textReader = new StringReader(phoneType))
                {
                    emp = (List<User>)xmlSerializer.Deserialize(textReader);
                }
                return emp;
            }
            return new List<User>();
        }
        public void WriteUsers(List<User> userList)
        {
            string emp;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(textWriter))
                {
                    xmlSerializer.Serialize(writer, userList);
                    emp = textWriter.ToString();
                }
            }
            File.WriteAllTextAsync(_userFile, emp).Wait();
        }
        public void WriteUserPhones(List<UserPhones> userPhones)
        {
            string emp;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<UserPhones>));
            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(textWriter))
                {
                    xmlSerializer.Serialize(writer, userPhones);
                    emp = textWriter.ToString();
                }
            }
            File.WriteAllTextAsync(_userPhonesFile, emp).Wait();
        }
        public void WritePhoneTypesVM(List<PhoneTypesVM> vm)
        {
            string emp;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PhoneTypesVM>));
            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(textWriter))
                {
                    xmlSerializer.Serialize(writer, vm);
                    emp = textWriter.ToString();
                }
            }
            File.WriteAllTextAsync(_phoneTypesFile, emp).Wait();
        }
    }
}
