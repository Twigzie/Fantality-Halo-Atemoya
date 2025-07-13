using System;
using System.Diagnostics;
using System.Reflection;

namespace Atemoya.Classes.Helpers.Extensions {

    public static class BaseExtensions {

        public static string GetFileVersion(Assembly assembly) {
            return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }
        public static long GetEpochTime() {
            return new DateTimeOffset(TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local)).ToUnixTimeSeconds();
        }
    }

}