using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OnPathWayStepDownSlope()
        {
            if (_player.IsBecameGrounded is false) return;
            _audioSources.GetNew().Raise(_audioClips.SERough.JumpDownStep, SoundType.SERough);
            1.Show();
        }
    }
}
