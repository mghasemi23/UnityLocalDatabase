using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LocalDataBase.Editor
{
    public class DataBaseBrowser : EditorWindow
    {
        #region Variables

        private Hashtable _hashTable;
        private Vector2 scrollPos;

        #endregion

        #region Methods-> private

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

            var i = 0;
            _hashTable = DataBase.GetAllData();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            foreach (var dataScheme in _hashTable.Keys)
            {
                var _key = dataScheme.ToString();
                var _value = _hashTable[_key];
                var _type = _value.GetType().ToString().Substring(7);

                var _color = (i % 2) == 0 ? Color.white : Color.gray;
                var _texture = MakeTex(200, 200, _color);
                EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.box) { normal = { background = _texture } });

                GUILayout.Label(i++ + "- Key: " + _key + " ,Value: " + _value + " ,Type: " + _type);

                EditorGUILayout.EndVertical();
            }

            if (i == 0)
            {
                EditorGUILayout.LabelField("Nothing To Show");
            }
            EditorGUILayout.EndScrollView();

            GUILayout.Label("*Data Updates in RunTime");

        }

        private static Texture2D MakeTex(int width, int height, Color col)
        {
            var pix = new Color[width * height];
            for (var i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        #endregion
    }
}
