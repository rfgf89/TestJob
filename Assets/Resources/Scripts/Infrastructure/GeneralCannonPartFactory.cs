using Game;
using Towers;
using Towers.Enums;
using Towers.Projectiles;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralCannonPartFactory : TowerPartFactory
    {
        [SerializeField] private Tower _crystalL1;
        [SerializeField] private Tower _crystalL2;
        [SerializeField] private Tower _crystalL3;

        [SerializeField] private Tower _cannonL1;
        [SerializeField] private Tower _cannonL2;
        [SerializeField] private Tower _cannonL3;

        private IProjectileFactory _projectileFactory;

        public void Init(GameLoop gameLoop, IProjectileFactory projectileFactory)
        {
            base.Init(gameLoop);
            _projectileFactory = projectileFactory;
        }

        public override Component CreatePart(TowerMarker marker, Vector3 at, Transform parent)
        {
            switch (marker.cannonType)
            {
                case CannonType.CrystalL1: return Instance(_crystalL1, at, parent).Init(_projectileFactory);
                case CannonType.CrystalL2: return Instance(_crystalL2, at, parent).Init(_projectileFactory);
                case CannonType.CrystalL3: return Instance(_crystalL3, at, parent).Init(_projectileFactory);

                case CannonType.CannonL1: return Instance(_cannonL1, at, parent).Init(_projectileFactory);
                case CannonType.CannonL2: return Instance(_cannonL2, at, parent).Init(_projectileFactory);
                case CannonType.CannonL3: return Instance(_cannonL3, at, parent).Init(_projectileFactory);
            }

            Debug.LogError($"Not Implement : {marker.towerType} for General Tower Part Factory");
            return null;
        }
    }
}