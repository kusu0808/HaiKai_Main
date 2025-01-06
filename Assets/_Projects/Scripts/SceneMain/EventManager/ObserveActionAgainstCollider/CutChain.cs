using Type = Main.Eventer.Objects.DaughterChainClass.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void CutChain(Type type)
        {
            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                _objects.DaughterChain.Cut(type);
            }
        }
    }
}