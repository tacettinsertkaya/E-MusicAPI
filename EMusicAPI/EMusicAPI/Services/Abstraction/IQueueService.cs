using EMusicAPI.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Services.Abstraction
{
    public interface IQueueService
    {
       void SendNotifQueue(Notif notif);
       void GetNotifQueue();

    }
}
