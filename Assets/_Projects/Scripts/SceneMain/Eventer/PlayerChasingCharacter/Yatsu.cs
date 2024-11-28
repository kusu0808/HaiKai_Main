
using System;

namespace Main.Eventer.PlayerChasingCharacter
{
    [Serializable]
    public sealed class Yatsu : APlayerChasingCharacter
    {
        protected override float InitSpeed => 0.5f;
    }
}
