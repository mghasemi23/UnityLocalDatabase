using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace LocalDataBase
{
    public static class DataBase
    {
        #region Varioables

        private static readonly Hashtable prefsHashtable = new Hashtable();
        private static bool hashTableChanged = false;
        private static string serializedOutput = "";
        private static string serializedInput = "";
        private const string PARAMETERS_SEPERATOR = ";";
        private const string KEY_VALUE_SEPERATOR = ":";
        private static string[] seperators = new string[] { PARAMETERS_SEPERATOR, KEY_VALUE_SEPERATOR };
        private static string fileName;
        private static byte[] bytes = ASCIIEncoding.ASCII.GetBytes(SystemInfo.deviceUniqueIdentifier.Substring(0, 8));
        private static bool wasEncrypted = false;
        private static bool securityModeEnabled = false;
        private static ConfigFile configFile;

        #endregion

        #region Constuct

        static DataBase()
        {
            LoadConfigFile();

            StreamReader fileReader = null;
            if (File.Exists(fileName))
            {
                fileReader = new StreamReader(fileName);
                serializedInput = wasEncrypted ? Decrypt(fileReader.ReadToEnd()) : fileReader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(serializedInput))
            {
                if (serializedInput.Length > 0 && serializedInput[serializedInput.Length - 1] == '\n')
                {
                    serializedInput = serializedInput.Substring(0, serializedInput.Length - 1);

                    if (serializedInput.Length > 0 && serializedInput[serializedInput.Length - 1] == '\r')
                    {
                        serializedInput = serializedInput.Substring(0, serializedInput.Length - 1);
                    }
                }

                Deserialize();
            }

            if (fileReader != null)
            {
                fileReader.Close();
            }
        }

        #endregion

        #region Methods-> Public

        /// <summary>
        /// Save String in DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="String"></param>
        public static void AddString(string key, string value)
        {
            if (!prefsHashtable.ContainsKey(key))
            {
                prefsHashtable.Add(key, value);
            }
            else
            {
                prefsHashtable[key] = value;
            }

            hashTableChanged = true;
        }

        /// <summary>
        /// Save Integer in DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Integer"></param>
        public static void AddInt(string key, int value)
        {
            if (!prefsHashtable.ContainsKey(key))
            {
                prefsHashtable.Add(key, value);
            }
            else
            {
                prefsHashtable[key] = value;
            }

            hashTableChanged = true;
        }

        /// <summary>
        /// Save Float in DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Float"></param>
        public static void AddFloat(string key, float value)
        {
            if (!prefsHashtable.ContainsKey(key))
            {
                prefsHashtable.Add(key, value);
            }
            else
            {
                prefsHashtable[key] = value;
            }

            hashTableChanged = true;
        }

        /// <summary>
        /// Save Bool in DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Bool"></param>
        public static void AddBool(string key, bool value)
        {
            if (!prefsHashtable.ContainsKey(key))
            {
                prefsHashtable.Add(key, value);
            }
            else
            {
                prefsHashtable[key] = value;
            }

            hashTableChanged = true;
        }

        /// <summary>
        /// Save Long in DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Long"></param>
        public static void AddLong(string key, long value)
        {
            if (!prefsHashtable.ContainsKey(key))
            {
                prefsHashtable.Add(key, value);
            }
            else
            {
                prefsHashtable[key] = value;
            }

            hashTableChanged = true;
        }

        /// <summary>
        /// Returns String form DB, If unavailabe Rrturns Null 
        /// </summary>
        /// <param name="key"></param>
        public static string GetString(string key)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return prefsHashtable[key].ToString();
            }

            return null;
        }

        /// <summary>
        /// Returns String from DB, If unavailabe Adds a string with given key and defaultValue to DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        public static string GetString(string key, string defaultValue)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return prefsHashtable[key].ToString();
            }
            else
            {
                prefsHashtable.Add(key, defaultValue);
                hashTableChanged = true;
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns Ineger from DB, If unavailabe Rrturns 0
        /// </summary>
        /// <param name="key"></param>
        public static int GetInt(string key)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return (int)prefsHashtable[key];
            }
            return 0;
        }

        /// <summary>
        /// Returns Integer from DB, If unavailabe Adds a Integer with given key and defaultValue to DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        public static int GetInt(string key, int defaultValue)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return (int)prefsHashtable[key];
            }
            else
            {
                prefsHashtable.Add(key, defaultValue);
                hashTableChanged = true;
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns Long from DB,  If unavailabe Rrturns 0
        /// </summary>
        /// <param name="key"></param>
        public static long GetLong(string key)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return (long)prefsHashtable[key];
            }

            return 0;
        }

        /// <summary>
        /// Returns Long from DB, If unavailabe Adds a Long with given key and defaultValue to DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        public static long GetLong(string key, long defaultValue)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return (long)prefsHashtable[key];
            }
            else
            {
                prefsHashtable.Add(key, defaultValue);
                hashTableChanged = true;
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns Float from DB, If unavailabe Rrturns 0.0f
        /// </summary>
        /// <param name="key"></param>
        public static float GetFloat(string key)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return (float)prefsHashtable[key];
            }

            return 0.0f;
        }

        /// <summary>
        /// Returns Float from DB, If unavailabe Adds a Float with given key and defaultValue to DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param
        public static float GetFloat(string key, float defaultValue)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return (float)prefsHashtable[key];
            }
            else
            {
                prefsHashtable.Add(key, defaultValue);
                hashTableChanged = true;
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns Bool from DB, If unavailabe Rrturns flase
        /// </summary>
        /// <param name="key"></param>
        public static bool GetBool(string key)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return (bool)prefsHashtable[key];
            }

            return false;
        }

        /// <summary>
        /// Returns Bool from DB, If unavailabe Adds a Bool with given key and defaultValue to DB
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        public static bool GetBool(string key, bool defaultValue)
        {
            if (prefsHashtable.ContainsKey(key))
            {
                return (bool)prefsHashtable[key];
            }
            else
            {
                prefsHashtable.Add(key, defaultValue);
                hashTableChanged = true;
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns All Data in HashTable
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetAllData()
        {
            return prefsHashtable;
        }

        /// <summary>
        /// Checks if Given Key is Available in DB
        /// </summary>
        /// <param name="key"></param>
        public static bool HasKey(string key)
        {
            return prefsHashtable.ContainsKey(key);
        }

        /// <summary>
        /// Delete a field from DB
        /// </summary>
        /// <param name="key"></param>
        public static void DeleteField(string key)
        {
            prefsHashtable.Remove(key);
        }

        /// <summary>
        /// Delete all Field from DB
        /// </summary>
        public static void DeleteAllFields()
        {
            prefsHashtable.Clear();
        }

        /// <summary>
        /// Check if Data is Encrypted or Not
        /// </summary>
        public static bool WasReadPlayerPrefsFileEncrypted()
        {
            return wasEncrypted;
        }

        /// <summary>
        /// Store All Data on Disk
        /// </summary>
        public static void Flush()
        {
            if (hashTableChanged)
            {
                Serialize();

                string output = (securityModeEnabled ? Encrypt(serializedOutput) : serializedOutput);

                StreamWriter fileWriter = null;

                File.Delete(fileName);
                fileWriter = File.CreateText(fileName);

                if (fileWriter == null)
                {
                    Debug.LogWarning("PlayerPrefs::Flush() opening file for writing failed: " + fileName);
                    return;
                }

                fileWriter.Write(output);

                fileWriter.Close();

                serializedOutput = "";
            }
        }

        #endregion

        #region Methods-> Private

        public static void EnableEncryption(bool enabled)
        {
            securityModeEnabled = enabled;
        }

        private static void Serialize()
        {
            IDictionaryEnumerator myEnumerator = prefsHashtable.GetEnumerator();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool firstString = true;
            while (myEnumerator.MoveNext())
            {
                //if(serializedOutput != "")
                if (!firstString)
                {
                    sb.Append(" ");
                    sb.Append(PARAMETERS_SEPERATOR);
                    sb.Append(" ");
                }
                sb.Append(EscapeNonSeperators(myEnumerator.Key.ToString(), seperators));
                sb.Append(" ");
                sb.Append(KEY_VALUE_SEPERATOR);
                sb.Append(" ");
                sb.Append(EscapeNonSeperators(myEnumerator.Value.ToString(), seperators));
                sb.Append(" ");
                sb.Append(KEY_VALUE_SEPERATOR);
                sb.Append(" ");
                sb.Append(myEnumerator.Value.GetType());
                firstString = false;
            }
            serializedOutput = sb.ToString();
        }

        private static void Deserialize()
        {
            string[] parameters = serializedInput.Split(new string[] { " " + PARAMETERS_SEPERATOR + " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in parameters)
            {
                string[] parameterContent = parameter.Split(new string[] { " " + KEY_VALUE_SEPERATOR + " " }, StringSplitOptions.None);
                try
                {
                    prefsHashtable.Add(DeEscapeNonSeperators(parameterContent[0], seperators), GetTypeValue(parameterContent[2], DeEscapeNonSeperators(parameterContent[1], seperators)));

                }
                catch (IndexOutOfRangeException)
                {
                    Debug.LogWarning("Data is Encrypted, Change Encrypt Setting or Delete Previous Database");
                    return;
                }

                if (parameterContent.Length > 3)
                {
                    Debug.LogWarning("PlayerPrefs::Deserialize() parameterContent has " + parameterContent.Length + " elements");
                }
            }
        }

        public static string EscapeNonSeperators(string inputToEscape, string[] seperators)
        {
            inputToEscape = inputToEscape.Replace("\\", "\\\\");

            for (int i = 0; i < seperators.Length; ++i)
            {
                inputToEscape = inputToEscape.Replace(seperators[i], "\\" + seperators[i]);
            }

            return inputToEscape;
        }

        public static string DeEscapeNonSeperators(string inputToDeEscape, string[] seperators)
        {

            for (int i = 0; i < seperators.Length; ++i)
            {
                inputToDeEscape = inputToDeEscape.Replace("\\" + seperators[i], seperators[i]);
            }

            inputToDeEscape = inputToDeEscape.Replace("\\\\", "\\");

            return inputToDeEscape;
        }

        private static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                return "";
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        private static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                return "";
            }
            try
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);
                return reader.ReadToEnd();
            }
            catch (FormatException)
            {
                Debug.LogWarning("Data is NOT Encrypted, Change Encrypt Setting or Delete Previous Database");
                return "";
            }
        }

        private static object GetTypeValue(string typeName, string value)
        {
            if (typeName == "System.String")
            {
                return (object)value.ToString();
            }
            if (typeName == "System.Int32")
            {
                return Convert.ToInt32(value);
            }
            if (typeName == "System.Boolean")
            {
                return Convert.ToBoolean(value);
            }
            if (typeName == "System.Single")
            { //float
                return Convert.ToSingle(value);
            }
            if (typeName == "System.Int64")
            { //long
                return Convert.ToInt64(value);
            }
            else
            {
                Debug.LogError("Unsupported type: " + typeName);
            }

            return null;
        }

        private static void LoadConfigFile()
        {
            configFile = Resources.Load<ConfigFile>("Config");
            fileName = Application.persistentDataPath + "\\" + configFile.GetFileName() + ".LDB";
            wasEncrypted = configFile.IsEncrypted();
            EnableEncryption(wasEncrypted);
        }

        #endregion
    }
}