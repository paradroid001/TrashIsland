namespace TrashIsland
{
    public interface ITIInventory
    {
        public TIObject GetOwner();
        public int GetCapacity();
        public int SetCapacity();
        public int GetAvailable();
        public TIObject PeekItemAt(int index);
        public bool AddItem(TIObject item);
        public TIObject RemoveItem(TIObject item);

    }
}