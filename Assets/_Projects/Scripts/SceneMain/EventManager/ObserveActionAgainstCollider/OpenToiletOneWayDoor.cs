using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenToiletOneWayDoor(CancellationToken ct)
        {
            if (_borders.IsFromUnderStageToShrineWayBorderEnabled is true)
            {
                _uiElements.LogText.ShowAutomatically("開かない");
                return;
            }

            _objects.IsToiletOneWayDoorEnabled = false;
            _uiElements.LogText.ShowAutomatically("ドアを開けた");
            _toiletDoor.PlayDoorOnce(ct).Forget();
        }
    }
}