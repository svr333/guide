using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Linq;
using Guide.Language;

namespace Guide.Modules
{
    public class Helpdesk : ModuleBase<SocketCommandContext>
    {
        private readonly ILanguage lang;

        public Helpdesk(ILanguage lang)
        {
            this.lang = lang;
        }

        [Command("helper")]
        public async Task ToggleHelper()
        {
            var user = Context.User as SocketGuildUser;
            var role = Context.Guild.GetRole(Constants.HelperRoleId);
            var phrase = "HELPER_ADDED";

            if(user.Roles.Any(r => r.Id == role.Id))
            {
                await user.RemoveRoleAsync(role);
                phrase = "HELPER_REMOVED";
            }
            else
            {
                await user.AddRoleAsync(role);
            }

            var message = await ReplyAsync(lang.GetPhrase(phrase));
            await Task.Delay(5000);
            await message.DeleteAsync();
            await Context.Message.DeleteAsync();
        }
    }
}