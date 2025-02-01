using General;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageHousePourDeerBlood()
        {
            if (_uiElements.CupFilledWithBlood.IsHolding() is true)
            {
                _uiElements.LogText.ShowAutomatically("こけしの仕掛けを解いた");
                _uiElements.CupFilledWithBlood.Release();
                _uiElements.Cup.Obtain();
                _audioSources.GetNew().Raise(_audioClips.SE.PourDeerBlood, SoundType.SE);
                _objects.KokeshiHead.IsEnabled = false;

                AudioSource audioSource = _audioSources.VillageToiletYatsuKnockDoor;
                if (audioSource != null) audioSource.Stop();
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("このこけしは血に濡れていないようだ");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("このこけしは血に濡れていないようだ");
            }
        }
    }
}