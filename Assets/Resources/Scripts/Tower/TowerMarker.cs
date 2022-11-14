using Towers.Enums;
using UnityEngine;

namespace Towers
{
    public class TowerMarker : MonoBehaviour
    {
        public TowerType towerType;
        [Min(1)]public int towerLevel;
        
        public CannonType cannonType;
        [Min(1)]public int cannonLevel;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1f);
            Gizmos.color = Color.white;
        }
    }
}