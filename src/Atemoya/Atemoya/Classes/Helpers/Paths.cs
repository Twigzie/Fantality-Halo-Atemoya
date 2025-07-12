using System.IO;
using System.Reflection;

namespace Atemoya.Classes.Helpers {

    public static class Paths {

        private static string AppPath {
            get {
                var temp = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (string.IsNullOrEmpty(temp))
                    return "";
                return temp;
            }
        }
        public static string AppConfig {
            get => Path.Combine(AppPath, "settings.json");
        }

    }
}
