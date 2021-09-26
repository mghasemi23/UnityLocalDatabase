using System.Text;
using UnityEngine;
using UnityEngine.UI;
using LocalDataBase;
public class Test : MonoBehaviour
{
    public Button testButton;

    void Start()
    {
        testButton.onClick.AddListener(Listener);
    }

    private void Listener()
    {
        Debug.Log("Clicked");
        CustomDataBase.SetInt("ghasem", 239);
        CustomDataBase.Flush();
    }
}
