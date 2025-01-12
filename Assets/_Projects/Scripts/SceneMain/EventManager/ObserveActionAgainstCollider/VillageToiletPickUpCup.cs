namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageToiletPickUpCup()
        {
            if (_objects.ToiletCup.IsEnabled is false) return;

            _uiElements.LogText.ShowAutomatically("コップを入手した");
            _uiElements.Cup.Obtain();
            _objects.ToiletCup.IsEnabled = false;
        }
    }
}