using System.Collections;
using System.Collections.Generic;
using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Runtime.Gameplay.Infrastucture;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        public override IEnumerator Initialize()
        {
            yield break;
        }

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;
        }

        public override void Run()
        {
            Debug.Log("Main Menu");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
                ICoroutineService coroutineService = _container.Resolve<ICoroutineService>();

                coroutineService.StartPerform(sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, 
                    new GameplayInputArgs(GameMode.Digits)));
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
                ICoroutineService coroutineService = _container.Resolve<ICoroutineService>();

                coroutineService.StartPerform(sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, 
                    new GameplayInputArgs(GameMode.Letters)));
            }
        }
    }
}