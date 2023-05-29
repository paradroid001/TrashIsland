namespace GameCore
{
    public class ServiceLocator<T> where T : IService
    {
        private static T _service;
        public static T GetProvider() { return _service; }
        public static void Provide(T service)
        {
            _service = service;
        }
    }
}