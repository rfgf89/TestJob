using UnityEngine;

namespace Towers
{
    public interface ITowerTarget
    {
        float MaxBoundSize { get; }
        Vector3 Velocity { get; }
        Transform transform { get; }
    }
}