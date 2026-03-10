using Assets._Project.Develop.Runtime.Gameplay.Features;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreatePlayerStatsService);
        }


        private static PlayerStatsService CreatePlayerStatsService(DIContainer c)
            => new PlayerStatsService(
                c.Resolve<PlayerDataProvider>(), 
                c.Resolve<ConfigsProviderService>(), 
                c.Resolve<WalletService>(),
                c.Resolve<WinLoseService>());
    }
}
