﻿using System.Threading.Tasks;
using System.Web.Http;
using SystemDatabase.Enumerations;
using Administration.Attributes;
using Microsoft.AspNet.SignalR.Hubs;

namespace Administration.SignalrHubs
{
    [SignalrAuthorize(Roles.Admin)]
    [HubName("SystemMessage")]
    public class SystemMessageHub : ParentHub
    {
        /// <summary>
        /// Callback when client disconnects from hub.
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}