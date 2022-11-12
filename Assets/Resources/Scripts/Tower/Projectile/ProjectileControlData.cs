using UnityEngine;

namespace Towers.Projectiles
{
    public class ProjectileControlData
    {
        public readonly Projectile projectile;
        public float time;
        public Vector3 target;

        public ProjectileControlData(Projectile projectile, float time, Vector3 target)
        {
            this.projectile = projectile;
            this.time = time;
            this.target = target;
        }
    }
}