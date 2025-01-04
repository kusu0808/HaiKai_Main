using Main.Eventer.Objects.DoorPuzzleSolving;
using Main.Eventer.UIElements;
using DoorType = Main.Eventer.Objects.DoorPuzzleSolving.DoorPuzzleSolvingClass.FinalKey2DoorType;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenCaveFinalKey2Door(DoorType type)
        {
            FinalKey2Door door = _objects.DoorPuzzleSolving.FinalKey2;
            UIItemClass key = _uiElements.GetFinalKey(type);

            if (door.Border.IsIn(_player.Position) is false) return;

            if (key.IsHolding() is false)
            {
                _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                return;
            }

            key.Release();

            door.Unlock(true);

            if (!door.IsOpenable()) return;

            door.Trigger();
            _uiElements.LogText.ShowAutomatically("鍵を開けた");
        }
    }
}