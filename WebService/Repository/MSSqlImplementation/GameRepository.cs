using System.Data.SqlClient;

namespace WebService.Repository.MSSqlImplementation;

public class GameRepository : Repository
{
    public const string GameSideTable = "[GameSide]";
    public const string WinReasonTable = "[WinReason]";
    public const string GameMoveTable = "[GameMove]";
    public const string GameEmoteTable = "[GameEmote]";
    public const string GameTurnTable = "[GameTurn]";
    public const string GameTable = "[Game]";

    public const string SideName = "[side_name]";
    public const string WinReasonName = "[win_reason_name]";
    public const string GameDuration = "[game_duration]";
    public const string GameStartTime = "[game_start_time]";
    public const string WinnerSideId = "[winner_side_id]";
    public const string WinReasonId = "[win_reason_id]";
    public const string Time = "[time]";
    public const string SideId = "[side_id]";
    public const string From = "[from]";
    public const string To = "[to]";
    public const string GameId = "[game_id]";
    public const string StartSocialCredit = "[start_social_credit]";
    public const string SocialCreditChange = "[social_credit_chang]";

    public const string CreateGameProc = "[SP_CreateGame]";
    public const string SelectGameProc = "[SP_SelectGame]";
    public const string CreateEmoteActionProc = "[SP_CreateEmoteAction]";
    public const string SelectEmoteActionProc = "[SP_SelectEmoteAction]";
    public const string CreateMoveActionProc = "[SP_CreateMoveAction]";
    public const string SelectMoveActionProc = "[SP_SelectMoveAction]";
    public const string CreateTurnActionProc = "[SP_CreateTurnAction]";
    public const string SelectTurnActionProc = "[SP_SelectTurnAction]";


    public const string GameStartTimeVar = "@start_time";
    public const string GameDurationVar = "@duration";
    public const string WinnerSideIdVar = "@winner_side_id";
    public const string WinReasonIdVar = "@win_reason_id";
    public const string SocialCreditChangeVar = "@social_credit_change";
    public const string GameIdVar = "@game_id";
    public const string SideIdVar = "@side_id";
    public const string TimeVar = "@time";
    public const string FromVar = "@from";
    public const string ToVar = "@to";


    protected GameRepository(SqlConnection connection) : base(connection)
    { }
}