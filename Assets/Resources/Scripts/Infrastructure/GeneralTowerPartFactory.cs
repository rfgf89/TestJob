using Game.Config;
using Towers;
using Towers.Enums;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralTowerPartFactory
    {
        private GeneralTowerScriptableObject _config;

        public GeneralTowerPartFactory(GeneralTowerScriptableObject config)
        {
            _config = config;
        }

        public Component Create(TowerMarker marker, Vector3 at, Transform parent = null)
        {
            switch (marker.towerType)
            {
                case TowerType.TowerL1:
                    return GameObject.Instantiate(_config.towerL1, _config.towerL1.transform.localPosition + at, Quaternion.identity, parent).transform;
                case TowerType.TowerL2:
                    return GameObject.Instantiate(_config.towerL2, _config.towerL2.transform.localPosition + at, Quaternion.identity, parent).transform;
                case TowerType.TowerL3:
                    return GameObject.Instantiate(_config.towerL3, _config.towerL3.transform.localPosition + at, Quaternion.identity, parent).transform;
                case TowerType.TowerL4:
                    return GameObject.Instantiate(_config.towerL4, _config.towerL4.transform.localPosition + at, Quaternion.identity, parent).transform;
            }

            Debug.LogError($"Not Implement : {marker.towerType} for General TowerWeapon Part Factory");
            return null;
        }
    }
}