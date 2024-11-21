using System.Collections.Generic;

public class GameProgress
{
    public List<LevelData> Levels = new List<LevelData>();
    public float TotalScore;

    public void UpdateTotalScore()
    {
        TotalScore = 0;
        foreach(var level in Levels)
        {
            TotalScore += level.Score;
        }
    }


    [System.Serializable]
    public class LevelData
    {
        public int LevelNumber;
        public bool IsUnlocked;
        public float Score;
    }

}
