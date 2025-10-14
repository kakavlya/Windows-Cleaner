using YG;

namespace WindowsCleaner.GameProgressNs
{
    public class LeaderboardService
    {
        private readonly string _leaderBoardName;

        public LeaderboardService(string leaderBoardName)
        {
            _leaderBoardName = leaderBoardName;
        }

        public void UpdateLeaderboard(int newRecord)
        {
            YandexGame.NewLeaderboardScores(_leaderBoardName, newRecord);
        }
    }
}