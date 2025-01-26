namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehouseScoopDeerBlood()
        {
            if (_objects.Deers.HasBeenHurtByKnife is false) return;

            if (_hasScoupedDeerBlood is true)
            {
                _uiElements.LogText.ShowAutomatically("もうくむ必要はない");
            }
            else if (_uiElements.Cup.IsHolding() is true)
            {
                _uiElements.LogText.ShowAutomatically("コップが血で満たされた");
                _uiElements.Cup.Release();
                _uiElements.CupFilledWithBlood.Obtain();
                _hasScoupedDeerBlood = true;
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