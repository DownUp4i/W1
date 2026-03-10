using System.Collections.Generic;
using System;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.KeyStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializers;
using Assets._Project.Develop.Runtime.Utilities.InputCheckerManagment;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = UnityEngine.Object;
using Assets._Project.Develop.Runtime.Gameplay.Features;

namespace Assets._Project.Develop.Runtime.Infrastucture.EntryPoint
{
    public class ProjectContextRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutineService>(CreateCoroutineService);

            container.RegisterAsSingle(CreateConfigsProviderService);

            container.RegisterAsSingle(CreateResourcesAssetsLoader);

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle(CreateWalletService).NonLazy();

            container.RegisterAsSingle(CreatePlayerDataProvider);

            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);

            container.RegisterAsSingle(CreateKeySceneSwitcher);

            container.RegisterAsSingle(CreateInputCheckerService);

            container.RegisterAsSingle(CreateWinLoseService);
        }


        private static WinLoseService CreateWinLoseService(DIContainer c)
        {
            ConfigsProviderService configService = c.Resolve<ConfigsProviderService>();

            WalletService walletService = c.Resolve<WalletService>();
            PlayerDataProvider playerDataProvider = c.Resolve<PlayerDataProvider>();

            return new WinLoseService(walletService, playerDataProvider, configService);
        }

        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[currencyType] = new ReactiveVariable<int>();

            return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
        }

        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
            => new PlayerDataProvider(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigsProviderService>());

        private static SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            IDataSerializer dataSerializer = new JsonSerializer();
            IDataKeyStorage dataKeyStorage = new MapDataKeyStorage();

            string saveFolderPath = Application.isEditor ?
                Application.dataPath :
                Application.persistentDataPath;

            IDataRepository repository = new LocalFileDataRepository(saveFolderPath, "json");

            return new SaveLoadService(dataSerializer, dataKeyStorage, repository);
        }

        private static InputCheckerService CreateInputCheckerService(DIContainer c)
            => new InputCheckerService();

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c)
            => new ResourcesAssetsLoader();

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resources = c.Resolve<ResourcesAssetsLoader>();

            ResourcesConfigsLoader configsLoaders = new ResourcesConfigsLoader(resources);

            return new ConfigsProviderService(configsLoaders);
        }

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(c.Resolve<SceneLoaderService>(), c);

        private static CoroutineService CreateCoroutineService(DIContainer c)
        {
            CoroutineService coroutinePrefab = c.Resolve<ResourcesAssetsLoader>()
                .Load<CoroutineService>("Utilities/Coroutine");

            return Object.Instantiate(coroutinePrefab);
        }
        private static KeySceneSwitcher CreateKeySceneSwitcher(DIContainer c)
            => new KeySceneSwitcher(c);
    }
}
