namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void ScatterGlassPiece()
        {
            if (_uiElements.GlassShard.IsHolding() is true)
            {
                _uiElements.GlassShard.Release();

                _objects.VillageFarWayScatteredGlassPiece.IsEnabled = true;
            }
        }
    }
}