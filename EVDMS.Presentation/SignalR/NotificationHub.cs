using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace EVDMS.Presentation.SignalR;

public class NotificationHub : Hub
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub( ILogger<NotificationHub> logger )
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var dealerId = Context.User?.FindFirstValue( "DealerId" ) ?? "0";

        if( !string.IsNullOrEmpty( dealerId.ToString() ) )
        {
            if( dealerId.Equals( "0" ) )
                _logger.LogInformation( "Admin login" );
            else
                _logger.LogInformation( "User login" );
            await Groups.AddToGroupAsync( Context.ConnectionId, dealerId.ToString() );
        }

        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync( Exception? exception )
    {
        // Lấy DealerId từ claim của người dùng
        var dealerId = Context.User?.FindFirstValue( "DealerId" ) ?? "0";

        // Nếu có, xóa kết nối này khỏi group để dọn dẹp
        if( !string.IsNullOrEmpty( dealerId.ToString() ) )
        {
            if( dealerId.Equals( "0" ) )
                _logger.LogInformation( "Admin logout" );
            else
                _logger.LogInformation( "User logout" );
            await Groups.RemoveFromGroupAsync( Context.ConnectionId, dealerId.ToString() );
        }

        await base.OnDisconnectedAsync( exception );
    }
}