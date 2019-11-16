using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Guide.Modules
{
    [RequireUserPermission(GuildPermission.Administrator)]
    [RequireBotPermission(GuildPermission.ManageNicknames)]
    public class Admin : ModuleBase<SocketCommandContext>
    {
        [Command("scramble")] 
        public async Task ScrambleNickname(SocketGuildUser user)
        {
            if (user == user.Guild.Owner) return;

            var randomName = File.ReadAllLines(Constants.NamesFile).GetRandomElement();

            await user.ModifyAsync(u => u.Nickname = randomName);
            await ReplyAsync($"OK, {user.Mention}.");
        }

        [Command("scramble")]
        public async Task ScrambleNickname(SocketRole role)
        {
            var usersWithRole = Context.Guild.Users.Where(u => u.Roles.Contains(role));

            foreach (var user in usersWithRole)
            {
                await ScrambleNickname(user);
            }
        }
    }
}
