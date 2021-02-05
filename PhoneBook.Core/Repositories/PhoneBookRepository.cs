using PhoneBook.Core.Interfaces;
using PhoneBook.Models.Models;
using PhoneBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PhoneBook.Core.Constant;
using PhoneBook.Core.FileHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.Core.Repositories
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public IList<IFileHandler> FileHandlers { get; set; } = new List<IFileHandler>();
        public PhoneBookRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            FindFileHandlers();
        }
        private void FindFileHandlers()
        {
            //search
            var handlers = typeof(PhoneBookRepository).Assembly
                .GetTypes()
                .Where(type => type.IsClass
                            && !type.IsAbstract
                            && type.IsPublic
                            && type.GetInterfaces().Any(i => i == typeof(IFileHandler)))
                .ToList();

            //instances
            foreach (var handler in handlers)
                FileHandlers.Add(ActivatorUtilities.CreateInstance(_serviceProvider, handler) as IFileHandler);

            //ordering
            FileHandlers = FileHandlers.OrderBy(handler => handler.FileType).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User where phone was added</returns>
        public GetPhoneBook AddPhoneNumber(PhoneBookVM vm, int fileType)
        {
            try
            {
                var user = PhoneBookVMToUser(vm);
                User userGet = FileHandlers[fileType].GetSpecificUser(user.Id);
                if (userGet == null)
                {
                    return null;
                }
                List<UserPhones> userPhones = FileHandlers[fileType].GetUserPhones();
                int userphoneid = userPhones.Count + 1;
                var userPhonesAdd = user.UserPhones?.AsEnumerable().Select(x => new UserPhones
                {
                    Id = userphoneid++,
                    Number = x.Number,
                    PhoneTypeId = x.PhoneTypeId,
                    UserId = userGet.Id
                }).ToList();
                userPhones.AddRange(userPhonesAdd);
                FileHandlers[fileType].WriteUserPhones(userPhones);
                return GetUser(user.Id, fileType);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        /// Delete user with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if the user is deleted, false otherwise</returns>
        public bool DeleteUser(int id, int fileType)
        {
            try
            {
                List<UserPhones> userPhones = FileHandlers[fileType].GetUserPhones();
                userPhones = userPhones.Where(x => x.UserId != id).ToList();
                FileHandlers[fileType].WriteUserPhones(userPhones);


                List<User> userList = FileHandlers[fileType].GetUsers();
                userList = userList.Where(x => x.Id != id).ToList();
                FileHandlers[fileType].WriteUsers(userList);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<PhoneTypesVM> GetPhoneTypes(int fileType)
        {
            try
            {              
                List<PhoneTypesVM> phoneTypesList = FileHandlers[fileType].GetPhoneTypesVM();
                return phoneTypesList;
            }
            catch (Exception)
            {

                return Enumerable.Empty<PhoneTypesVM>();
            }
        }

        public GetPhoneBook GetUser(int id, int fileType)
        {
            try
            {
                User user = FileHandlers[fileType].GetSpecificUser(id);
                List<UserPhones> userPhones = FileHandlers[fileType].GetUserPhonesForUser(id);
                List<PhoneTypes> phoneTypesList = FileHandlers[fileType].GetPhoneTypes();



                userPhones.ForEach(x => x.PhoneType = phoneTypesList.FirstOrDefault(pt => pt.Id == x.PhoneTypeId));
                user.UserPhones = userPhones;
                return UserToPhoneBookVM(user);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        private IEnumerable<User> GetAllUsers(int fileType)
        {
            try
            {
                List<User> userList = FileHandlers[fileType].GetUsers();
                List<UserPhones> userPhones = FileHandlers[fileType].GetUserPhones();
                List<PhoneTypes> phoneTypesList = FileHandlers[fileType].GetPhoneTypes();



                userPhones.ForEach(x => x.PhoneType = phoneTypesList.FirstOrDefault(pt => pt.Id == x.PhoneTypeId));
                userList.ForEach(x => x.UserPhones = userPhones.Where(up => up.UserId == x.Id));
                return userList;
            }
            catch (Exception ex)
            {

                return new List<User>();
            }
        }

        public IEnumerable<GetPhoneBook> GetUsersOrdered(bool firstnameOrder, bool ascOrder, int fileType)
        {
            var user = GetAllUsers(fileType);
            var users = user.Select(x => new GetPhoneBook
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumbers = x.UserPhones.Select(y => new GetPhoneNumberVM
                {
                    Id = y.Id,
                    Number = y?.Number ?? string.Empty,
                    PhoneType = y?.PhoneType?.Name ?? string.Empty
                }).ToList()
            }).AsEnumerable();
            if (ascOrder)
            {
                if (firstnameOrder)
                {
                    users = users.OrderBy(x => x.FirstName);
                }
                else
                {
                    users = users.OrderBy(x => x.LastName);
                }
            }
            else
            {
                if (firstnameOrder)
                {
                    users = users.OrderByDescending(x => x.FirstName);
                }
                else
                {
                    users = users.OrderByDescending(x => x.LastName);
                }
            }

            return users;
        }

        public GetPhoneBook PostUser(PhoneBookVM vm, int fileType)
        {
            try
            {
                User user = PhoneBookVMToUser(vm);

                List<User> userList = FileHandlers[fileType].GetUsers();

                int userId = userList.Count + 1;
                userList.Add(new User
                {
                    Id = userId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                });

                FileHandlers[fileType].WriteUsers(userList);


                List<UserPhones> userPhones = FileHandlers[fileType].GetUserPhones();

                int userPhonesId = userPhones.Count + 1;
                var userPhonesAdd = user.UserPhones?.AsEnumerable().Select(x => new UserPhones
                {
                    Id = userPhonesId++,
                    Number = x.Number,
                    PhoneTypeId = x.PhoneTypeId,
                    UserId = userId
                }).ToList();
                userPhones.AddRange(userPhonesAdd);

                FileHandlers[fileType].WriteUserPhones(userPhones);

                return GetUser(userId, fileType);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public GetPhoneBook PutUser(PhoneBookVM vm, int fileType)
        {
            try
            {
                User user = PhoneBookVMToUser(vm);
                List<User> userList = FileHandlers[fileType].GetUsers();


                userList.Where(x => x.Id == user.Id).ToList().ForEach(upd =>
                  {
                      upd.FirstName = user.FirstName;
                      upd.LastName = user.LastName;

                  });


                FileHandlers[fileType].WriteUsers(userList);

                List<UserPhones> userPhones = FileHandlers[fileType].GetUserPhones();

                List<int> userPhonesID = user.UserPhones?.Select(x => x.Id).ToList();
                userPhones.Where(x => userPhonesID.Contains(x.Id)).ToList().ForEach(x =>
                 {
                     x.Number = user.UserPhones.FirstOrDefault(y => y.Id == x.Id)?.Number;
                     x.PhoneTypeId = user.UserPhones.FirstOrDefault(y => y.Id == x.Id)?.PhoneTypeId ?? 1;
                 });

                FileHandlers[fileType].WriteUserPhones(userPhones);

                return GetUser(user.Id, fileType);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        private static GetPhoneBook UserToPhoneBookVM(User user)
        {
            return new GetPhoneBook
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumbers = user.UserPhones.Select(x => new GetPhoneNumberVM
                {
                    Id = x.Id,
                    Number = x?.Number ?? string.Empty,
                    PhoneType = x?.PhoneType?.Name ?? string.Empty
                }).ToList()
            };
        }

        private static User PhoneBookVMToUser(PhoneBookVM phoneBook)
        {
            return new User
            {
                Id = phoneBook.Id,
                FirstName = phoneBook.FirstName,
                LastName = phoneBook.LastName,
                UserPhones = phoneBook.PhoneNumbers.Select(x => new UserPhones
                {
                    Id = x.Id,
                    Number = x.Number,
                    PhoneTypeId = x.PhoneTypeId
                }).ToList(),
            };
        }

        public bool AddPhoneTypesVM(PostPhoneTypesVM vm, int fileType)
        {
            try
            {
                List<PhoneTypesVM> phoneTypesList = FileHandlers[fileType].GetPhoneTypesVM();
                int phonetypeid = phoneTypesList.Count + 1;
                phoneTypesList.Add(new PhoneTypesVM
                {
                    Id = phonetypeid,
                    Name = vm.Name
                });
                FileHandlers[fileType].WritePhoneTypesVM(phoneTypesList);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
