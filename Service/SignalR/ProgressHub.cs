using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInstall.Service.SignalR
{
    public class ProgressHub : Hub
    {
        public async Task UpdateProgress(int progress, int total)
        {
            await Clients.All.SendAsync("UpdateProgress", progress, total);
        }

        public async Task OperationCompleted()
        {
            await Clients.All.SendAsync("OperationCompleted");
        }
    }
}
