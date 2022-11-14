using UnityEngine;

namespace Towers
{
    public class Tower : MonoBehaviour, ILevel
    {
        public virtual int Level { get; }
    }
}