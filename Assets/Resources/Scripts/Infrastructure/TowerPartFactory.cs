using Towers;
using UnityEngine;

namespace Infrastructure
{
    public class TowerPartFactory : GameLoopFactory
    {
        public virtual Component CreatePart(TowerMarker marker, Vector3 at, Transform parent = null) => null;
    }
}