using Application.Features.Message.Command.AddMessage;
using Application.Features.Message.Query.GetAllMessages;
using MediatR;
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
        [HttpGet]
        public async Task<ActionResult<MessageReturnDto>> Get(CancellationToken ct)
        {
            var messages = await mediator.Send(new GetAlMessageQuery(),ct);
            return Ok(messages);
        }

        [HttpPost]  
        public async Task<ActionResult<MessageReturnDto>> Posr([FromBody] AddMessageDto addMessageDto, CancellationToken ct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command=new AddMessageCommand(addMessageDto);   
                    var response=await mediator.Send(command);  
                    return response.IsSuccess? Ok(response) : BadRequest(response.Message);
                }
                return BadRequest("erorr While Adding New Message , ModelState in valid");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
