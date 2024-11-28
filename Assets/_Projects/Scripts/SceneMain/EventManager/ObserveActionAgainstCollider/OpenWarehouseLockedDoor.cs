using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenWarehouseLockedDoor(CancellationToken ct)
        {
            if (_playerItem.HasButaiSideKey is false || _uiElements.IsHoldingButaiSideKey() is false)
            {
                _uiElements.NewlyShowLogText("鍵がかかっている");
                return;
            }
            _playerItem.HasButaiSideKey = false;
            _uiElements.IsShowbutaiSideKey = false;
            _uiElements.CupIndex = 1;

            _objects.IsWarehouseLockedDoorEnabled = false;
            _uiElements.NewlyShowLogText("鍵を開けた");
            _warehouseLookedDoor.PlayDoorOnce(ct).Forget();
        }
    }
}