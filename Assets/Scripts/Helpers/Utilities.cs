using System;
using UnityEngine;

namespace Helpers
{
    public class Utilities : MonoBehaviour
    {
        public enum PlatformType
        {
            SquarePlatform,
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
