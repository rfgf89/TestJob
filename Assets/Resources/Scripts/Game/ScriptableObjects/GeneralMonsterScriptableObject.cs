using Game.Monsters;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GeneralMonsterConfig", menuName = "GameConfig/General Monster Config")]
    public class GeneralMonsterScriptableObject : ScriptableObject
    {
        public Monster monster;
    }
}