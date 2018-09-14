using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Guide.Services
{
    public class UserIssuesService
    {
        private readonly DiscordSocketClient client;

        public UserIssuesService(DiscordSocketClient client)
        {
            this.client = client;

            client.MessageReceived += MessageReceived;
        }

        public async Task CreateIssue(SocketCommandContext context)
        {
            // TODO: Requires UserIssues.cs to be done
            
            // userIssuesService.NewIssue(new UserIssue
            // {
            //     Id = await CreateIssueMessage(context).ConfigureAwait(false),
            //     UserId = context.User.Id,
            //     Contents = context.Message.Content
            // });
        }

        private async Task MessageReceived(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg))
            {
                return;
            }

            var context = new SocketCommandContext(client, msg);

            if(context.Message.Content.StartsWith(Constants.IssueMarker))
            {
                await CreateIssue(context);
            }
        }

        private static async Task<ulong> CreateIssueMessage(SocketCommandContext context)
        {
            var issuesBoard = context.Guild.GetTextChannel(Constants.IssuesBoardId);
            var message = await issuesBoard.SendMessageAsync(GetIssueText(context));
            return message.Id;
        }

        private static string GetIssueText(SocketCommandContext context)
        {
            var contents = context.Message.Content;
            var user = context.User as SocketGuildUser;
            return $"**Issue by:** {user.Mention}\n{contents}";
        }
    }
}
