using System.IO;
using TMPro;
using UnityEngine;

public class ReadTextFromFile : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void Click()
    {
        string filepath = Application.persistentDataPath + "/testfile.txt";
        try
        {
            if (File.Exists(filepath))
            {
                text.text = File.ReadAllText(filepath);
            }
            else
            {
                text.text = "lol";
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
