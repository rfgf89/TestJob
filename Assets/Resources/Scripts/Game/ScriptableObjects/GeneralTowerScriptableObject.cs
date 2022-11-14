using System.Linq;
using Towers;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GeneralTowerConfig", menuName = "GameConfig/General Tower Config")]
    public class GeneralTowerScriptableObject : ScriptableObject
    {
        [SerializeField] private StoneTower[] _stoneTowers;
        
        public StoneTower FindStone(int level) => _stoneTowers.First(tower => tower.Level == level);
        
    }
}