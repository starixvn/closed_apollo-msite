// --------------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
// </history>

namespace Fanex.Maintenance.Core.Web
{
    using System;
    using System.Globalization;
    using System.Web;

    /// <summary>
    /// HTML helper extension.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region DateTime

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent string
        /// representation using the specified format.
        /// </summary>
        /// <param name="value">DateTime value.</param>
        /// <param name="dateFormat">Date format.</param>
        /// <param name="timeFormat">Time format.</param>
        /// <returns>HTML string.</returns>
        public static IHtmlString ToFullDateString(
            this DateTime value,
            string dateFormat = "MMM dd, yyyy",
            string timeFormat = "HH:mm")
        {
            return new HtmlString(value.ToString(dateFormat) + " - " + value.ToString(timeFormat).Replace(':', 'h') + "'");
        }

        /// <summary>
        /// Convert the Date value and combine with time value to specific format for showing on view.
        /// </summary>
        /// <param name="value">DateTime value.</param>
        /// <param name="time">Time value.</param>
        /// <param name="dateFormat">Date format.</param>
        /// <param name="timeFormat">Time format.</param>
        /// <returns>HTML string.</returns>
        public static IHtmlString ToFullDateWithTimeAdded(
            this DateTime value,
            int time = 0,
            string dateFormat = "MMM dd, yyyy",
            string timeFormat = "HH:mm")
        {
            //// check format time correct HHmmss
            string strTime = time.ToString();

            if (strTime.Length < 6)
            {
                int count = 6 - strTime.Length;

                for (int i = 0; i < count; i++)
                {
                    strTime = strTime.Insert(0, "0");
                }
            }

            return new HtmlString(value.ToString(dateFormat) + " - " + value.ToString(timeFormat).Replace(':', 'h') + "'");
        }

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent string
        /// representation using the specified format.
        /// </summary>
        /// <param name="value">DateTime value.</param>
        /// <param name="dateFormat">Date format.</param>
        /// <returns>HTML string.</returns>
        public static IHtmlString ToDateString(this DateTime value, string dateFormat = "MMM dd, yyyy")
        {
            return new HtmlString(value.ToString(dateFormat));
        }

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent string
        /// representation using the specified format.
        /// </summary>
        /// <param name="value">Date value.</param>
        /// <param name="time">Time value.</param>
        /// <param name="dateFormat">Date format.</param>
        /// <returns>HTML string.</returns>
        public static IHtmlString ToFullDateString(
            this int value,
            int time,
            string dateFormat = "MMM dd, yyyy")
        {
            //// check format time correct HHmmss
            string strTime = time.ToString();
            if (strTime.Length < 6)
            {
                int count = 6 - strTime.Length;
                for (int i = 0; i < count; i++)
                {
                    strTime = strTime.Insert(0, "0");
                }
            }

            DateTime date = DateTime.ParseExact(
                value.ToString() + " " + strTime,
                "yyyyMMdd HHmmss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);

            return new HtmlString(date.ToString(dateFormat) + " - " + date.ToString("HH:mm").Replace(':', 'h') + "'");
        }

        #endregion DateTime
    }
}