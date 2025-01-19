namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void PathWayPickUpDaughterKnife()
        {
            if (_objects.DaughterKnife.IsEnabled is false) return;

            _uiElements.LogText.ShowAutomatically("娘のナイフを入手した");
            _uiElements.DaughterKnife.Obtain();
            _objects.DaughterKnife.IsEnabled = false;
        }
    }
}