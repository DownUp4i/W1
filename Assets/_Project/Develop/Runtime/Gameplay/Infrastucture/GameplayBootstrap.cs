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
    private KeySceneSwitcher _keySceneSwitcher;
    private GameplayCycle _cycle;

    public override IEnumerator Initialize()
    {
        yield return _container.Resolve<ConfigsProviderService>().LoadAsync();

        _cycle = _container.Resolve<GameplayCycle>();
    }

    public override void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs)
    {
        _container = container;

        _keySceneSwitcher = _container.Resolve<KeySceneSwitcher>();

        if (sceneArgs is not GameplayInputArgs gameplayArgs)
            throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)}");

        GameplayContextRegistrations.Process(_container, gameplayArgs);
    }

    public override void Run()
    {
        Debug.Log($"Gameplay Running");
        _cycle.Start();
    }

    private void Update()
    {
        if (_container != null)
            _keySceneSwitcher.SwitchTo(Scenes.MainMenu, KeyCode.Space);
    }
}
