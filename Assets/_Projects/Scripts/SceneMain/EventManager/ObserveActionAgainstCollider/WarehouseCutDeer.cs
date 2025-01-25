namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehouseCutDeer()
        {
            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                _objects.Deers.HurtByKnife();
                _uiElements.LogText.ShowAutomatically("温かい血が迸った");
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("鋭利なものが必要だ");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("鹿が吊るされている");
            }
        }
    }
}