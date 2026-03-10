using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project.Develop.Configs.Gamemode;
using Assets._Project.Develop.Configs.Meta.Features.Stats;
using Assets._Project.Develop.Configs.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsPaths = new()
        {
            {typeof(LevelsConfigs), "Configs/LevelsConfigs"},
            {typeof(StartWalletConfig), "Configs/Features/Wallet/StartWalletConfig"},
            {typeof(CurrencyChangeConfig), "Configs/Features/Wallet/CurrencyChangeConfig" },
            {typeof(CurrencyToResetStatsConfig), "Configs/Features/Stats/CurrencyToResetStatsConfig" }
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configPath in _configsPaths)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configPath.Value);
                loadedConfigs.Add(configPath.Key, config);
                yield return null;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }
    }
}
