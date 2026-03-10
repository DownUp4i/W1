using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Runtime.Gameplay.Features;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.InputCheckerManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastucture
{
    public static class GameplayContextRegistrations
    {
        private static GameplayInputArgs _gameplayInputArgs;

        public static void Process(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _gameplayInputArgs = gameplayInputArgs;

            container.RegisterAsSingle(CreateRandomService);
            container.RegisterAsSingle(CreateGameplayCycle);
        }

        private static RandomService CreateRandomService(DIContainer c)
            => new RandomService();

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
        {
            ConfigsProviderService configService = c.Resolve<ConfigsProviderService>();
            LevelsConfigs levelsConfigs = configService.GetConfig<LevelsConfigs>();

            LevelConfig levelConfig = levelsConfigs.GetLevelConfigBy(_gameplayInputArgs.GameMode);

            InputCheckerService inputCheckerService = c.Resolve<InputCheckerService>();
            RandomService randomService = c.Resolve<RandomService>();
            ICoroutineService coroutineService = c.Resolve<ICoroutineService>();
            KeySceneSwitcher keySceneSwitcher = c.Resolve<KeySceneSwitcher>();
            WinLoseService winLoseService = c.Resolve<WinLoseService>();
            PlayerDataProvider playerDataProvider = c.Resolve<PlayerDataProvider>();

            return new GameplayCycle(
                inputCheckerService,
                randomService, coroutineService,
                levelConfig,
                winLoseService,
                playerDataProvider);
        }
    }
}
