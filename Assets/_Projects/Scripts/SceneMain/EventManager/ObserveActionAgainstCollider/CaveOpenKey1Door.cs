using System.Collections.Generic;
using General;
using Main.Eventer.Objects.DoorPuzzleSolving;
using Main.Eventer.UIElements;
using Key1DoorType = Main.Eventer.Objects.DoorPuzzleSolving.Key1Door.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private Dictionary<Key1DoorType, UIItemClass> _caveKey1DoorPlacedKeys = null;

        private void CaveOpenKey1Door(Key1DoorType type)
        {
            if (_caveKey1DoorPlacedKeys is null)
            {
                _caveKey1DoorPlacedKeys = new()
                {
                    { Key1DoorType.First, null },
                    { Key1DoorType.Second, null },
                };

                _dispose += () =>
                {
                    _caveKey1DoorPlacedKeys.Clear();
                    _caveKey1DoorPlacedKeys = null;
                };
            }

            Key1Door door = _objects.DoorPuzzleSolving.GetKey1Door(type);
            if (door.IsMoving is true) return;

            if (door.IsOpen is true)
            {
                if (door.Border.IsIn(_player.Position) is false)
                {
                    _uiElements.LogText.ShowAutomatically("届かない...");
                    return;
                }

                UIItemClass placedKey = _caveKey1DoorPlacedKeys[type];
                if (placedKey is null) return;
                _caveKey1DoorPlacedKeys[type] = null;

                placedKey.Obtain();

                door.Trigger();
                _uiElements.LogText.ShowAutomatically("鍵を取った");
                _audioSources.GetNew().Raise(_audioClips.SE.ObtainItem, SoundType.SE);
                _audioSources.GetNew().Raise(_audioClips.SE.OpenIronSlideDoor, SoundType.SE);
            }
            else
            {
                UIItemClass holdingKey = _uiElements.KeysInFinalKey2Door.IsHoldingAny();
                if (holdingKey is null)
                {
                    _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                    _audioSources.GetNew().Raise(_audioClips.SE.OpenIronUnopenableDoor, SoundType.SE);
                    return;
                }

                if (door.Border.IsIn(_player.Position) is false)
                {
                    _uiElements.LogText.ShowAutomatically("届かない...");
                    return;
                }

                if (_caveKey1DoorPlacedKeys[type] is not null) return;
                _caveKey1DoorPlacedKeys[type] = holdingKey;

                holdingKey.Release();

                door.Trigger();
                _uiElements.LogText.ShowAutomatically("鍵を差した");
                _audioSources.GetNew().Raise(_audioClips.SE.KeyOpen, SoundType.SE);
                _audioSources.GetNew().Raise(_audioClips.SE.OpenIronSlideDoor, SoundType.SE);
            }
        }
    }
}