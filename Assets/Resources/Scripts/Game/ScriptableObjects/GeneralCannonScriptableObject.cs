using Towers;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GeneralCannonConfig", menuName = "GameConfig/General Cannon Config")]
    public class GeneralCannonScriptableObject : ScriptableObject
    {
        public CrystalTower crystalL1;
        public CrystalTower crystalL2;
        public CrystalTower crystalL3;

        public CannonTower cannonL1;
        public CannonTower cannonL2;
        public CannonTower cannonL3;
    }
}