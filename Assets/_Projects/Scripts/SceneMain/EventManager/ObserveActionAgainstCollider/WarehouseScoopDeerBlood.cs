namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehouseScoopDeerBlood()
        {
            if (_uiElements.Cup.IsHolding() is true)
            {
                _uiElements.LogText.ShowAutomatically("コップが血て満たされた");
                _uiElements.Cup.Release();
                _uiElements.CupFilledWithBlood.Obtain();
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("何か血をくめるものはないだろうか？");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("何か血をくめるものはないだろうか？");
            }
        }
    }
}