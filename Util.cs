﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace Ultimate_Steam_Acount_Manager
{
    class Util
    {

        private static string CachedDataFolder = null;
        public static string GetDataFolder()
        {
            if(string.IsNullOrEmpty(CachedDataFolder))
                CachedDataFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
                + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar;
            return CachedDataFolder;
        }

        public static SteamAccount GetAccountByName(List<SteamAccount> accounts, string name)
        {
            if (accounts == null) return null;
            foreach (SteamAccount account in accounts)
            {
                if (account.Login == name) return account;
            }
            return null;
        }

        public static string GetRelativeTimeString(long time)
        {
            if (time == 0) return "never";
            var ts = new TimeSpan((SteamAuth.Util.GetSystemUnixTime() - time) * 10000000);
            double delta = Math.Abs(ts.TotalSeconds);
            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 120)
            {
                return "a minute ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days ago";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }

        public static TDelegate GetVirtualFunction<TDelegate>(IntPtr obj, int index)
        {
            IntPtr vTable = Marshal.ReadIntPtr(obj, 0);
            IntPtr function = Marshal.ReadIntPtr(vTable, index * Marshal.SizeOf<IntPtr>());
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(function);
        }

    }
}
