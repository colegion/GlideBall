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
    }
}
