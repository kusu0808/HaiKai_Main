using System.Collections.Generic;
using General;
using Main.Eventer.Objects.DoorPuzzleSolving;
using Main.Eventer.UIElements;
using FinalKey2DoorType = Main.Eventer.Objects.DoorPuzzleSolving.FinalKey2Door.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private Dictionary<FinalKey2DoorType, UIItemClass> _caveFinalKey2DoorPlacedKeys = null;

        private void CaveOpenFinalKey2Door(FinalKey2DoorType type)
        {
            if (_caveFinalKey2DoorPlacedKeys is null)
            {
                _caveFinalKey2DoorPlacedKeys = new()
                {
                    { FinalKey2DoorType.Left, null },
                    { FinalKey2DoorType.Right, null },
                };

                _dispose += () =>
                {
                    _caveFinalKey2DoorPlacedKeys.Clear();
                    _caveFinalKey2DoorPlacedKeys = null;
                };
            }

            FinalKey2Door door = _objects.DoorPuzzleSolving.FinalKey2;
            if (door.IsMoving(type) is true) return;

            if (door.IsOpenBoth is true) return;

            if (door.IsOpen(type) is true)
            {
                if (door.Border.IsIn(_player.Position) is false)
                {
                    _uiElements.LogText.ShowAutomatically("届かない...");
                    return;
                }

                UIItemClass placedKey = _caveFinalKey2DoorPlacedKeys[type];
                if (placedKey is null) return;
                _caveFinalKey2DoorPlacedKeys[type] = null;

                placedKey.Obtain();
                door.SetKey(type, false);

                _uiElements.LogText.ShowAutomatically("鍵を取った");
                _audioSources.GetNew().Raise(_audioClips.SE.ObtainItem, SoundType.SE);
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

                if (_caveFinalKey2DoorPlacedKeys[type] is not null) return;
                _caveFinalKey2DoorPlacedKeys[type] = holdingKey;

                holdingKey.Release();
                door.SetKey(type, true);

                if (door.IsOpenBoth)
                {
                    door.Trigger();
                    _audioSources.GetNew().Raise(_audioClips.SE.KeyOpen, SoundType.SE);
                    _audioSources.GetNew().Raise(_audioClips.SE.OpenIronKannonDoor, SoundType.SE);
                }
                else
                {
                    _uiElements.LogText.ShowAutomatically("鍵を差した");
                    _audioSources.GetNew().Raise(_audioClips.SE.KeyOpen, SoundType.SE);
                }
            }
        }
    }
}