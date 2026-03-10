using System.Collections;
using System.Collections.Generic;
using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Runtime.Gameplay.Infrastucture;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private KeySceneSwitcher _keySceneSwitcher;

        private PlayerDataProvider _playerDataProvider;
        private ICoroutineService _coroutineService;

        public override IEnumerator Initialize()
        {
            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _coroutineService = _container.Resolve<ICoroutineService>();

            yield break;
        }

        public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);

            _keySceneSwitcher = _container.Resolve<KeySceneSwitcher>();

        }

        public override void Run()
        {
            Debug.Log("Main Menu 1 - цифры, 2 - буквы");
        }

        private void Update()
        {
            if (_container != null)
            {
                _keySceneSwitcher.SwitchByGamemode(GameMode.Digits, KeyCode.Alpha1);
                _keySceneSwitcher.SwitchByGamemode(GameMode.Letters, KeyCode.Alpha2);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _container.Resolve<PlayerStatsService>().Reset();
                _coroutineService.StartPerform(_playerDataProvider.Save());
            }


            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _container.Resolve<PlayerStatsService>().Show();
            }
        }
    }
}