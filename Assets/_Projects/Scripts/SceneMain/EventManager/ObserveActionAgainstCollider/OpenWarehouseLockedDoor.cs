namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenWarehouseLockedDoor()
        {
            if (_uiElements.WarehouseKey.IsHolding() is false)
            {
                _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                return;
            }

            _uiElements.WarehouseKey.Release();

            _objects.WarehouseLockedDoor.Trigger();
            _uiElements.LogText.ShowAutomatically("鍵を開けた");
        }
    }
}