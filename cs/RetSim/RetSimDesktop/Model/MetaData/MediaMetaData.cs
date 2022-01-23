
using RetSim.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RetSimDesktop
{
    public class MediaMetaData
    {
        public static Dictionary<int, string> GemsToIconName = new();
        public static Dictionary<int, ItemMetaData> ItemsMetaData = new();
        public static void Initialize()
        {
            try
            {
                string jsonString = File.ReadAllText($"Properties\\MetaData\\gemsMetaData.json");
                var gemsMetaData = JsonSerializer.Deserialize<Dictionary<int, string>>(jsonString);
                if (gemsMetaData != null)
                {
                    GemsToIconName = gemsMetaData;
                }
            }
            catch (Exception)
            {
            }

            try
            {
                string jsonString = File.ReadAllText($"Properties\\MetaData\\itemsMetaData.json");
                var itemsMetaData = JsonSerializer.Deserialize<Dictionary<int, ItemMetaData>>(jsonString);
                if (itemsMetaData != null)
                {
                    ItemsMetaData = itemsMetaData;
                }
            }
            catch (Exception)
            {
            }
        }

    }

    public class ItemMetaData
    {
        public string IconFileName { get; set; }
        public List<SocketColor> AlternativeGemOrder { get; set; }
    }

}
