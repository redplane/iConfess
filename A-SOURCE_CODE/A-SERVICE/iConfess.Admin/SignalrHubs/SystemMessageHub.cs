using iConfess.Admin.Attributes;
using iConfess.Database.Enumerations;
using iConfess.Database.Models.Tables;
using Microsoft.AspNet.SignalR.Hubs;

namespace iConfess.Admin.SignalrHubs
{
    [SignalrAuthorize(AccountRole.Admin)]
    [HubName("SystemMessage")]
    public class SystemMessageHub : ParentHub
    {
    }
}