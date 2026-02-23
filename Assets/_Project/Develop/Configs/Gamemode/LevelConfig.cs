using UnityEngine;

namespace Assets._Project.Develop.Configs.Gamemode
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/NewLevelConfig", fileName = "LevelConfig")]

    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public string Symbols { get; private set; }
    }
}