using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DatabaseConfigWindow : EditorWindow
{
    [MenuItem("Database/Config Window", false, 10)]
    private static void OpenConfigWindow()
    {
        GetWindow<DatabaseConfigWindow>("Database: Config Window");
    }
}
