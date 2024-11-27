namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenToiletOneWayDoor()
        {
            if (_borders.IsFromUnderStageToShrineWayBorderEnabled is true)
            {
                _uiElements.LogText.ShowAutomatically("開かない");
                return;
            }

            _objects.IsToiletOneWayDoorEnabled = false;
            _uiElements.LogText.ShowAutomatically("ドアを開けた");
        }
    }
}