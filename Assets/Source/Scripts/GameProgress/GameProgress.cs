using System.Collections.Generic;

namespace WindowsCleaner.GameProgressNs
{
    [System.Serializable]
    public class GameProgress
    {
        public List<LevelData> Levels = new List<LevelData>();
        public float TotalScore;
        public int CurrentLevel;

        public void UpdateTotalScore()
        {
            TotalScore = 0;
            foreach (var level in Levels)
            {
                TotalScore += level.Score;
            }
        }
    }
}