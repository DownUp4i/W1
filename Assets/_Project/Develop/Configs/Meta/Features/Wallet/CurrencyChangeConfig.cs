using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Develop.Configs.Meta.Features.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/CurrencyChangeConfig", fileName = "CurrencyChangeConfig")]
    public class CurrencyChangeConfig : ScriptableObject
    {
        [field: SerializeField] public int ValueToAdd { get; private set;}
        [field: SerializeField] public int ValueToRemove { get; private set;}
    }
}
