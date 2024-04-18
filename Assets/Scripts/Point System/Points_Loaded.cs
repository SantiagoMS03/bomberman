using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Points_Loaded : MonoBehaviour
{
    public string Load_Room;
    public string saveDirectory = "Highscore";
    public string saveName = "Highscore_DO_NOT_DELETE";
    public Points_Saved The_Save;
    // Start is called before the first frame update

    public int The_Curr_Highscore()
    {
        if (!Directory.Exists(saveDirectory) || !File.Exists(saveDirectory + "/" + saveName + ".bin"))
        {
            return 0;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();

            // Choosing the saved file to open
            FileStream saveFile = File.Open(saveDirectory + "/" + saveName + ".bin", FileMode.Open);

            // Convert the file data into SaveGameData format for use in game
            Points_Saved loadData = (Points_Saved)formatter.Deserialize(saveFile);
            saveFile.Close();
            return loadData.The_Highscore.Curr_Highscore;
        }
        //SceneManager.LoadScene(Load_Room);
    }
}
