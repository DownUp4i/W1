using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.InputCheckerManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Infrastucture.EntryPoint
{
    public class ProjectContextRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateResourcesAssetsLoader);

            container.RegisterAsSingle(CreateConfigsProviderService);

            container.RegisterAsSingle<ICoroutineService>(CreateCoroutineService);

            container.RegisterAsSingle(CreateSceneLoaderService);
            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle(CreateInputCheckerService);
        }

        public static InputCheckerService CreateInputCheckerService(DIContainer c)
            => new InputCheckerService();

        public static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c)
            => new ResourcesAssetsLoader();

        public static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resources = c.Resolve<ResourcesAssetsLoader>();

            ResourcesConfigsLoader configsLoaders = new ResourcesConfigsLoader(resources);

            return new ConfigsProviderService(configsLoaders);
        }

        public static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        public static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(c.Resolve<SceneLoaderService>(), c);

        public static CoroutineService CreateCoroutineService(DIContainer c)
        {
            CoroutineService coroutinePrefab = c.Resolve<ResourcesAssetsLoader>()
                .Load<CoroutineService>("Utilities/Coroutine");

            return Object.Instantiate(coroutinePrefab);
        }
    }
}
