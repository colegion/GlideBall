using System;
using UnityEngine;

namespace Helpers
{
    public class Utilities : MonoBehaviour
    {
        [SerializeField] private float platformDefaultBoostAmount;

        public static float DefaultBoostAmount { get; private set; }

        private void Start()
        {
            DefaultBoostAmount = platformDefaultBoostAmount;
        }

        public enum PlatformType
        {
            SquarePlatform = 0,
            CirclePlatform
        }

        [Serializable]
        public class Constraints
        {
            public bool freezeRotationX;
            public bool freezeRotationY;
            public bool freezeRotationZ;

            public bool freezePositionX;
            public bool freezePositionY;
            public bool freezePositionZ;
        }

        [Serializable]
        public struct ForceType
        {
            public AnimationCurve forceCurve;
        }

        public enum ForceTypes
        {
            Impulse,
            Linear,
        }
    }
}
