using Game;
using Infrastructure;
using UnityEngine;

namespace Towers
{
    public abstract class TowerWeapon : MonoBehaviour, ILevel
    {
        public virtual int Level { get; }
        protected bool TryGetTarget(out ITowerTarget target, float radius, LayerMask layerMask)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, radius, layerMask);
            if (targets.Length > 0)
            {
                target = targets[0].GetComponent<ITowerTarget>();
                return true;
            }

            target = null;

            return false;
        }
    }
}