using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Guide.Helpers;
using Guide.Logging;

namespace Guide.Services
{
    public class UserIssuesService
    {
        private readonly DiscordSocketClient client;
        private readonly UserIssues userIssues;
        private readonly ILogger logger;

        public UserIssuesService(DiscordSocketClient client, UserIssues userIssues, ILogger logger)
        {
            this.client = client;
            this.userIssues = userIssues;
            this.logger = logger;

            client.MessageReceived += MessageReceived;
            client.ReactionAdded += ReactionAdded;
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> userMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if(channel.Id != Constants.IssuesBoardId) return;

            if(reaction.Emote.Name != Constants.SolvedReactionName)
            {
                logger.Log($"Added reaction was '{reaction.Emote.Name}' instead of expected '{Constants.SolvedReactionName}'.");
                return;
            }

            var msg = await userMessage.DownloadAsync();
            
            var issue = userIssues.GetByMessageId(msg.Id);

            var reactioner = client.GetGuild(Constants.TutorialGuildId).GetUser(reaction.UserId);

            if(reactioner is null)
            {
                logger.Log($"Could not find a guild user with ID {reaction.UserId}. Cannot verify issue authority.");
                return;
            }

            if(issue is null)
            {
                logger.Log($"Issue with message is '{msg.Id}' was not found. In order to keep integrity, I'm deleting the message.");
                userIssues.DeleteByMessageId(msg.Id);
                await msg.DeleteAsync();
                return;
            }

            if(reactioner.IsAdmin() || reactioner.Id == issue.UserId)
            {
                userIssues.DeleteByMessageId(msg.Id);
                await msg.DeleteAsync();
            }
        }

        public async Task CreateIssue(SocketCommandContext context)
        {
            if(context.Message.Content.Length <= 250)
            {
                await context.Channel.SendMessageAsync($":bulb: Your issue seems to be quite short.");    
            }

            userIssues.AddIssue(new UserIssue
            {
                MessageId = await CreateIssueMessage(context).ConfigureAwait(false),
                UserId = context.User.Id,
                Content = context.Message.Content
            });

            var guildUser = context.User as SocketGuildUser;

            await context.Channel.SendMessageAsync($":package: {guildUser.Nickname ?? guildUser.Username}, your issue was created.");
        }

        private async Task MessageReceived(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg))
            {
                return;
            }

            var context = new SocketCommandContext(client, msg);

            if(context.Channel.Id != Constants.GeneralId) return;

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
