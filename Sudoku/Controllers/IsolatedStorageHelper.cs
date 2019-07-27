using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace Sudoku.Controllers
{
    internal static class IsolatedStorageHelper
    {
        public static T GetObject<T>(string key)
        {

            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                string serializedObject = IsolatedStorageSettings.ApplicationSettings[key].ToString();
                return Deserialize<T>(serializedObject);
            }

            return default(T);
        }

        public static void SaveObject<T>(string key, T objectToSave)
        {
            string serializedObject = Serialize(objectToSave);
            IsolatedStorageSettings.ApplicationSettings[key] = serializedObject;

            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static void DeleteObject(string key)
        {
            IsolatedStorageSettings.ApplicationSettings.Remove(key);
        }

        private static string Serialize(object objectToSerialize)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer ser = new XmlSerializer(objectToSerialize.GetType());
                ser.Serialize(sw, objectToSerialize);

                return sw.ToString();
            }
        }

        private static T Deserialize<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return default(T);

            using (XmlReader reader = XmlReader.Create(new StringReader(jsonString)))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    return (T)ser.Deserialize(reader);
                }
                catch (Exception)
                {
                    return default(T);
                }

            }
        }
    }
}
