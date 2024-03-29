﻿namespace Dragonfly.UmbracoHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Microsoft.AspNetCore.Html;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Web.Common;

    //using Umbraco.Core.Models.PublishedContent;
    //using Umbraco.Web;

    /// <summary>
    /// Provides Functionality useful for Macros
    /// </summary>
    public static class Macros
    {
        private const string ThisClassName = "Dragonfly.UmbracoHelpers.Macros";

        /// <summary>
        /// Return an Object for a Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="DefaultNullValue"></param>
        /// <returns>Default null value - if provided, otherwise, NULL</returns>
        public static object GetSafeParamObject(IDictionary<string, object> MacrosCollection, string Key,
            object DefaultNullValue = null)
        {
            return MacrosCollection[Key] != null ? MacrosCollection[Key] : DefaultNullValue;
        }

        /// <summary>
        /// Return a String for a Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="DefaultNullValue">Default null value - if provided, otherwise, an Empty String</param>
        /// <returns></returns>
        public static string GetSafeParamString(IDictionary<string, object> MacrosCollection, string Key,
            string DefaultNullValue = "")
        {
            return MacrosCollection[Key] != null ? MacrosCollection[Key].ToString() : DefaultNullValue;
        }

        /// <summary>
        /// Return an HtmlString for a Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="DefaultNullValue">Default null value - if provided, otherwise, an Empty String</param>
        /// <returns></returns>
        public static HtmlString GetSafeParamHtmlString(IDictionary<string, object> MacrosCollection, string Key,
            string DefaultNullValue = "")
        {
            var value = MacrosCollection[Key];

            if (value != null && value.ToString() != "")
            {
                var unencodedString = "";
                if (value.ToString().Contains("&lt;"))
                {
                    var encodedString = value.ToString();
                    unencodedString = WebUtility.HtmlDecode(encodedString);
                }
                else
                {
                    unencodedString = value.ToString();
                }

                return new HtmlString(unencodedString);
            }
            else
            {
                return new HtmlString(DefaultNullValue);
            }

        }

        /// <summary>
        /// Return an Int for a Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="DefaultNullValue"></param>
        /// <returns>Default null value - if provided, otherwise, 0</returns>
        public static int GetSafeParamInt(IDictionary<string, object> MacrosCollection, string Key,
            int DefaultNullValue = 0)
        {
            var value = MacrosCollection[Key];

            if (value != null && value.ToString() != "")
            {
                var returnInt = Convert.ToInt32(value);
                return returnInt;
            }
            else
            {
                return DefaultNullValue;
            }

        }

        /// <summary>
        /// Return a Boolean for a Macro Parameter
        /// </summary>
        /// <remarks>Supports Numeric (1) and Text (True and true) values.</remarks>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="DefaultNullValue"></param>
        /// <returns>Default null value - if provided, otherwise, false</returns>
        public static bool GetSafeParamBool(IDictionary<string, object> MacrosCollection, string Key,
            bool DefaultNullValue = false)
        {
            var value = MacrosCollection[Key];

            if (value != null && value.ToString() != "")
            {
                var returnBool = false;

                if (value.ToString() == "1")
                {
                    returnBool = true;
                }
                else if (value.ToString().ToLower() == "true")
                {
                    returnBool = true;
                }

                return returnBool;
            }
            else
            {
                return DefaultNullValue;
            }
        }

        /// <summary>
        /// Returns a collection of IPublishedContent from a MultiContentPicker Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="UmbracoHelper">ex: 'Umbraco'</param>
        /// <returns>IEnumerable&lt;IPublishedContent&gt;</returns>
        public static IEnumerable<IPublishedContent> GetSafeParamMultiContent(
            IDictionary<string, object> MacrosCollection, string Key, UmbracoHelper UmbracoHelper)
        {
            var nodesList = new List<IPublishedContent>();

            var value = MacrosCollection[Key];

            if (value != null && value.ToString() != "")
            {
                var contentIds = value.ToString().Split(',');

                if (contentIds.Any())
                {
                    foreach (var id in contentIds)
                    {
                        var node = UmbracoHelper.Content(id);
                        if (node != null)
                        {
                            nodesList.Add(node);
                        }
                    }
                }
            }

            return nodesList;
        }

        /// <summary>
        /// Returns an IPublishedContent from a ContentPicker Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="UmbracoHelper">ex: 'Umbraco'</param>
        /// <returns>IPublishedContent or NULL</returns>
        public static IPublishedContent GetSafeParamContent(IDictionary<string, object> MacrosCollection, string Key,
            UmbracoHelper UmbracoHelper)
        {
            var value = MacrosCollection[Key];

            if (value != null && value.ToString() != "" && value.ToString() != "0")
            {
                var node = UmbracoHelper.Content(value);
                if (node != null)
                {
                    return node;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a collection of IPublishedContent from a MultiMediaPicker Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="UmbracoHelper">ex: 'Umbraco'</param>
        /// <returns>IEnumerable&lt;IPublishedContent&gt;</returns>
        public static IEnumerable<IPublishedContent> GetSafeParamMultiMedia(
            IDictionary<string, object> MacrosCollection, string Key, UmbracoHelper UmbracoHelper)
        {
            var nodesList = new List<IPublishedContent>();

            var value = MacrosCollection[Key];

            if (value != null && value.ToString() != "")
            {
                var contentIds = value.ToString().Split(',');

                if (contentIds.Any())
                {
                    foreach (var id in contentIds)
                    {
                        var node = UmbracoHelper.Media(id);
                        if (node != null)
                        {
                            nodesList.Add(node);
                        }
                    }
                }
            }

            return nodesList;
        }


        /// <summary>
        /// Returns an IPublishedContent from a MediaPicker Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="UmbracoHelper">ex: 'Umbraco'</param>
        /// <returns>IPublishedContent or NULL</returns>
        public static IPublishedContent GetSafeParamMedia(IDictionary<string, object> MacrosCollection, string Key,
            UmbracoHelper UmbracoHelper)
        {
            var value = MacrosCollection[Key];

            if (value != null && value.ToString() != "")
            {
                return UmbracoHelper.Media(value);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Returns an IPublishedContent from a MediaPicker Macro Parameter
        /// </summary>
        /// <param name="MacrosCollection">ex: 'Model.MacroParameters'</param>
        /// <param name="Key">Parameter alias</param>
        /// <param name="UmbracoHelper">ex: 'Umbraco'</param>
        /// <returns>IPublishedContent or NULL</returns>
        //Obsolete on 2019-11-23
        [Obsolete("Renamed: Use GetSafeParamMedia()")]
        public static IPublishedContent GetSafeParamMediaContent(IDictionary<string, object> MacrosCollection,
            string Key, UmbracoHelper UmbracoHelper)
        {
            return GetSafeParamMedia(MacrosCollection, Key, UmbracoHelper);
        }
    }
}
