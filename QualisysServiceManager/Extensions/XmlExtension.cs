﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace QualisysServiceManager.Extensions
{
    public static class XmlExtension
    {
        #region Settings

        public static string GetSetting(this XDocument pObjDocument, string pStrKey)
        {
            if (pObjDocument.SettingExists(pStrKey))
            {
                return pObjDocument.GetSettingByKey(pStrKey).Attribute("value").Value;
            }
            else
            {
                throw new Exception(string.Format("No se encontró la configuración '{0}'.", pStrKey));
            }
        }

        public static void SetSetting(this XDocument pObjDocument, string pStrKey, string pStrValue)
        {
            if (pObjDocument.SettingExists(pStrKey))
            {
                pObjDocument.GetSettingByKey(pStrKey).Attribute("value").SetValue(pStrValue);
            }
            else
            {
                pObjDocument.GetAppSettings().Add(new XElement("add", new XAttribute("key", pStrKey), new XAttribute("value", pStrValue)));
            }
        }

        public static bool SettingExists(this XDocument pObjDocument, string pStrKey)
        {
            return pObjDocument.GetSettings().Where(x => (string)x.Attribute("key") == pStrKey).Count() > 0;
        }

        public static XElement GetAppSettings(this XDocument pObjDocument)
        {
            return pObjDocument.Root.Element("appSettings");
        }

        public static IEnumerable<XElement> GetSettings(this XDocument pObjDocument)
        {
            return pObjDocument.GetAppSettings().Elements("add");
        }

        public static XElement GetSettingByKey(this XDocument pObjDocument, string pStrKey)
        {
            return pObjDocument.GetSettings().FirstOrDefault(x => (string)x.Attribute("key") == pStrKey);
        }

        #endregion

        #region Connection

        public static void SetConnectionStringName(this XDocument pObjDocument, string pStrName)
        {
            pObjDocument.GetConnectionString().Attribute("name").SetValue(pStrName);
        }

        public static void SetConnectionString(this XDocument pObjDocument, string pStrConnectionString)
        {
            pObjDocument.GetConnectionString().Attribute("connectionString").SetValue(pStrConnectionString);
        }

        public static XElement GetConnectionString(this XDocument pObjDocument)
        {
            return pObjDocument.GetConnectionStrings().FirstOrDefault();
        }

        public static IEnumerable<XElement> GetConnectionStrings(this XDocument pObjDocument)
        {
            return pObjDocument.Root.Element("connectionStrings").Elements("add");
        }

        #endregion
    }
}
