using Application.Features.Accounts.Command.CheckUserNameOrEmailExist;
using Application.Features.Accounts.Command.Login;
using Application.Features.Accounts.Command.Register;
using Application.Features.Accounts.Command.RemovePhoto;
using Application.Features.Accounts.Command.SetMainPhoto;
using Application.Features.Accounts.Command.UpdateCurrentMember;
using Application.Features.Accounts.Command.UploadPhoto;
using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Features.Accounts.Queries.GetCurrentUser;
using Application.Features.Accounts.Queries.GetUserByUserId;
using Application.Features.Accounts.Queries.GetUserByUserName;
using Application.Helper;
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
        public async Task<ActionResult<RegisterDto>> Register([FromBody] RegisterDto registerDto)
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
        public async Task<ActionResult> GetAllUsers([FromQuery] UserParams userParams,CancellationToken ct)
        {
            try
            {
               
                

                var users = await mediator.Send(new GetAllUsersQuery(userParams), ct);
                if (users is not null)
                {
                    Response.AddPaginationHeader(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages);
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


        [HttpPost("upload-photo")]
        public async Task<ActionResult<PhotoDto>> UploadPhoto( IFormFile file)
        {
            try
            {
                var command = new UploadPhotoCommand {PhotoFile= file };
                var response=await mediator.Send(command);
                if (response is not null)
                    return Ok(response);
                return BadRequest("Unable to upload photo");
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Unable to upload photo {ex.Message}");

                
            }
        }

        [HttpDelete("deleted-photo/{id}")]
        public async Task<IActionResult> RemovePhoto(int id)
        {
            try
            {
                var command = new RemovePhotoCommand(id);
                var response = await mediator.Send(command);
                if (response)
                    return Ok("Remove Photo Successfully");
                return BadRequest("Unable to Remove photo");

            }
            catch (Exception ex)
            {
                return BadRequest($"Unable to Remove photo {ex.Message}");


            }
        }



        [HttpPut("set-main-photo/{id}")]
        public async Task<IActionResult> SetMainPhoto(int id)
        {
            try
            {
                if (id>0)
                {
                    var command=new SetMainPhotoCommand(id);
                    var response=await mediator.Send(command);
                    if (response)
                        return Ok("assign successfully");

                }
                return NotFound($"This id {id} doesn't fount");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}