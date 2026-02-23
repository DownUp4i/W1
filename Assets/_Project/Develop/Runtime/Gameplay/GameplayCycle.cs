using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.InputCheckerManagment;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameplayCycle : IDisposable
    {
        public event Action<string> OnInputAdded;
        private DIContainer _container;
        private GameMode _gameMode;

        private InputCheckerService _inputChecker;

        private ReactiveList<string> _input;
        private List<string> _secret;
        private int _count;

        public bool IsWin {  get; private set; }

        public GameplayCycle(DIContainer container, GameMode gameMode)
        {
            _container = container;
            _gameMode = gameMode;

            _inputChecker = _container.Resolve<InputCheckerService>();

            _input = new ReactiveList<string>();
            _secret = new List<string>();
        }

        public void Start()
        {
            ConfigsProviderService configService = _container.Resolve<ConfigsProviderService>();
            LevelsConfigs levelsConfigs = configService.GetConfig<LevelsConfigs>();
            LevelConfig levelConfig = levelsConfigs.GetLevelConfigBy(_gameMode);

            Restart();

            _inputChecker.Activate();

            _container.Resolve<ICoroutineService>().StartPerform(_inputChecker.Check());
        }

        public void Restart()
        {
            ConfigsProviderService configService = _container.Resolve<ConfigsProviderService>();
            LevelsConfigs levelsConfigs = configService.GetConfig<LevelsConfigs>();
            LevelConfig levelConfig = levelsConfigs.GetLevelConfigBy(_gameMode);

            _inputChecker.OnKeyPressed += CheckInput;
            _input.OnAdded += ShowInput;

            _input.Clear();
            _secret.Clear();

            _count = 0;

            SetRandomSymbols(levelConfig.Symbols);
        }

        private void CheckInput(KeyCode key)
        {
            string inputChar = "";

            if (key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9)
                inputChar = ((char)('0' + (key - KeyCode.Alpha0))).ToString();
            else if (key >= KeyCode.A && key <= KeyCode.Z)
                inputChar = key.ToString().ToLower();
            else
                return;

            _input.Add(inputChar);

            ++_count;

            if (_count == _secret.Count)
            {
                if (_input.Values.SequenceEqual(_secret))
                    Win();
                else
                    Loose();
            }
        }

        private void ShowInput()
        {
            string text = "";

            foreach (string item in _input.Values)
                text += item;

            UnityEngine.Debug.Log("Введенные символы: " + text);
        }

        private void ShowSecret()
        {
            string text = "";

            _secret.ForEach(item => text += item);
            UnityEngine.Debug.Log("Секретные символы: " + text);

        }

        private void Win()
        {
            UnityEngine.Debug.Log("Win");
            UnityEngine.Debug.Log("Нажмите пробел чтобы перейти на главное меню");
            _inputChecker.OnKeyPressed -= CheckInput;
            _input.OnAdded -= ShowInput;
            IsWin = true;
        }

        private void SetRandomSymbols(string symbols, int quantity = 5)
        {
            RandomService randomService = _container.Resolve<RandomService>();

            for (int i = 0; i < quantity; i++)
            {
                char symbol = randomService.GetRandomCharFrom(symbols);
                _secret.Add(symbol.ToString());
            }

            ShowSecret();
        }

        private void Loose()
        {
            UnityEngine.Debug.Log("Loose");

            _inputChecker.OnKeyPressed -= CheckInput;
            _input.OnAdded -= ShowInput;

            IsWin = false;
        }

        public void Dispose()
        {
            _inputChecker.OnKeyPressed -= CheckInput;
            _input.OnAdded -= ShowInput;
        }
    }
}
