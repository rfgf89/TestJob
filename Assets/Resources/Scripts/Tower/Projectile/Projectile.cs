using Game;
using UnityEngine;

namespace Towers.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public virtual void Launch(GameObject target, Vector3 velocity = default, float gravity = 9.81f, Vector3 start = default)
        { }
        
    }
}