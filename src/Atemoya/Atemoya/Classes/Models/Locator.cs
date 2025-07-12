using Atemoya.Classes.Models.Application;

namespace Atemoya.Classes.Models {

    public sealed class Locator {

        #region ISingleton

        static Locator() {
            Instance = new Locator();
        }
        public static Locator Instance {
            get;
        }

        #endregion

        public AppModel Application => AppModel.Instance;
        public GamertagModel Gamertags => GamertagModel.Instance;

    }

}