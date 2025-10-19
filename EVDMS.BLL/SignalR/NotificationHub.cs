using Microsoft.AspNetCore.SignalR;

namespace EVDMS.BLL.SignalR;

public class NotificationHub : Hub
{
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}