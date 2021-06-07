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
        public   EMusicDbContext _db { get; set; }
        private readonly ApplicationDbContext _userdb;
        public NotifHub(ApplicationDbContext userdb, EMusicDbContext db)
        {
            _db = db;
            _userdb= userdb;
            
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
