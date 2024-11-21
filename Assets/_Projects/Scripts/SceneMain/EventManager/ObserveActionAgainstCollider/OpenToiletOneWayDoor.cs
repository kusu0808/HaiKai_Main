namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenToiletOneWayDoor()
        {
            if (_borders.IsFromUnderStageToShrineWayBorderEnabled is true)
            {
                _uiElements.NewlyShowLogText("開かない");
                return;
            }

            _objects.IsToiletOneWayDoorEnabled = false;
            _uiElements.NewlyShowLogText("ドアを開けた");

            if (_yatsuKnockToiletDoorAudioSource == null) return;
            _yatsuKnockToiletDoorAudioSource.Pause();
        }
    }
}