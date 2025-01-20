using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "New Physics Properties", menuName = "Scriptable/Custom Physics")]
    public class CustomPhysicsProperties : ScriptableObject
    {
        public float defaultGravity;
        public float currentGravity;
        public float friction;
    }
}
