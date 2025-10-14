namespace WindowsCleaner.GameProgressNs
{
    public class GameDataRepository
    {
        private readonly GameDataHandle _gameDataHandle = new GameDataHandle();

        public GameProgress LoadProgress()
        {
            return _gameDataHandle.LoadProgress();
        }

        public void SaveProgress(GameProgress progress)
        {
            _gameDataHandle.SaveProgress(progress);
        }
    }
}