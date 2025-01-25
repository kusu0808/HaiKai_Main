namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageFarWayScatterGlassPiece()
        {
            if (_uiElements.GlassShard.IsHolding() is true)
            {
                _uiElements.GlassShard.Release();
                _uiElements.LogText.ShowAutomatically("ガラスを巻いた");

                _objects.VillageFarWayScatteredGlassPiece.IsEnabled = true;
                _objects.VillageFarWayCanScatterGlassPieceArea.IsEnabled = false;
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("何かが割られている");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("ガラスが散らばっている");
            }
        }
    }
}