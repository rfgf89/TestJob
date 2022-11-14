using System.Collections.Generic;
using System.Linq;
using Towers;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GeneralTowerWeaponConfig", menuName = "GameConfig/General Tower Weapon Config")]
    public class GeneralTowerWeaponScriptableObject : ScriptableObject
    {
        [SerializeField] private CrystalTowerWeapon[] _crystals;
        [SerializeField] private CannonTowerWeapon[] _cannons;

        public CrystalTowerWeapon FindCrystal(int level) => _crystals.First(weapon => weapon.Level == level);
        public CannonTowerWeapon FindCannon(int level) => _cannons.First(weapon => weapon.Level == level);
    }
    
}