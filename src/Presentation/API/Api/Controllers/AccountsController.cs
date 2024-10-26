using Application.Features.Accounts.Command.CheckUserNameOrEmailExist;
using Application.Features.Accounts.Command.Login;
using Application.Features.Accounts.Command.Register;
using Application.Features.Accounts.Command.UpdateCurrentMember;
using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Features.Accounts.Queries.GetCurrentUser;
using Application.Features.Accounts.Queries.GetUserByUserId;
using Application.Features.Accounts.Queries.GetUserByUserName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        private readonly IMediator mediator;

        public AccountsController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var command = new LoginCommand(loginDto);
                    var response = await mediator.Send(command);

                    if (response.IsSuccess)
                    {
                        return Ok(response.Data);
                    }

                    if (response.IsSuccess == false && response.Message == "unAuthorized")
                    {
                        return Unauthorized();
                    }

                    if (response.IsSuccess == false && response.Message == "notFound")
                    {
                        return NotFound();
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Take Data From Body
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>
        /// Return Token-userName-Email
        /// </returns>
        /// <remarks>
        /// Rolss :[1=Admin,2=Moderator ,3=Member]
        /// 
        /// </remarks>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = new RegisterCommand(registerDto);
                    var response = await mediator.Send(command);
                    if (response.IsSuccess)
                    {
                        return Ok(response.Data);
                    }

                    if (response.IsSuccess == false)
                    {
                        return BadRequest(response.Erorrs);
                    }


                    return BadRequest(response.Message);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("get-current-user")]
        public async Task<ActionResult<UserReturnDto>> GetCurrentUser(CancellationToken ct)
        {
            try
            {
                var user = await mediator.Send(new GetCurrentUserQuery(), ct);

                if (user is not null)
                {
                    return Ok(user);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("check-userName-or-email-exist/{searchTerm}")]
        public async Task<ActionResult<bool>> CheckUserNameExist(string searchTerm, CancellationToken ct)
        {
            try
            {
                var result = await mediator.Send(new CheckUserNameOrEmailExistQuery(searchTerm), ct);

                if (result)
                    return Ok(result);
                return NotFound(false);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult> GetAllUsers(CancellationToken ct)
        {
            try
            {
                var users = await mediator.Send(new GetAllUsersQuery(), ct);

                if (users is not null)
                {
                    return Ok(users);
                }
                return NotFound();

            }
            catch (Exception ex)
            {

                return BadRequest($"{ex.Message}");
            }


        }

        [HttpGet("get-all-userName/{userName}")]
        public async Task<ActionResult<MemberDto>> GetUserByUserName(string userName, CancellationToken ct)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    var user = await mediator.Send(new GetUserByUserNameQuery(userName), ct);
                    if (user is not null) return Ok(user);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpGet("get-all-userId/{userId}")]
        public async Task<ActionResult<MemberDto>> GetUserByUserId(string userId, CancellationToken ct)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await mediator.Send(new GetUserByUserIdQuery(userId), ct);
                    if (user is not null) return Ok(user);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-current-member")]
        public async Task<ActionResult<UpdateCurrentMemberDto>> UpdateCurrentMember([FromBody] UpdateCurrentMemberDto updateCurrentMemberDto)
        {
            try
            {
                var command = new UpdateCurrentMemberCommand(updateCurrentMemberDto);
                var response=await mediator.Send(command);
                if (response.IsSuccess) 
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Erorrs);

            }
            catch (Exception)
            {

                throw;
            }
        }
    
    
    }
}