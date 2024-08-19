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
    }
}
