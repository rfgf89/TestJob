using Game;
using Game.Config;
using Towers.Enums;
using Towers.Projectiles;
using Unity.Mathematics;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralProjectileFactory : IProjectileFactory
    {
        private readonly GeneralProjectileScriptableObject _config;
        
        private readonly GameLoop _gameLoop;   
        
        public GeneralProjectileFactory(GeneralProjectileScriptableObject config, GameLoop gameLoop)
        {
            _config = config;
            _gameLoop = gameLoop;
        }
        
        public Projectile Create(ProjectileType projectileType, Vector3 at)
        {
            switch (projectileType)
            {
                case ProjectileType.Common: return Instance(_config.common, at).Init(_gameLoop);
                case ProjectileType.Guided: return Instance(_config.guided, at).Init(_gameLoop);
            }

            Debug.LogError($"Not Implementation : {projectileType} for General Projectile Factory");
            return null;
        }

        private T Instance<T>(T prefab, Vector3 at, Transform parent = null) where T : Component
        {
            var projectile = GameObject.Instantiate(prefab, prefab.transform.localPosition + at, Quaternion.identity, parent);
            _gameLoop.Add(projectile as IGameUpdate);
            return projectile;
        }

    }
}