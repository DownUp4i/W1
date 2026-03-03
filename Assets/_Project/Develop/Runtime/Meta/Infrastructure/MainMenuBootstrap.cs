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
        private KeySceneSwitcher _keySceneSwitcher;

        public override IEnumerator Initialize()
        {
            yield break;
        }

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            _keySceneSwitcher = _container.Resolve<KeySceneSwitcher>();

            MainMenuContextRegistrations.Process(_container);
        }

        public override void Run()
        {
            Debug.Log("Main Menu");
        }

        private void Update()
        {
            if (_container != null)
            {
                _keySceneSwitcher.SwitchByGamemode(GameMode.Digits, KeyCode.Alpha1);
                _keySceneSwitcher.SwitchByGamemode(GameMode.Letters, KeyCode.Alpha2);
            }
        }
    }
}