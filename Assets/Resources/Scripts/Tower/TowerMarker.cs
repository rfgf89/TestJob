using Towers.Enums;
using UnityEngine;

namespace Towers
{
    public class TowerMarker : MonoBehaviour
    {
        public TowerType towerType;
        public CannonType cannonType;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1f);
            Gizmos.color = Color.white;
        }
    }
}