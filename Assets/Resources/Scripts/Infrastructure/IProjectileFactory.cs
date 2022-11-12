using Towers.Enums;
using Towers.Projectiles;
using UnityEngine;

namespace Infrastructure
{
    public interface IProjectileFactory
    {
        Projectile Create(ProjectileType projectileType, Vector3 at);
    }
}