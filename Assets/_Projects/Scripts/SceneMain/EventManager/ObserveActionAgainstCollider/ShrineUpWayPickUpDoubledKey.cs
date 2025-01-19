namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void ShrineUpWayPickUpDoubledKey()
        {
            if (_objects.WarehouseKeyDoubled.IsEnabled is false) return;

            _uiElements.LogText.ShowAutomatically("2つの鍵を入手した");
            _uiElements.WarehouseKeyDoubled.Obtain();
            _objects.WarehouseKeyDoubled.IsEnabled = false;
        }
    }
}