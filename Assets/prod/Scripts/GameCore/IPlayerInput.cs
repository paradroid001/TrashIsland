namespace GameCore
{
    public interface IPlayerInput<T>
    {
        public void StartCollecting();
        public void StopCollecting();
        public T GetState();
    }
}