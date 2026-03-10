using System;
using System.Collections;
using Object = UnityEngine.Object;
using Assets._Project.Develop.Runtime.Infrastucture.DI;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public class SceneSwitcherService
    {
        private SceneLoaderService _sceneLoaderService;
        private DIContainer _projectContainer;

        public SceneSwitcherService(SceneLoaderService sceneLoaderService, DIContainer projectContainer)
        {
            _sceneLoaderService = sceneLoaderService;
            _projectContainer = projectContainer;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(nameof(sceneBootstrap) + " not found");

            DIContainer sceneContainer = new DIContainer(_projectContainer);

            sceneBootstrap.ProcessRegistration(sceneContainer, sceneArgs);

            sceneContainer.Initialize();

            yield return sceneBootstrap.Initialize();

            sceneBootstrap.Run();
        }
    }
}
