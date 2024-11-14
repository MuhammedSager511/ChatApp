using Application.Features.Message.Query.GetAlMessageForUse;
using Application.Helper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presistence.Contracts
{
    public interface IMessageRepository :IGenericRepository<Message>
    {
        //for future
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);   
        Task<PagedList<MessageDto>> GetAlMessageForUser(MessageParams messageParams);  
        Task<IEnumerable<MessageDto>> GetMessageRead(string currentUserName, string recipientUserName);
    }   
}
