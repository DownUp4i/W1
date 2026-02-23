using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastucture
{
    public static class GameplayContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateRandomService);
        }

        public static RandomService CreateRandomService(DIContainer c)
            => new RandomService();
    }
}
