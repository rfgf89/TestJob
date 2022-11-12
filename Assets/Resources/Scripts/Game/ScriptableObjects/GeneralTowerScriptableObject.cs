using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GeneralTowerConfig", menuName = "GameConfig/General Tower Config")]
    public class GeneralTowerScriptableObject : ScriptableObject
    {
        public GameObject towerL1;
        public GameObject towerL2;
        public GameObject towerL3;
        public GameObject towerL4;
    }
}