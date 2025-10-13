using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

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
