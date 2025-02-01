using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void CavePickUpKeyInDoorPuzzleSolving()
        {
            if (_objects.KeyInDoorPuzzleSolving.IsEnabled is false) return;

            _uiElements.LogText.ShowAutomatically("失われた鍵を入手した");
            _uiElements.KeyInDoorPuzzleSolving.Obtain();
            _audioSources.GetNew().Raise(_audioClips.SE.ObtainItem, SoundType.SE);
            _objects.KeyInDoorPuzzleSolving.IsEnabled = false;
        }
    }
}