using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
<<<<<<< HEAD
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";
    private const string LevelCompleted = "LevelCompleted";

    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
=======
    public class SaveLoadService : ISaveLoadService
>>>>>>> 884faa757ea49c0624f6142f92af3e27e5492eb9
    {
        private const string ProgressKey = "Progress";
        private const string LevelCompleted = "LevelCompleted";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);
      
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

<<<<<<< HEAD
    public void SaveLevelCompleted()
    {
      int temp = _progressService.Progress.GameProgressData.LevelsCompleted;
      PlayerPrefs.SetInt(LevelCompleted, temp);
    }

    public int LoadLevelCompleted()
    {
      int value = PlayerPrefs.GetInt(LevelCompleted);
      
      return value;
    }

    public PlayerProgress LoadProgress()
    {
      PlayerProgress progress = PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
   
      return progress;
=======
        public void SaveLevelCompleted()
        {
            int temp = _progressService.Progress.GameProgressData.LevelsCompleted;
            PlayerPrefs.SetInt(LevelCompleted, temp);
        }

        public int LoadLevelCompleted()
        {
            int value = PlayerPrefs.GetInt(LevelCompleted);
      
            return value;
        }

        public PlayerProgress LoadProgress()
        {
            PlayerProgress progress = PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
   
            return progress;
        }
>>>>>>> 884faa757ea49c0624f6142f92af3e27e5492eb9
    }
}