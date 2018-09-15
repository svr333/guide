using Discord;

namespace Guide
{
    public static class Constants
    {
        public const string ConfigKeyToken = "DiscordToken";
        public const string JsonDirectory = "data";
        public const string LanguageFile = "Lang/english.json";
        public const string UserIssueGroup = "userIssues/";
        public const string IssueMarker = "<:problem:470220494835286016>";
        public const string SolvedReactionName = "solved";
#if DEBUG
        public const ulong TutorialGuildId = 464334692502470666;
        public const ulong WaitingRoomId = 490426923630067746;
        public const ulong GeneralId = 488989707900813313;
        public const ulong MemberRoleId = 470188590538948608;
        public const ulong IssuesBoardId = 488989757607510028;
#else
        public const ulong TutorialGuildId = 377879473158356992;
        public const ulong WaitingRoomId = 411864218548043785;
        public const ulong GeneralId = 377879473644765185;
        public const ulong MemberRoleId = 411865173318696961;
        public const ulong IssuesBoardId = 470217412147675137;
#endif

        public const string PKWelcomeTitle = "WELCOME_EMBED_TITLE";
        public const string PKWhileYouWait = "WELCOME_EMBED_WHILE_YOU_WAIT";
        public const string PKFunServerFact = "FUN_SERVER_FACT";
        public const string PKUserJoinedTitle = "USER_JOINED_TITLE";
        public static readonly Color PrimaryColor = new Color(41, 182, 246);
    }
}
