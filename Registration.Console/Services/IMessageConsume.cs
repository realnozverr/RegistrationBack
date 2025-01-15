using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.ConsoleConsumer.Services
{
    public interface IMessageConsume
    {
        Task Consume();
    }
}
