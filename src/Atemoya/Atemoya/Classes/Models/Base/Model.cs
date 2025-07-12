using Atemoya.Classes.Helpers;
using PropertyChanged;

namespace Atemoya.Classes.Models.Base {

    [AddINotifyPropertyChangedInterface]
    public class Model {

        public TabType Type {
            get;
            set;
        }

    }

}