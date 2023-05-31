public interface ISelectable
{
    public void OnSelected();
    public void OnUnselected();
    public bool IsAvailableForSelection();
    public bool IsSelected();
}
