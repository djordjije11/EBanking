using EBanking.BusinessLayer.Interfaces;
using EBanking.API.DTO.UserDtos;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EBanking.API.DTO.AccountDtos;

namespace EBanking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic UserLogic;
        private readonly IMapper Mapper;
        public UserController(IUserLogic userLogic, IMapper mapper)
        {
            UserLogic = userLogic;
            Mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetUserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var users = await UserLogic.GetAllUsersAsync();
                if (!ModelState.IsValid)
                    return BadRequest();
                return Ok(Mapper.Map<IEnumerable<GetUserDto>>(users));
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var user = await UserLogic.FindUserAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest();
                return Ok(Mapper.Map<GetUserDto>(user));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}/Accounts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAccountDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountsAsync(int id)
        {
            try
            {
                var accounts = await UserLogic.GetAccountsByUserAsync(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Mapper.Map<IEnumerable<GetAccountDto>>(accounts));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetUserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddUserDto user)
        {
            if (user == null)
                return BadRequest(ModelState);
            try
            {
                var createdUser = await UserLogic.AddUserAsync(user.FirstName, user.LastName, user.Email, user.Password);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Created(new Uri(Request.GetEncodedUrl() + "/" + createdUser.Id), Mapper.Map<GetUserDto>(createdUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id, [FromBody] DeleteUserDto user)
        {
            if (user == null)
                return BadRequest();
            try
            {
                var deletedUser = await UserLogic.DeleteUserAsync(id, user.Password);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Mapper.Map<GetUserDto>(deletedUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto user)
        {
            if (user == null)
                return BadRequest();
            try
            {
                var updatedUser = await UserLogic.UpdateUserAsync(id, user.Email, user.OldPassword, user.NewPassword);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Mapper.Map<GetUserDto>(updatedUser));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
