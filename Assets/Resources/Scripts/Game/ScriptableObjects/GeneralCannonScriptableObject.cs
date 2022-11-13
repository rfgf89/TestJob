using Towers;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GeneralCannonConfig", menuName = "GameConfig/General Cannon Config")]
    public class GeneralCannonScriptableObject : ScriptableObject
    {
        public CrystalTowerWeapon crystalL1;
        public CrystalTowerWeapon crystalL2;
        public CrystalTowerWeapon crystalL3;

        public CannonTowerWeapon cannonL1;
        public CannonTowerWeapon cannonL2;
        public CannonTowerWeapon cannonL3;
    }
}