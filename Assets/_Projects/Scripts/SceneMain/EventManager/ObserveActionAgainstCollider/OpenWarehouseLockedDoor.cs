using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenWarehouseLockedDoor(CancellationToken ct)
        {
            if (_playerItem.HasButaiSideKey is false || _uiElements.WarehouseKey.IsHolding() is false)
            {
                _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                return;
            }
            _playerItem.HasButaiSideKey = false;
            _uiElements.WarehouseKey.IsShow = false;
            _uiElements.Cup.Index = 1;

            _objects.IsWarehouseLockedDoorEnabled = false;
            _uiElements.LogText.ShowAutomatically("鍵を開けた");
            _warehouseLookedDoor.PlayDoorOnce(ct).Forget();
        }
    }
}