using Application.Features.Likes.Commands.AddLike;
using Application.Helper;
using Application.Presistence.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Likes.Queries.GetUserLike
{
    public class GetUserLikeQuery:IRequest<PagedList<likeDto>>
    {
        public GetUserLikeQuery(LikeParams likeParams)
        {
            LikeParams = likeParams;
        }

        public LikeParams LikeParams { get; set; }


        class Handler : IRequestHandler<GetUserLikeQuery, PagedList<likeDto>>
        {
            private readonly ILikeRepository likeRepository;
            private readonly IHttpContextAccessor httpContextAccessor;

            public Handler(ILikeRepository likeRepository,IHttpContextAccessor httpContextAccessor)
            {
                this.likeRepository = likeRepository;
                this.httpContextAccessor = httpContextAccessor;
            }
            public async Task<PagedList<likeDto>> Handle(GetUserLikeQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    request.LikeParams.UserId = httpContextAccessor?
                        .HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    var user = await likeRepository.GetUserLikes(request.LikeParams);
                    if (user is not null)
                    {
                        return user;
                    }
                    return null;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
