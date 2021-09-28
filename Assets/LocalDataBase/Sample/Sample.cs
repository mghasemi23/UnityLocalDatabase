using UnityEngine;
using UnityEngine.UI;
using LocalDataBase;

public class Sample : MonoBehaviour
{
    public InputField KeyInput, ValueInput;
    public Text Text;
    public Toggle toggle;

    public void SaveData()
    {
        DataBase.AddString(KeyInput.text, ValueInput.text);
        Text.text = "Added";
    }

    public void LoadData()
    {
        Text.text = DataBase.GetString(KeyInput.text);
    }

    public void DeleteData()
    {
        DataBase.DeleteField(KeyInput.text);
        Text.text = "Deleted";
    }

    public void Flush()
    {
        DataBase.Flush();
    }

    private void OnApplicationQuit()
    {
        if (toggle.isOn)
        {
            DataBase.Flush();
        }
    }
}
