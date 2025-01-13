namespace Interfaces
{
    public interface IPoolable
    {
        public void OnFetchedFromPool();

        public void ResetSelf();
    }
}
