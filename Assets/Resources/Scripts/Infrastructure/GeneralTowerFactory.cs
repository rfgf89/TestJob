using Game.Config;
using Towers;
using Towers.Enums;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralTowerFactory
    {
        private readonly GeneralTowerScriptableObject _config;

        public GeneralTowerFactory(GeneralTowerScriptableObject config)
        {
            _config = config;
        }

        public Component Create(TowerMarker marker, Vector3 at, Transform parent = null)
        {
            switch (marker.towerType)
            {
                case TowerType.StoneTower : return TryInstance(_config.FindStone(marker.towerLevel), at, parent)?.transform;
            }
            
            Debug.LogError($"Not Implement : {marker.towerType} for Tower Factory");
            return null;
        }
        
        
        private T TryInstance<T>(T prefab, Vector3 at, Transform parent = null) where T : Component
        {
            if (prefab != null)
                return GameObject.Instantiate(prefab, prefab.transform.localPosition + at, Quaternion.identity, parent);
            
            Debug.LogError($"Not Find : Tower from Factory Config");
            return null;
        }
        
        
    }
}