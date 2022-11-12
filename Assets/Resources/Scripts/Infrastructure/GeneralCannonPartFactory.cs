using Game;
using Game.Config;
using Towers;
using Towers.Enums;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralCannonPartFactory
    {
        private GeneralCannonScriptableObject _config;

        private IProjectileFactory _projectileFactory;
        private GameLoop _gameLoop;
        public void Init(GeneralCannonScriptableObject config, GameLoop gameLoop, IProjectileFactory projectileFactory)
        {
            _config = config;
            _gameLoop = gameLoop;
            _projectileFactory = projectileFactory;
        }

        public void Create(TowerMarker marker, Vector3 at, Transform parent)
        {
            switch (marker.cannonType)
            {
                case CannonType.CrystalL1: Instance(_config.crystalL1,at, parent).Init(_projectileFactory); return;
                case CannonType.CrystalL2: Instance(_config.crystalL2,at, parent).Init(_projectileFactory); return;
                case CannonType.CrystalL3: Instance(_config.crystalL3,at, parent).Init(_projectileFactory); return;

                case CannonType.CannonL1: Instance(_config.cannonL1,at, parent).Init(_gameLoop, _projectileFactory); return;
                case CannonType.CannonL2: Instance(_config.cannonL2,at, parent).Init(_gameLoop, _projectileFactory); return;
                case CannonType.CannonL3: Instance(_config.cannonL3,at, parent).Init(_gameLoop, _projectileFactory); return;
            }

            Debug.LogError($"Not Implement : {marker.towerType} for General Tower Part Factory");
        }

        private T Instance<T>(T prefab, Vector3 at, Transform parent = null) where T : Component
        { 
            var instance = GameObject.Instantiate(prefab, prefab.transform.localPosition + at, Quaternion.identity, parent);
            _gameLoop.Add(instance as IGameUpdate);
            return instance;
        }
    }
}