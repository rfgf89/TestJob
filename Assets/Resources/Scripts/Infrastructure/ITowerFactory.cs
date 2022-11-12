using Towers;
using UnityEngine;

namespace Infrastructure
{
    public interface ITowerFactory
    {
        Tower Create(TowerMarker towerMarker, Vector3 at);
    }
}