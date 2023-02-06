using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMusicAPI.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace EMusicAPI.Hub
{
    public class NotifHub: Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly AppDbContext _db;
        public NotifHub( AppDbContext db)
        {
            _db = db;
            
        }

        public override Task OnConnectedAsync()
        {

            base.OnConnectedAsync();
            return Task.CompletedTask;
        }

        public async Task Notification(string content)
        {

            await Clients.All.SendAsync("notifReceiver", "");

        }


    }
}
