using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void CutBigIvy()
        {
            if (_uiElements.IsHoldingDaughterKnife())
            {
                "植物を切り開くSEを1回だけ再生".Warn();
                _objects.DeactivateBigIvy();
                _uiElements.NewlyShowLogText("通れるようになった", EventManagerConst.EventTextShowDuration);
            }
            else
            {
                _uiElements.NewlyShowLogText("大きな植物が道を遮っている", EventManagerConst.NormalTextShowDuration);
            }
        }
    }
}