using Game;
using Towers.Enums;
using Towers.Projectiles;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralProjectileFactory : GameLoopFactory, IProjectileFactory
    {
        [SerializeField] private Projectile _common;
        [SerializeField] private Projectile _guided;

        public void Init(GameLoop gameLoop)
        {
            base.Init(gameLoop);
        }

        public Projectile Create(ProjectileType projectileType, Vector3 at)
        {
            switch (projectileType)
            {
                case ProjectileType.Common: return Instance(_common, at);
                case ProjectileType.Guided: return Instance(_guided, at);
            }

            Debug.LogError($"Not Implementation : {projectileType} for GeneralProjectileFactory");
            return null;
        }

    }
}