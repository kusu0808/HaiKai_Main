namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehouseOpenOneWayDoor()
        {
            if (_borders.IsFromUnderStageToShrineWayBorderEnabled is true)
            {
                _uiElements.LogText.ShowAutomatically("開かない");
                return;
            }

            _objects.WarehouseOneWayDoor.Trigger();
            _uiElements.LogText.ShowAutomatically("ドアを開けた");
        }
    }
}