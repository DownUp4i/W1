using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Configs.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace Assets._Project.Develop.Runtime.Gameplay.Features
{
    public class WinLoseService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private int _winCount;
        private int _loseCount;

        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;

        public int WinCount => _winCount;
        public int LoseCount => _loseCount;

        public WinLoseService(WalletService walletService,
            PlayerDataProvider playerDataProvider,
            ConfigsProviderService configsProviderService)
        {
            _configsProviderService = configsProviderService;
            _walletService = walletService;

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public void Win()
        {
            _walletService.Add(CurrencyTypes.Gold, _configsProviderService.GetConfig<CurrencyChangeConfig>().ValueToAdd);
            _winCount++;
            UnityEngine.Debug.Log("Вы победили");
        }

        public void Lose()
        {
            _walletService.Spend(CurrencyTypes.Gold, _configsProviderService.GetConfig<CurrencyChangeConfig>().ValueToRemove);
            _loseCount++;
            UnityEngine.Debug.Log("Вы проиграли");
        }

        public void Reset()
        {
            _loseCount = 0;
            _winCount  =0;
        }

        public void ReadFrom(PlayerData data)
        {
            _winCount = data.WinCount;
            _loseCount = data.LoseCount;
        }

        public void WriteTo(PlayerData data)
        {
            data.WinCount = _winCount;
            data.LoseCount = _loseCount;
        }
    }
}
