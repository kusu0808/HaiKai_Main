namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageToiletOpenOneWayDoor()
        {
            if (_borders.IsFromUnderStageToShrineWayBorderEnabled is true)
            {
                _uiElements.LogText.ShowAutomatically("開かない");
                return;
            }

            _objects.ToiletOneWayDoor.Trigger();
            _uiElements.LogText.ShowAutomatically("ドアを開けた");
        }
    }
}