using UnityEngine;
using UnityEditor;

namespace LocalDataBase.Editor
{
    public class DatabaseConfigWindow : EditorWindow
    {

        #region Variables

        private string fileName = "customPrefs";
        private bool isEncrypted;
        private ConfigFile configFile;

        #endregion

        #region Methods->private

        /// <summary>
        /// Add Button to Unity Toolbar
        /// </summary>
        [MenuItem("Database/Config Window", false, 10)]
        private static void OpenConfigWindow()
        {
            GetWindow<DatabaseConfigWindow>("Database: Config Window");
        }

        /// <summary>
        /// Draws Config window
        /// </summary>
        private void OnGUI()
        {
            configFile = GetConfigFile();
            if (!configFile)
            {
                configFile = ScriptableObject.CreateInstance<ConfigFile>();
                AssetDatabase.CreateAsset(configFile, "Assets/Resources/Config.asset");
                EditorUtility.SetDirty(configFile);
                AssetDatabase.SaveAssets();
            }
            else
            {
                var _fileName = configFile.GetFileName();
                if (!string.IsNullOrEmpty(_fileName))
                    fileName = _fileName;

                isEncrypted = configFile.IsEncrypted();

                SaveConfigChanges();
            }

            EditorGUI.BeginChangeCheck();

            fileName = EditorGUILayout.TextField("File Name: ", fileName);
            isEncrypted = EditorGUILayout.Toggle("Encrypt Data: ", isEncrypted);

            if (EditorGUI.EndChangeCheck())
            {
                SaveConfigChanges();
            }

            if (GUILayout.Button("Reset To Default"))
            {
                fileName = "customPrefs";
                isEncrypted = false;

                SaveConfigChanges();
            }
        }

        /// <summary>
        /// Save Config Properties into Scriptable Object
        /// </summary>
        private void SaveConfigChanges()
        {
            configFile.SetProperties(isEncrypted, fileName);
        }


        /// <summary>
        /// Returns Config Scriptable Object
        /// </summary>
        /// <returns>Config File</returns>
        private static ConfigFile GetConfigFile()
        {
            return Resources.Load<ConfigFile>("Config");
        }

        #endregion
    }
}
