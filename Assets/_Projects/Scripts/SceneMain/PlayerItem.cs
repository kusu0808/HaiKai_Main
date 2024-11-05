using System;
using UnityEngine;

namespace Main
{
    [Serializable]
    public sealed class PlayerItem
    {
        public bool HasKnife { get; set; } = false;

        public PlayerItem() { }

        private static readonly string playerItemKey = "PlayerItem";

        public static void Save(PlayerItem playerItem)
        {
            string jsonData = JsonUtility.ToJson(playerItem);
            ES3.Save(playerItemKey, jsonData);
        }

        public static void Load(out PlayerItem playerItem)
        {
            string jsonData = ES3.Load<string>(playerItemKey, string.Empty);
            playerItem = JsonUtility.FromJson<PlayerItem>(jsonData);
        }
    }
}
