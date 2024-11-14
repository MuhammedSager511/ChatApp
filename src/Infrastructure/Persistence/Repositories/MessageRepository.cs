using Application.Features.Likes.Commands.AddLike;
using Application.Features.Message.Query.GetAlMessageForUse;
using Application.Helper;
using Application.Presistence.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class MessageRepository : GenericRepository<Domain.Entities.Message>, IMessageRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MessageRepository(ApplicationDbContext context,IMapper mapper) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void DeleteMessage(Message message)
        {
            context.Messages.Remove(message);
            context.SaveChanges();
        }
        public async Task<Message> GetMessage(int id)
        =>await context.Messages.Include(x=>x.Sender).Include(x=>x.Recipient)
                .FirstOrDefaultAsync(x=>x.Id == id);
        

        public async Task<PagedList<MessageDto>> GetAlMessageForUser(MessageParams messageParams)
        {
           var query=context.Messages.OrderByDescending(z=>z.MessageSend).AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(x => x.Recipient.UserName == messageParams.UserName
                && x.RecipientDeleted==false
                ),
                "Outbox" => query.Where(x => x.Sender.UserName == messageParams.UserName
                 && x.SenderDeleted == false),
                _ => query.Where(x => x.Recipient.UserName == messageParams.UserName 
                                && x.RecipientDeleted == false && x.DateRead == null),
            };
            var message=query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);

                 return await PagedList<MessageDto>.CreateAsync(message, messageParams.PageNumber, messageParams.PageSize);
        }

       

        public async Task<IEnumerable<MessageDto>> GetMessageRead(string currentUserName, string recipientUserName)
        {
            var message = context.Messages
                .Include(x => x.Sender).ThenInclude(x => x.Photos)
                .Include(x => x.Recipient).ThenInclude(x => x.Photos)
                .Where(
                x => x.Recipient.UserName == currentUserName
                && x.RecipientDeleted == false
                && 
                x.Sender.UserName == recipientUserName
                ||
                x.Recipient.UserName == recipientUserName 
                &&
                x.Sender.UserName == currentUserName && x.SenderDeleted == false)
                .OrderByDescending(x=>x.MessageSend)
                .ToList();

            var unReadMessage=message.Where(
                                      x=>x.DateRead == null && 
                                      x.Recipient.UserName==currentUserName).ToList();

            if(unReadMessage.Any())
            {
                foreach (var item in unReadMessage)
                {
                    item.DateRead = DateTime.Now;
                    context.Messages.Update(item); 
                    context.SaveChanges();
                }
            }
            return  mapper.Map<IEnumerable<MessageDto>>(message);
        }
    }
}
