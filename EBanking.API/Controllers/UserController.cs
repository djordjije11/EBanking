using EBanking.BusinessLayer.Interfaces;
using EBanking.Models;
using EBanking.Models.ModelsDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EBanking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic UserLogic;
        public UserController(IUserLogic userLogic)
        {
            UserLogic = userLogic;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await UserLogic.GetAllUsersAsync();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var user = await UserLogic.FindUserAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}/Accounts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Account>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountsAsync(int id)
        {
            try
            {
                var accounts = await UserLogic.GetAccountsByUserAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(User user)
        {
            if (user == null)
                return BadRequest(ModelState);
            try
            {
                var createdUser = await UserLogic.AddUserAsync(user.FirstName, user.LastName, user.Email, user.Password);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                //return Ok(createdUser);
                return Created(new Uri(Request.GetEncodedUrl() + "/" + createdUser.Id), createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id, [FromBody] UserDto userDto)
        {
            /*
            if (userDto == null)
                return BadRequest();
            */
            try
            {
                var deletedUser = await UserLogic.DeleteUserAsync(id, userDto.Password);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest();
            try
            {
                var updatedUser = await UserLogic.UpdateUserAsync(id, userDto.Email, userDto.OldPassword, userDto.Password);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(updatedUser);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*
        [HttpPut]
        public async Task<IActionResult> Update(int id, string? firstname, string? lastname, string? oldPassword, string? newPassword)
        {
            System.Console.WriteLine(id.ToString(), firstname, lastname, oldPassword, newPassword);
            try
            {
                var updatedUser = await UserLogic.UpdateUserAsync(id, firstname, lastname, oldPassword, newPassword);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        */
    }
}
