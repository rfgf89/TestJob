using Towers.Projectiles;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GeneralProjectileConfig", menuName = "GameConfig/General Projectile Config")]
    public class GeneralProjectileScriptableObject : ScriptableObject
    {
        public CannonProjectile common;
        public GuidedProjectile guided;
    }
}