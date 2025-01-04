using Main.Eventer.Objects.DoorPuzzleSolving;
using DoorType = Main.Eventer.Objects.DoorPuzzleSolving.DoorPuzzleSolvingClass.Key1DoorType;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenCaveKey1Door(DoorType type)
        {
            Key1Door door = _objects.DoorPuzzleSolving.GetKey1Door(type);

            if (door.Border.IsIn(_player.Position) is false) return;

            if (door.IsOpen is true)
            {
                _uiElements.WarehouseKey.Obtain();

                door.Trigger();
                _uiElements.LogText.ShowAutomatically("鍵を閉めた");
            }
            else
            {
                if (_uiElements.WarehouseKey.IsHolding() is false)
                {
                    _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                    return;
                }

                _uiElements.WarehouseKey.Release();

                door.Trigger();
                _uiElements.LogText.ShowAutomatically("鍵を開けた");
            }
        }
    }
}