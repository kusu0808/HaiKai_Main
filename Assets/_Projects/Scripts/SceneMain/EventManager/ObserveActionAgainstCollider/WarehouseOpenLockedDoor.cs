namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehouseOpenLockedDoor()
        {
            if (_uiElements.WarehouseKey.IsHolding() is true)
            {
                _uiElements.WarehouseKey.Release();

                _objects.WarehouseLockedDoor.Trigger();
                _uiElements.LogText.ShowAutomatically("鍵を開けた");
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("鍵を開けられるものはないだろうか？");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("鍵がかかっている");
            }
        }
    }
}