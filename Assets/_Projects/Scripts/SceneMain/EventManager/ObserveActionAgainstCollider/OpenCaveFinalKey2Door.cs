using Main.Eventer.Objects.DoorPuzzleSolving;
using Main.Eventer.UIElements;
using FinalKey2DoorType = Main.Eventer.Objects.DoorPuzzleSolving.FinalKey2Door.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenCaveFinalKey2Door(FinalKey2DoorType type)
        {
            FinalKey2Door door = _objects.DoorPuzzleSolving.FinalKey2;
            UIItemClass[] keys = _uiElements.KeysInFinalKey2Door;

            if (door.Border.IsIn(_player.Position) is false) return;
            if (door.GetIsKeyInDoorInserted(type) is true) return;

            foreach (var key in keys)
            {
                if (key.IsHolding() is true)
                {
                    key.Release();

                    var isOpenable = door.Trigger(type);
                    if (isOpenable is true) _uiElements.LogText.ShowAutomatically("鍵を開けた");
                    else _uiElements.LogText.ShowAutomatically("鍵を差し込んだ");

                    return;
                }
            }

            _uiElements.LogText.ShowAutomatically("鍵がかかっている");
        }
    }
}