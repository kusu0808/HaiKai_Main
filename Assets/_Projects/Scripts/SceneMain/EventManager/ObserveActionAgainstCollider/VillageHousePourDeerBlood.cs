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
                _objects.KokeshiHead.IsEnabled = false;

                AudioSource audioSource = _audioSources.VillageToiletYatsuKnockDoor;
                if (audioSource != null) audioSource.Stop();
            }
        }
    }
}