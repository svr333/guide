using Guide.Json;

namespace Guide.Helpers
{
    public class UserIssues
    {
        private readonly IJsonStorage jsonStorage;

        public UserIssues(IJsonStorage jsonStorage)
        {
            this.jsonStorage = jsonStorage;
        }

        public void AddIssue(UserIssue issue)
        {
            jsonStorage.Store(issue, GetIssueFileByMessageId(issue.MessageId), false);
        }

        public UserIssue GetByMessageId(ulong id)
        {
            try
            {
                return jsonStorage.Get<UserIssue>(GetIssueFileByMessageId(id));
            }
            catch
            {
                return null;
            }
        }

        public void DeleteByMessageId(ulong id)
        {
            jsonStorage.DeleteFile($"data/{GetIssueFileByMessageId(id)}");
        }

        private string GetIssueFileByMessageId(ulong id)
        {
            return $"{Constants.UserIssueGroup}{id}.json";
        }
    }
}
