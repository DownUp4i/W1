using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Configs.Gamemode
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/NewLevelConfigs", fileName = "LevelsConfigs")]
    public class LevelsConfigs : ScriptableObject
    {
        [SerializeField] private List<Config> _configs;

        public LevelConfig GetLevelConfigBy(GameMode gamemode)
            => _configs.First(config => config.GameMode == gamemode).LevelConfig;

        [Serializable]
        private class Config
        {
            [field: SerializeField] public GameMode GameMode { get; private set; }
            [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
        }
    }
}