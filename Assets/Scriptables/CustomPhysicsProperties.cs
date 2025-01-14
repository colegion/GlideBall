using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "New Physics Properties", menuName = "Scriptable/Custom Physics")]
    public class CustomPhysicsProperties : ScriptableObject
    {
        public float gravity;
        public float friction;
    }
}
