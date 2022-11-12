using Towers;
using Towers.Enums;
using Towers.Projectiles;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralTowerPartFactory : TowerPartFactory
    {
        [SerializeField] private GameObject _towerL1;
        [SerializeField] private GameObject _towerL2;
        [SerializeField] private GameObject _towerL3;

        public override Component CreatePart(TowerMarker marker, Vector3 at, Transform parent = null)
        {
            switch (marker.towerType)
            {
                case TowerType.TowerL1:
                    return Instantiate(_towerL1, _towerL1.transform.localPosition + at, Quaternion.identity, parent)
                        .transform;
                case TowerType.TowerL2:
                    return Instantiate(_towerL2, _towerL2.transform.localPosition + at, Quaternion.identity, parent)
                        .transform;
                case TowerType.TowerL3:
                    return Instantiate(_towerL3, _towerL3.transform.localPosition + at, Quaternion.identity, parent)
                        .transform;
            }

            Debug.LogError($"Not Implement : {marker.towerType} for General Tower Part Factory");
            return null;
        }
    }
}