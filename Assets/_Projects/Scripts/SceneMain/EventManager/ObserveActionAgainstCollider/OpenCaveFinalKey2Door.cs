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
            UIItemClass[] keys = _uiElements.GetFinal2DoorKeys();

            if (door.Border.IsIn(_player.Position) is false) return;

            foreach (var key in keys)
            {
                if (key.IsHolding() is false)
                {
                    _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                    continue;
                }

                key.Release();

                door.Unlock(type);
                return;
            }

            if (!door.IsOpenable()) return;

            door.Trigger();
            _uiElements.LogText.ShowAutomatically("鍵を開けた");
        }
    }
}