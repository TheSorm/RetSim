using RetSim.Items;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RetSim.Data.Items;

namespace RetSimDesktop.Model
{
    public class SelectedGemWrapper
    {
    }

    public class SelectedGemJsonConverter : JsonConverter<SelectedGemWrapper>
    {

        public override SelectedGemWrapper? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.Read();
            reader.Read();
            int redSocketBaseID = reader.GetInt32();
            reader.Read();

            reader.Read();
            int blueSocketBaseID = reader.GetInt32();
            reader.Read();

            reader.Read();
            int yellowSocketBaseID = reader.GetInt32();
            reader.Read();

            reader.Read();
            int metaSocketBaseID = reader.GetInt32();
            reader.Read();

            foreach (var item in AllItems.Values)
            {
                foreach (var socket in item.Sockets)
                {
                    if (socket == null)
                    {
                        continue;
                    }

                    switch (socket.Color)
                    {
                        case SocketColor.Red:
                            socket.SocketedGem = redSocketBaseID == 0 ? null : Gems[redSocketBaseID];
                            break;
                        case SocketColor.Blue:
                            socket.SocketedGem = blueSocketBaseID == 0 ? null : Gems[blueSocketBaseID];
                            break;
                        case SocketColor.Yellow:
                            socket.SocketedGem = yellowSocketBaseID == 0 ? null : Gems[yellowSocketBaseID];
                            break;
                        case SocketColor.Meta:
                            socket.SocketedGem = metaSocketBaseID == 0 ? null : MetaGems[metaSocketBaseID];
                            break;
                    }
                }
            }
            reader.Read();
            reader.Read();

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                int propertyId = int.Parse(reader.GetString());
                reader.Read();
                reader.Read();

                while (reader.TokenType != JsonTokenType.EndObject)
                {
                    string prpertyName = reader.GetString();
                    reader.Read();
                    int value = reader.GetInt32();
                    reader.Read();

                    if (Gems.ContainsKey(value) || MetaGems.ContainsKey(value))
                    {
                        switch (prpertyName)
                        {
                            case "Socket1":
                                if (AllItems[propertyId].Socket1 != null)
                                    AllItems[propertyId].Socket1.SocketedGem = Gems.ContainsKey(value) ? Gems[value] : MetaGems[value];
                                break;
                            case "Socket2":
                                if (AllItems[propertyId].Socket2 != null)
                                    AllItems[propertyId].Socket2.SocketedGem = Gems.ContainsKey(value) ? Gems[value] : MetaGems[value];
                                break;
                            case "Socket3":
                                if (AllItems[propertyId].Socket3 != null)
                                    AllItems[propertyId].Socket3.SocketedGem = Gems.ContainsKey(value) ? Gems[value] : MetaGems[value];
                                break;
                        }
                    }
                }
                reader.Read();
            }
            reader.Read();
            return new SelectedGemWrapper();
        }

        public override void Write(Utf8JsonWriter writer, SelectedGemWrapper value, JsonSerializerOptions options)
        {
            Dictionary<SocketColor, Dictionary<int, int>> gemCount = new();

            foreach (var item in AllItems.Values)
            {
                foreach (var socket in item.Sockets)
                {
                    if (socket == null)
                    {
                        continue;
                    }

                    if (!gemCount.ContainsKey(socket.Color))
                    {
                        gemCount[socket.Color] = new();
                    }

                    if (socket.SocketedGem == null)
                    {
                        if (!gemCount[socket.Color].ContainsKey(0))
                        {
                            gemCount[socket.Color].Add(0, 0);
                        }
                        gemCount[socket.Color][0]++;
                    }
                    else
                    {
                        if (!gemCount[socket.Color].ContainsKey(socket.SocketedGem.ID))
                        {
                            gemCount[socket.Color].Add(socket.SocketedGem.ID, 0);
                        }
                        gemCount[socket.Color][socket.SocketedGem.ID]++;
                    }
                }
            }

            Dictionary<SocketColor, Tuple<int, int>> gemsWithMaxCount = new();
            gemsWithMaxCount[SocketColor.Red] = new(0, 0);
            gemsWithMaxCount[SocketColor.Blue] = new(0, 0);
            gemsWithMaxCount[SocketColor.Yellow] = new(0, 0);
            gemsWithMaxCount[SocketColor.Meta] = new(0, 0);

            foreach (var socket in gemCount)
            {
                foreach (var item in socket.Value)
                {
                    if (gemsWithMaxCount[socket.Key].Item2 < item.Value)
                    {
                        gemsWithMaxCount[socket.Key] = new(item.Key, item.Value);
                    }
                }
            }

            writer.WriteStartObject();

            writer.WriteNumber("RedSocketBase", gemsWithMaxCount[SocketColor.Red].Item1);
            writer.WriteNumber("BlueSocketBase", gemsWithMaxCount[SocketColor.Blue].Item1);
            writer.WriteNumber("YellowSocketBase", gemsWithMaxCount[SocketColor.Yellow].Item1);
            writer.WriteNumber("MetaSocketBase", gemsWithMaxCount[SocketColor.Meta].Item1);

            writer.WriteStartObject("SelectedGems");

            foreach (var item in AllItems.Values)
            {
                if (item.Socket1 != null)
                {
                    int[] socketOutput = new int[3];
                    socketOutput[0] = -1;
                    socketOutput[1] = -1;
                    socketOutput[2] = -1;
                    for (int i = 0; i < item.Sockets.Length; i++)
                    {
                        if (item.Sockets[i] == null)
                        {
                            continue;
                        }
                        if (item.Sockets[i].SocketedGem == null)
                        {
                            if (gemsWithMaxCount[item.Sockets[i].Color].Item1 != 0)
                            {
                                socketOutput[i] = 0;
                            }
                        }
                        else
                        {
                            if (gemsWithMaxCount[item.Sockets[i].Color].Item1 != item.Sockets[i].SocketedGem.ID)
                            {
                                socketOutput[i] = item.Sockets[i].SocketedGem.ID;
                            }
                        }
                    }

                    if (socketOutput[0] != -1 || socketOutput[1] != -1 || socketOutput[2] != -1)
                    {
                        writer.WriteStartObject(item.ID.ToString());
                        if (socketOutput[0] != -1)
                        {
                            writer.WriteNumber("Socket1", socketOutput[0]);
                        }
                        if (socketOutput[1] != -1)
                        {
                            writer.WriteNumber("Socket2", socketOutput[1]);
                        }
                        if (socketOutput[2] != -1)
                        {
                            writer.WriteNumber("Socket3", socketOutput[2]);
                        }
                        writer.WriteEndObject();
                    }
                }
            }
            writer.WriteEndObject();

            writer.WriteEndObject();
        }
    }
}
