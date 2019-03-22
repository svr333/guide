using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Guide.Language;
using Guide.Preconditions;

namespace Guide.Modules
{
    [RequireChannelId(Constants.WaitingRoomId)]
    public class WaitingRoom : ModuleBase<SocketCommandContext>
    {
        private readonly ILanguage lang;

        public WaitingRoom(ILanguage lang)
        {
            this.lang = lang;
        }

        [Command("I accept the rules", RunMode = RunMode.Async)]
        [Alias("I accept the rules.")]
        public async Task AcceptRules([Remainder]string bio = "The user didn't provide this information.")
        {
            if (!(Context.User is SocketGuildUser user) || UserIsMember(user))
            {
                return;
            }

            if(!IsAsciiPrintable(user.Username[0]))
            {
                await WarnNonAsciiName();
                return;
            }

            var memberRole = Context.Guild.GetRole(Constants.MemberRoleId);

            await user.AddRoleAsync(memberRole);

            var embed = new EmbedBuilder()
                .WithTitle(string.Format(lang.GetPhrase(Constants.PKUserJoinedTitle), user.Nickname ?? user.Username))
                .WithColor(Constants.PrimaryColor)
                .AddField("About this user", bio)
                .Build();

            var general = Context.Guild.GetTextChannel(Constants.GeneralId);

            await general.SendMessageAsync("", embed: embed);
        }

        private static bool UserIsMember(SocketGuildUser user)
            => user.Roles.Any(r => r.Id == Constants.MemberRoleId);

        private static bool IsAsciiPrintable(char ch) {
            return ch >= 32 && ch < 127;
        }

        private async Task WarnNonAsciiName()
        {
            await ReplyAsync("Sorry, before I let you in, you need to make sure your username/nickname is easy to mention on a US-EN keyboard.\nIf you think this is a mistake, please contact a Staff member.");
        }
    }
}

