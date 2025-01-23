using Main.Eventer.Objects.DoorPuzzleSolving;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private bool _hasTriggeredCaveOneWayDoor = false;

        private void CaveOpenOneWayDoor()
        {
            if (_hasTriggeredCaveOneWayDoor is true) return;

            OneWayDoor door = _objects.DoorPuzzleSolving.OneWay;
            if (door.IsMoving is true) return;

            if (door.Border.IsIn(_player.Position) is false)
            {
                _uiElements.LogText.ShowAutomatically("届かない...");
                return;
            }

            _hasTriggeredCaveOneWayDoor = true;
            door.Trigger();
            _uiElements.LogText.ShowAutomatically("ドアを開けた");
        }
    }
}