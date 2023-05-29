using System.Collections.Generic;

namespace GameCore
{
    public interface IInteractor
    {
        public List<IInteractable> GetInteractables();
        public bool AddInteractable(IInteractable interactable);
        public bool RemoveInteractable(IInteractable interactable);
    }
}