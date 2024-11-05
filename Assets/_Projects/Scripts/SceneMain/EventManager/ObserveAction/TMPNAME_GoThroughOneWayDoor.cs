using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid TMPNAME_GoThroughOneWayDoor(CancellationToken ct)
        {
            // bool isOpenSide みたいな引数を追加するといいかも

            "一方通行のドアにインタラクトした".Warn();
            await UniTask.Delay(1000, cancellationToken: ct);

            // ログを出したりとかも、この中ですると良さげ
        }
    }
}