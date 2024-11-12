using Application.Features.Likes.Commands.AddLike;
using Application.Features.Likes.Queries.GetUserLike;
using Application.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
   
    public class LikesController : BaseController
    {
        private readonly IMediator mediator;

        public LikesController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("add-like/{userName}")]
        public async Task<IActionResult> AddLike(string userName,CancellationToken ct)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    var command=new AddLikeCommand(userName);
                    var response=await mediator.Send(command);
                    if (response.IsSuccess)
                    {
                       return Ok(response);
                    }
                    else
                    {
                        return BadRequest(response.Message);
                    }
                }
                return NotFound("userName not fount");
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-user-like")]
        public async Task<ActionResult<IEnumerable<likeDto>>> GetUserLike([FromQuery]LikeParams likeParams, CancellationToken ct)
        {
            try
            {
                var likes=await mediator.Send(new GetUserLikeQuery(likeParams),ct);
                if(likes != null)
                {
                    Response.AddPaginationHeader(likes.CurrentPage, likes.PageSize, likes.TotalCount, likes.TotalPages);
                    return Ok(likes);
                }
                return NotFound();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
