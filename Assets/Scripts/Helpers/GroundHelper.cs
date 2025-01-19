using System;
using UnityEngine;

namespace Helpers
{
    public class GroundHelper : MonoBehaviour
    {
        private Player _player;
        private bool _followEnabled;
        public void SetTarget(Player player)
        {
            _player = player;
            _followEnabled = true;
        }

        private void FixedUpdate()
        {
            if (!_followEnabled) return;

            var position = _player.transform.position;
            var target = new Vector3(position.x, 0f, position.z);
            transform.position = target;
        }
    }
}
