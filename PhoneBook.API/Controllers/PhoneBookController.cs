using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.Models.ViewModels;
using PhoneBook.Core.Interfaces;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookRepository _phoneBookRepository;

        public PhoneBookController(IPhoneBookRepository phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }
        /// <summary>
        /// Get method to get all users
        /// </summary>
        /// <param name="firstnameOrder"></param>
        /// <param name="ascOrder"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        // GET: api/<PhoneBookController>
        [HttpGet]
        public IEnumerable<GetPhoneBook> Get(bool firstnameOrder = true, bool ascOrder = false, int fileType = 0)
        {            
            if (fileType != 1 && fileType != 2 && fileType != 0)
                fileType = 0;
            var result = _phoneBookRepository.GetUsersOrdered(firstnameOrder, ascOrder, fileType);
            return result;
        }

        // GET api/<PhoneBookController>/5
        [HttpGet("{id}")]
        public GetPhoneBook Get(int id, int fileType = 0)
        {
            if (fileType != 1 && fileType != 2 && fileType != 0)
                fileType = 0;
            var result = _phoneBookRepository.GetUser(id, fileType);
            return result;
        }
        // GET api/values
        [HttpGet("GetPhoneTypes")]
        public IEnumerable<PhoneTypesVM> GetPhoneTypes(int fileType = 0)
        {
            if (fileType != 1 && fileType != 2 && fileType != 0)
                fileType = 0;
            var result = _phoneBookRepository.GetPhoneTypes(fileType);
            return result;
        }
        // POST api/<PhoneBookController>
        [HttpPost]
        public GetPhoneBook Post([FromBody] PhoneBookVM user, int fileType = 0)
        {
            if (fileType != 1 && fileType != 2 && fileType != 0)
                fileType = 0;
            var result = _phoneBookRepository.PostUser(user, fileType);
            return result;
        }
        [HttpPost("AddPhoneNumber")]
        public ActionResult<GetPhoneBook> AddPhoneNumber([FromBody] PhoneBookVM phoneBook, int fileType = 0)
        {
            if (fileType != 1 && fileType != 2 && fileType != 0)
                fileType = 0;
            var result = _phoneBookRepository.AddPhoneNumber(phoneBook, fileType);
            if (result == null)
            {
                return new BadRequestObjectResult("Check user data");
            }
            return result;
        }

        // PUT api/<PhoneBookController>/5
        [HttpPut("{id}")]
        public ActionResult<GetPhoneBook> Put(int id, [FromBody] PhoneBookVM user, int fileType = 0)
        {
            if (id != user.Id)
            {
                return new BadRequestObjectResult("Check out data entered");
            }
            if (fileType != 1 && fileType != 2 && fileType != 0)
                fileType = 0;
            var result = _phoneBookRepository.PutUser(user, fileType);

            return result;
        }

        // DELETE api/<PhoneBookController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id, int fileType = 0)
        {
            if (fileType != 1 && fileType != 2 && fileType != 0)
                fileType = 0;
            var result = _phoneBookRepository.DeleteUser(id, fileType);
            return result;
        }
        [HttpPost("AddPhoneTYPES")]
        public bool AddPhoneTypesVM([FromBody] PostPhoneTypesVM vm, int fileType = 0)
        {
            if (fileType != 1 && fileType != 2 && fileType != 0)
                fileType = 0;
            var result = _phoneBookRepository.AddPhoneTypesVM(vm, fileType);
            return result;
        }
    }
}
