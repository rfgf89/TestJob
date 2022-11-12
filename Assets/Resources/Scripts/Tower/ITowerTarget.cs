using UnityEngine;

namespace Towers
{
    public interface ITowerTarget
    {
        float MaxBoundSize { get; }
        Vector3 Velocity { get; }
        float Speed { get; }
        GameObject gameObject { get; }
    }
}