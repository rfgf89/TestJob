using Game;
using Game.Config;
using Towers;
using Towers.Enums;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralTowerWeaponFactory
    {
        private readonly GeneralTowerWeaponScriptableObject _config;

        private readonly IProjectileFactory _projectileFactory;
        private readonly GameLoop _gameLoop;
        public GeneralTowerWeaponFactory(GeneralTowerWeaponScriptableObject config, GameLoop gameLoop, IProjectileFactory projectileFactory)
        {
            _config = config;
            _gameLoop = gameLoop;
            _projectileFactory = projectileFactory;
        }

        public void Create(TowerMarker marker, Vector3 at, Transform parent)
        {
            switch (marker.cannonType)
            {
                case CannonType.Crystal: 
                    TryInstance(_config
                    .FindCrystal(marker.cannonLevel), at, parent)
                    ?.Init(_projectileFactory); return;
                
                case CannonType.Cannon: 
                    TryInstance(_config
                    .FindCannon(marker.cannonLevel), at, parent)
                    ?.Init(_projectileFactory); return;
            }
            Debug.LogError($"Not Implement : {marker.towerType} for Weapon Factory");
        }

        private T TryInstance<T>(T prefab, Vector3 at, Transform parent = null) where T : Component
        {
            if (prefab != null)
                return Instance(prefab, at, parent);
            
            Debug.LogError($"Not Find : Weapon from Factory Config");
            return null;
        }
        
        private T Instance<T>(T prefab, Vector3 at, Transform parent = null) where T : Component
        { 
            var instance = GameObject.Instantiate(prefab, prefab.transform.localPosition + at, Quaternion.identity, parent);
            _gameLoop.Add(instance as IGameUpdate);
            return instance;
        }
    }
}