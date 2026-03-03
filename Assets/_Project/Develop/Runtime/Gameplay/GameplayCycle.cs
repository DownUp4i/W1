using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.InputCheckerManagment;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameplayCycle : IDisposable
    {
        private InputCheckerService _inputChecker;
        private RandomService _randomService;
        private LevelConfig _levelConfig;
        private ICoroutineService _coroutine;

        private ReactiveList<string> _input;
        private List<string> _secret;

        private int _count;

        public bool IsWin { get; private set; }

        public GameplayCycle(
            InputCheckerService inputChecker,
            RandomService randomService,
            ICoroutineService coroutine,
            LevelConfig levelConfig)
        {
            _inputChecker = inputChecker;
            _randomService = randomService;
            _coroutine = coroutine;

            _input = new ReactiveList<string>();
            _secret = new List<string>();

            _levelConfig = levelConfig;
        }

        public void Start()
        {
            Restart();
        }

        public void Restart()
        {
            Unsubscribe();

            _inputChecker.OnKeyPressed += CheckInput;
            _input.OnAdded += ShowInput;

            IsWin = false;

            _inputChecker.Activate();

            _input.Clear();
            _secret.Clear();

            _count = 0;

            _secret = _randomService.GenerateFrom(_levelConfig.Symbols);

            _coroutine.StartPerform(_inputChecker.Check());

            ShowSecret();
        }

        private void CheckInput(char keyChar)
        {
            string inputChar = keyChar.ToString().ToLower();

            _input.Add(inputChar);

            ++_count;

            if (_count >= _secret.Count)
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

            IsWin = true;

            _inputChecker.Deactivate();

            Unsubscribe();
        }

        private void Loose()
        {
            UnityEngine.Debug.Log("Loose");

            IsWin = false;

            _inputChecker.Deactivate();

            Unsubscribe();
        }

        public void Dispose()
        {
            _inputChecker.OnKeyPressed -= CheckInput;
            _input.OnAdded -= ShowInput;
        }

        private void Unsubscribe()
        {
            _inputChecker.OnKeyPressed -= CheckInput;
            _input.OnAdded -= ShowInput;
        }
    }
}
