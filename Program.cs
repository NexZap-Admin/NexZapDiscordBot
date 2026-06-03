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

        string? token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("DISCORD_TOKEN not found!");
            return;
        }

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