using General;
using System;
using System.Text;

namespace Main
{
    public sealed class PlayerItem
    {
        private bool _hasItem1 = false;
        private bool _hasItem2 = false;
        private bool _hasItem3 = false;

        // セッタ未実装
        public bool HasItem1 => _hasItem1;
        public bool HasItem2 => _hasItem2;
        public bool HasItem3 => _hasItem3;

        public PlayerItem() { }

        public string Write()
        {
            StringBuilder sb = new();
            sb.Append(_hasItem1 ? "1" : "0");
            sb.Append(_hasItem2 ? "1" : "0");
            sb.Append(_hasItem3 ? "1" : "0");
            return sb.ToString();
        }

        public void Read(string id)
        {
            try
            {
                if (id.Length != 3) throw new Exception();

                SetOrThrow(ref _hasItem1, id[0]);
                SetOrThrow(ref _hasItem2, id[1]);
                SetOrThrow(ref _hasItem3, id[2]);
            }
            catch (Exception)
            {
                "読み込み失敗".Warn();
            }

            static void SetOrThrow(ref bool flag, char c) => flag = c switch
            {
                '1' => true,
                '0' => false,
                _ => throw new Exception()
            };
        }
    }
}