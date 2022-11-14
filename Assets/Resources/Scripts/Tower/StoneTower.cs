using UnityEngine;

namespace Towers
{
    public class StoneTower : Tower
    {
        [SerializeField, Min(1)] private int _levelStoneTower;
        public override int Level => _levelStoneTower;
    }
}