
using System;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Yatsu : ASerializedPlayerChasingCharacter
    {
        protected override float InitSpeed => 0.5f;
    }
}
