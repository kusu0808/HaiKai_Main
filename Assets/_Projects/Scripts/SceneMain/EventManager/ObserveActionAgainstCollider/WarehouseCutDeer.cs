namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehouseCutDeer()
        {
            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                _objects.Deers.HurtByKnife();
            }
        }
    }
}