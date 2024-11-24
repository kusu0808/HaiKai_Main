namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenWarehouseLockedDoor()
        {
            if (_playerItem.HasButaiSideKey is false || _uiElements.IsHoldingButaiSideKey() is false)
            {
                _uiElements.NewlyShowLogText("鍵がかかっている");
                return;
            }
            _playerItem.HasButaiSideKey = false;
            _uiElements.IsShowbutaiSideKey = false;
            _uiElements.CupIndex = 1;
            _uiElements.NewlyShowLogText("鍵を開けた");

            _objects.IsWarehouseLockedDoorEnabled = false;
        }
    }
}