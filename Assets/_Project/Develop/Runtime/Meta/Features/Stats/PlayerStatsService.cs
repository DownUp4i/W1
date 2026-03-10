using System;
using Assets._Project.Develop.Configs.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.Gameplay.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class PlayerStatsService
    {
        private PlayerDataProvider _playerDataProvider;
        private ConfigsProviderService _configProviderService;

        private WalletService _walletService;
        private WinLoseService _winLoseService;

        public PlayerStatsService(
            PlayerDataProvider playerDataProvider, 
            ConfigsProviderService configProviderService, 
            WalletService walletService, 
            WinLoseService winLoseService)
        {
            _playerDataProvider = playerDataProvider;
            _configProviderService = configProviderService;
            _walletService = walletService;
            _winLoseService = winLoseService;
        }

        public void Show()
        {
            UnityEngine.Debug.Log($"Золото: {_playerDataProvider.Data.WalletData[CurrencyTypes.Gold]}," +
                $" Победы: {_playerDataProvider.Data.WinCount}, " +
                $"Поражения: {_playerDataProvider.Data.LoseCount}");
        }

        public void Reset()
        {
            int valueToReset = _configProviderService.GetConfig<CurrencyToResetStatsConfig>().Value;

            if (_walletService.Enough(CurrencyTypes.Gold, valueToReset))
            {
                _walletService.Spend(CurrencyTypes.Gold, valueToReset);
                _winLoseService.Reset();
                UnityEngine.Debug.Log("Reset");
            }
            else
            {
                UnityEngine.Debug.Log($"Не хватает {nameof(CurrencyTypes.Gold)} : {Math.Abs(_playerDataProvider.Data.WalletData[CurrencyTypes.Gold] - valueToReset)}");
            }
        }
    }
}
