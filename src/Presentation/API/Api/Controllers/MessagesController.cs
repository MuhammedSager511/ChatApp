using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Features.Message.Command.AddMessage;
using Application.Features.Message.Command.DeleteMessage;
using Application.Features.Message.Command.GetMessageRead;
using Application.Features.Message.Query.GetAllMessages;
using Application.Features.Message.Query.GetAlMessageForUse;
using Application.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController
    {
        private readonly IMediator mediator;

        public MessagesController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost("add-message")]
        public async Task<IActionResult> AddMessage([FromBody] AddMessageDto addMessageDto, CancellationToken ct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = new AddMessageCommand(addMessageDto);
                    var response = await mediator.Send(command,ct);
                    return response.IsSuccess ? Ok(response.Data) : BadRequest(response.Message);
                }
                return BadRequest("erorr While Adding New Message , ModelState in valid");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        [HttpGet("get-message-for-user")]
        public async Task<ActionResult<MessageDto>> GetMessageForUser([FromQuery] MessageParams messageParams,CancellationToken ct)
        {
            var messages = await mediator.Send(new GetAlMessageForUserQuery(messageParams),ct);
            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount,messages.TotalPages);
            if (messages is not null)
            {
                return Ok(messages);
            }
            return NotFound();
        }


        [HttpGet("get-message-read/{userName}")]
        public async Task<ActionResult<List<MessageDto>>> GetMessageRead(string userName, CancellationToken ct)
        {
            try
            {
                var response = await mediator.Send(new GetMessageReadCommand(userName));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }


        [HttpDelete("delete-message/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                var command = new DeleteMessageCommand(id);
                var response=await mediator.Send(command,CancellationToken.None);
                if (response.IsSuccess == false && response.Message == "Unauthorized")
                {
                    return Unauthorized();
                }
                if (response.IsSuccess && response.Message == "ok")
                {
                    return Ok();
                }
                return NotFound($"this message id `{id}` not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
