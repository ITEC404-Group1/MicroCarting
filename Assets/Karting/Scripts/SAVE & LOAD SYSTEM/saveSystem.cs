using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class saveSystem 
{
    public static void savePlayer(player player)
    {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath +"/player.fun";
      FileStream stream = new FileStream(path, FileMode.Create);
      player_data data = new player_data(player);

      formatter.Serialize(stream,data);
      stream.Close();
    }
    public static player_data loadPlayer()
    {
       string path = Application.persistentDataPath +"/player.fun";
       if(File.Exists(path)) 
       {
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream stream = new FileStream(path, FileMode.Open);
           player_data data = formatter.Deserialize(stream) as player_data;
           stream.Close();
           return data;

       }else
       {
           Debug.LogError("save file not fount in"+ path);
           return null;
       }
    }
 
}
