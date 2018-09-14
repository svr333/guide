using Discord;
using Discord.WebSocket;

namespace Guide.Connection
{
    public static class DiscordSocketClientFactory
    {
        public static DiscordSocketClient GetDefault()
        {
            return new DiscordSocketClient(new DiscordSocketConfig{
                LogLevel = LogSeverity.Verbose
            });
        }
    }
}
