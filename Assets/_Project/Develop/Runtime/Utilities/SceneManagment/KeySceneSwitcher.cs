using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Runtime.Gameplay.Infrastucture;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public class KeySceneSwitcher
    {
        private DIContainer _container;

        public KeySceneSwitcher(DIContainer container)
        {
            _container = container;
        }

        public void SwitchByGamemode(GameMode gamemode, KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
                ICoroutineService coroutineService = _container.Resolve<ICoroutineService>();

                coroutineService.StartPerform(sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay,
                    new GameplayInputArgs(gamemode)));
            }
        }

        public void SwitchTo(string scene, KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
                ICoroutineService coroutineService = _container.Resolve<ICoroutineService>();

                coroutineService.StartPerform(sceneSwitcher.ProcessSwitchTo(scene));
            }
        }
    }
}
