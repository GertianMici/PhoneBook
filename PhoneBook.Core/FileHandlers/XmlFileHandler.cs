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
using PhoneBook.Core.Static;
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
        public async Task<List<PhoneTypes>> GetPhoneTypes()
        {
            var phoneType = await StaticFileAccess.ReadFromFile(_phoneTypesFile);
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

        public async Task<List<PhoneTypesVM>> GetPhoneTypesVM()
        {
            var phoneType = await StaticFileAccess.ReadFromFile(_phoneTypesFile);
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

        public async Task<User> GetSpecificUser(int id)
        {
            var phoneType = await StaticFileAccess.ReadFromFile(_userFile);
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

        public async Task<List<UserPhones>> GetUserPhones()
        {
            var phoneType = await StaticFileAccess.ReadFromFile(_userPhonesFile);
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

        public async Task<List<UserPhones>> GetUserPhonesForUser(int id)
        {
            var phoneType = await StaticFileAccess.ReadFromFile(_userPhonesFile);
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

        public async Task<List<User>> GetUsers()
        {
            var phoneType = await StaticFileAccess.ReadFromFile(_userFile);
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
        public async Task WriteUsers(List<User> userList)
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
            await StaticFileAccess.WriteToFile(_userFile, emp);
        }
        public async Task WriteUserPhones(List<UserPhones> userPhones)
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
            await StaticFileAccess.WriteToFile(_userPhonesFile, emp);
        }
        public async Task WritePhoneTypesVM(List<PhoneTypesVM> vm)
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
            await StaticFileAccess.WriteToFile(_phoneTypesFile, emp);
        }
    }
}
