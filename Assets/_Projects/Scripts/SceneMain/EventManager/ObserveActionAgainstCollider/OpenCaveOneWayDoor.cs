using Main.Eventer.Objects.DoorPuzzleSolving;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OpenCaveOneWayDoor()
        {
            OneWayDoor door = _objects.DoorPuzzleSolving.OneWay;

            if (door.Border.IsIn(_player.Position) is false) return;

            door.Trigger();
            _uiElements.LogText.ShowAutomatically("ドアを開けた");
        }
    }
}