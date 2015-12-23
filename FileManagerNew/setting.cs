using System;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace FileManagerNew
{
    public class setting
    {
        // Our settings
        System.IO.IsolatedStorage.IsolatedStorageFile isf = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();
     //   IsolatedStorage settings;
        System.IO.IsolatedStorage.IsolatedStorageFileStream settingStream;
        IDictionary<string, object> settings = new Dictionary<string, object>(); 
        // The key names of our settings
        const string CheckBoxSettingKeyName = "CheckBoxSetting";
        const string ListBoxSettingKeyName = "ListBoxSetting";
        const string RadioButton1SettingKeyName = "RadioButton1Setting";
        const string RadioButton2SettingKeyName = "RadioButton2Setting";
        const string RadioButton3SettingKeyName = "RadioButton3Setting";
        const string UsernameSettingKeyName = "UsernameSetting";
        const string PasswordSettingKeyName = "PasswordSetting";

        // The default value of our settings
        const bool CheckBoxSettingDefault =false;
        string abc = CultureInfo.CurrentUICulture.Name;
       
     
        //const int ListBoxSettingDefault = 0;
        const bool RadioButton1SettingDefault = true;
        const bool RadioButton2SettingDefault = false;
        const bool RadioButton3SettingDefault = false;
        const string UsernameSettingDefault = "";
        const string PasswordSettingDefault = "";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public setting()
        {
           
            if (!isf.FileExists("setting"))
            {


                using (var fileStream = isf.OpenFile("setting", System.IO.FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fileStream))
                    {
                        sw.Write("Chinese/tCreatTime");
                    }
                }   
            }
            settingStream = isf.OpenFile("setting", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            using (StreamReader reader = new StreamReader(settingStream))
                        {
       
               string  txtfile = reader.ReadToEnd();
                string[] abc = txtfile.Split(new Char[] { '\t' });
                settings.Add(CheckBoxSettingKeyName,abc[0]);
                settings.Add(ListBoxSettingKeyName,abc[1]);
                settings.Add(RadioButton1SettingKeyName,abc[2]);
                settings.Add(RadioButton2SettingKeyName,abc[3]);
                settings.Add(RadioButton3SettingKeyName,abc[4]);
          

                }
            }
            
            
            // Get the settings for this application.
            //settings = IsolatedStorageSettings.ApplicationSettings;
        


        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;
            
            // If the key exists
            if (settings.ContainsKey(Key))
            {
                // If the value has changed
                if (settings[Key] != value)
                {
                    // Store the new value
                    settings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (settings.ContainsKey(Key))
            {
                value = (T)settings[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;

                        value = defaultValue;


                
          
            }
            return value;
        }
        
        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            string saveStr = "";
            foreach (var vl in settings.Values)
            {
                saveStr = saveStr + vl + "/t";
            }
            settingStream = isf.OpenFile("setting", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            using (StreamWriter sw = new StreamWriter(settingStream))
            {
                sw.Write(saveStr);
            }
            
        }


        /// <summary>
        /// Property to get and set a CheckBox Setting Key.
        /// </summary>
        public bool CheckBoxSetting
        {
            get
            {
                return GetValueOrDefault<bool>(CheckBoxSettingKeyName, CheckBoxSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(CheckBoxSettingKeyName, value))
                {
                    Save();
                }
            }
        }


        /// <summary>
        /// Property to get and set a ListBox Setting Key.
        /// </summary>
        public int ListBoxSetting
        {
            get
            {

                int Language;
                  switch (abc)
                {
                    case "zh-CN":
                        Language = 0;
                        break;

                    case "ja-JP":
                        Language = 2;
                        break;
                    default:
                        Language = 1;
                        break;
            }

                                return GetValueOrDefault<int>(ListBoxSettingKeyName, Language);
            }
            set
            {
                if (AddOrUpdateValue(ListBoxSettingKeyName, value))
                {
                    Save();
                }
            }
        }


        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton1Setting
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton1SettingKeyName, RadioButton1SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton1SettingKeyName, value))
                {
                    Save();
                }
            }
        }


        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton2Setting
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton2SettingKeyName, RadioButton2SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton2SettingKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a RadioButton Setting Key.
        /// </summary>
        public bool RadioButton3Setting
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton3SettingKeyName, RadioButton3SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton3SettingKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a Username Setting Key.
        /// </summary>
        public string UsernameSetting
        {
            get
            {
                return GetValueOrDefault<string>(UsernameSettingKeyName, UsernameSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(UsernameSettingKeyName, value))
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// Property to get and set a Password Setting Key.
        /// </summary>
        public string PasswordSetting
        {
            get
            {
                return GetValueOrDefault<string>(PasswordSettingKeyName, PasswordSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(PasswordSettingKeyName, value))
                {
                    Save();
                }
            }
        }
    }
}