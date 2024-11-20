using System;
using UnityEngine;

namespace Main
{
    [Serializable]
    public sealed class PlayerItem
    {
        public bool HasKnife = false;
        public bool HasButaiSideKey = false;

        private const string KEY = nameof(PlayerItem);

        public static void Save(PlayerItem playerItem)
        {
            string jsonData = JsonUtility.ToJson(playerItem);
            ES3.Save(KEY, jsonData);
        }

        public static void Load(out PlayerItem playerItem)
        {
            string jsonData = ES3.Load<string>(KEY);
            playerItem = JsonUtility.FromJson<PlayerItem>(jsonData);
        }
    }
}
