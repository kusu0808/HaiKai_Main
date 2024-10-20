using Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static class SaveLoadManager
    {
        public static void SavePlayerItem(PlayerItem playerItem, string key)
        {
            ES3.Save(key + "_hasItem1", playerItem.HasItem1);
            ES3.Save(key + "_hasItem2", playerItem.HasItem2);
            ES3.Save(key + "_hasItem3", playerItem.HasItem3);
        }

        public static void LoadPlayerItem(PlayerItem playerItem, string key)
        {
            bool hasItem1 = ES3.Load<bool>(key + "_hasItem1", false);
            bool hasItem2 = ES3.Load<bool>(key + "_hasItem2", false);
            bool hasItem3 = ES3.Load<bool>(key + "_hasItem3", false);

            playerItem.SetHasItem1(hasItem1);
            playerItem.SetHasItem2(hasItem2);
            playerItem.SetHasItem3(hasItem3);
        }
    }
}
