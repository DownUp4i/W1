using System;
using System.Collections;
using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Runtime.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastucture;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.InputCheckerManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

public class GameplayBootstrap : SceneBootstrap
{
    private DIContainer _container;
    private GameplayCycle _cycle;
    private GameMode _gameMode;

    public override IEnumerator Initialize()
    {
        yield return _container.Resolve<ConfigsProviderService>().LoadAsync();
    }

    public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs)
    {
        _container = container;
        GameplayContextRegistrations.Process(_container);

        if (sceneArgs is not GameplayInputArgs gameplayArgs)
            throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)}");

        _gameMode = gameplayArgs.GameMode;

        _cycle = new GameplayCycle(_container, _gameMode);
    }

    public override void Run()
    {
        Debug.Log("Gameplay Running");
        _cycle.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_cycle.IsWin)
            {
                SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
                ICoroutineService coroutineService = _container.Resolve<ICoroutineService>();

                coroutineService.StartPerform(sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));

                _container.Resolve<InputCheckerService>().Deactivate();
            }
            else
            {
                _cycle.Restart();
            }

        }
    }

}
