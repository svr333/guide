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
        public async Task AcceptRules()
        {
            var user = Context.User as SocketGuildUser;
            var memberRole = Context.Guild.GetRole(Constants.MemberRoleId);

            await user.AddRoleAsync(memberRole);

            var embed = new EmbedBuilder()
                .WithTitle(string.Format(lang.GetPhrase(Constants.PKUserJoinedTitle), user.Nickname ?? user.Username))
                .WithColor(Constants.PrimaryColor)
                .Build();

            var general = Context.Guild.GetTextChannel(Constants.GeneralId);

            await general.SendMessageAsync(user.Mention, embed: embed);
        }
    }
}
