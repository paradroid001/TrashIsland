namespace GameCore
{
    public interface IUIWindow
    {
        public void Hide();
        public void Show();
        public void Toggle();
        public void Destroy();
        public void Refresh();
    }
}