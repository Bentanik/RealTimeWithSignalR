using Microsoft.AspNetCore.SignalR;

namespace SignR.Api;
public sealed class ChatHub : Hub
{
    public async Task TestMe(string x)
    {
        await Clients.All.SendAsync($"Dmm {x}", CancellationToken.None);
    }
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiverMessage" +  $"{Context.ConnectionId} has joined");
        await base.OnConnectedAsync();
    }
}