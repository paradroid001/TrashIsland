using GameCore;
namespace TrashIsland
{
    public interface ITIObject
    {
        public string GetObjectName();
        public string GetObjectDescription();
        public IInteractable GetInteractable();
        public ISelectable GetSelectable();

    }
}