using Type = Main.Eventer.Objects.DaughterChainClass.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void CutChain(Type type)
        {
            var chain = _objects.DaughterChain;

            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                chain.Cut(type);

                if (chain.IsAllCut() is false) return;

                _daughter.SpawnHere(_points.ShrineDaughterSpawnPoint);
            }
        }
    }
}