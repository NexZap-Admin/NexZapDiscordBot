using Discord;
using Discord.WebSocket;

class Program
{
    private DiscordSocketClient _client;

    static Task Main()
        => new Program().MainAsync();

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents =
                GatewayIntents.Guilds |
                GatewayIntents.GuildMembers
        });

        _client.Log += Log;
        _client.UserJoined += UserJoined;

        string token = "MTUxMTYyODU0ODYyMjc3ODU5OQ.Gd_XIk.pBMsHTXT05un4sX3jtxzfYQANp5eLdtfohXSKM";

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private async Task UserJoined(SocketGuildUser user)
    {
        var channel = user.Guild.TextChannels
            .FirstOrDefault(x => x.Name == "welcome");

        if (channel != null)
        {
            await channel.SendMessageAsync(
                 $"🎉 Chào mừng bạn {user.Username} đã gia nhập gia đình NexZap."
            );
        }
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }
}