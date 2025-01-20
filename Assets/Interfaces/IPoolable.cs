using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        public void OnFetchedFromPool();

        public void OnReturnPool();

        public GameObject GameObject();
    }
}
