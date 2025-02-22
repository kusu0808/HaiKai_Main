using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehouseOpenOneWayDoor()
        {
            if (_borders.IsFromUnderStageToShrineWayBorderEnabled is true)
            {
                _uiElements.LogText.ShowAutomatically("開かない");
                _audioSources.GetNew().Raise(_audioClips.SE.OpenWoodUnopenableDoor, SoundType.SE);
                return;
            }

            _objects.WarehouseOneWayDoor.Trigger();
            _uiElements.LogText.ShowAutomatically("ドアを開けた");
            _audioSources.GetNew().Raise(_audioClips.SE.OpenWoodSlideDoor, SoundType.SE);
        }
    }
}