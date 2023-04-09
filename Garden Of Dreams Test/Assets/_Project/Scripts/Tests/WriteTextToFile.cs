using System.IO;
using TMPro;
using UnityEngine;

public class WriteTextToFile : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void Click()
    {
        string filepath = Application.persistentDataPath + "/testfile.txt";
        try
        {
            if (File.Exists(filepath))
            {
                File.WriteAllText(filepath, "olo");
            }
            else
            {
                File.WriteAllText(filepath, "ololo");
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
