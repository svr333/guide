using System.Linq;
using Discord.WebSocket;

namespace Guide
{
    public static class GuildUserExtensions
    {
        public static bool IsAdmin(this SocketGuildUser user)
        {
            return user.Roles.Any(r => r.Id == 412683783872184331);
        }
    }
}
