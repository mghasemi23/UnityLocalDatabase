using UnityEngine;

namespace LocalDataBase
{
    /// <summary>
    /// Scriptable Objcet to Save Configurations
    /// </summary>
    public class ConfigFile : ScriptableObject
    {
        [HideInInspector] [SerializeField] private bool isEncrypted;
        [HideInInspector] [SerializeField] private string fileName;

        /// <summary>
        /// Sets Config File's properties
        /// </summary>
        /// <param name="isEncrypted"></param>
        /// <param name="fileName"></param>
        public void SetProperties(bool isEncrypted, string fileName)
        {
            this.isEncrypted = isEncrypted;
            this.fileName = fileName;
        }

        /// <summary>
        /// Returns Save File Name
        /// </summary>
        public string GetFileName()
        {
            return fileName;
        }

        /// <summary>
        /// Returns true if File is Encrypted
        /// </summary>
        public bool IsEncrypted()
        {
            return isEncrypted;
        }
    }
}
