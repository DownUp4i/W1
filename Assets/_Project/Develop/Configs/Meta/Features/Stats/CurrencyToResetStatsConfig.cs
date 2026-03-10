using UnityEngine;

namespace Assets._Project.Develop.Configs.Meta.Features.Stats
{
    [CreateAssetMenu(menuName = "Configs/Meta/Stats/CurrencyToResetStatsConfig", fileName = "CurrencyToResetStatsConfig")]
    public class CurrencyToResetStatsConfig : ScriptableObject
    {
        [field: SerializeField] public int Value { get; private set; }
    }
}
