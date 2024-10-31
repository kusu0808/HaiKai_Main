using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid CutBigIvy(CancellationToken ct)
        {
            if (_uiElements.IsHoldingDaughterKnife())
            {
                "植物を切り開くSEを1回だけ再生".Warn();
                _objects.DeactivateBigIvy();
                _uiElements.NewlyShowLogText("通れるようになった", EventManagerConst.EventTextShowDuration);
                _uiElements.IsShowDaughterKnife = false;
            }
            else
            {
                _uiElements.NewlyShowLogText("大きな植物が道を遮っている", EventManagerConst.NormalTextShowDuration);
            }
        }
    }
}