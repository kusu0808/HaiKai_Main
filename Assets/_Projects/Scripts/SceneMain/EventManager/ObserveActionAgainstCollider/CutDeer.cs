namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void CutDeer()
        {
            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                _objects.Deers.HurtByKnife();
            }
        }
    }
}