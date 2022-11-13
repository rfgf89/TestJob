using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GeneralTowerConfig", menuName = "GameConfig/General TowerWeapon Config")]
    public class GeneralTowerScriptableObject : ScriptableObject
    {
        public Tower towerL1;
        public Tower towerL2;
        public Tower towerL3;
        public Tower towerL4;
    }
}