using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LocalDataBase.Editor
{
    public class DataBaseBrowser : EditorWindow
    {

        #region Methods->private

        [MenuItem("Database/Data Browser", false, 30)]
        private static void OpenBrowserWindow()
        {
            GetWindow<DataBaseBrowser>("Database: Data Browser");
        }

        [MenuItem("Database/Open Data Directory", false, 31)]
        private static void OpenDirectory()
        {
            System.Diagnostics.Process.Start(Application.persistentDataPath);
            //string path = EditorUtility.OpenFilePanel("Overwrite with png", Application.persistentDataPath, "png");
        }

        private void OnGUI()
        {

        }

        #endregion
    }
}
