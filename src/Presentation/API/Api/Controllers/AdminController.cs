using Application.Features.Admin.Commands.UpdateRole;
using Application.Features.Admin.Queries.GetUsersWithRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IMediator mediator;

        public AdminController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }


        [Authorize(Policy ="RequiredAdminRole")]
        [HttpGet("get-users-with-roles")]
        public async Task<ActionResult<UsersWithRoleDto>> GetUsersWithRoles()
        {
            try
            {
                var query = new GetUsersWithRoleQuery();
                var response=await mediator.Send(query);
                if(response != null)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("update-roles/{userName}")]
        public async Task<IActionResult> UpdateRole(string userName, [FromQuery] string roles)
        {
            //Admin ,Member

            var command =new UpdateRoleCommand(userName, roles);
            var response=await mediator.Send(command,CancellationToken.None);
            if(response.IsSuccess==false && response.Message== "badRequst")
            {
                return BadRequest(response.Erorrs);
            }

            if (response.IsSuccess == true)
            {
                return Ok(response.Data);
            }
            return NotFound("this username not found");
        }
    }
}
