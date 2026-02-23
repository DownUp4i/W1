using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastucture
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs (GameMode gameMode)
        {
            GameMode = gameMode;
        }

        public GameMode GameMode { get; }
    }
}
