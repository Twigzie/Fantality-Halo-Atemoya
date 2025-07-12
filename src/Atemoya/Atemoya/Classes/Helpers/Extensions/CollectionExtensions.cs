using System.Collections.ObjectModel;
using System.Linq;
using Atemoya.Classes.Models.Application;

namespace Atemoya.Classes.Helpers.Extensions {

    public static class CollectionExtensions {

        public static bool ContainsTab(this ObservableCollection<TabModel> source, string tab) {
            return source?.FirstOrDefault(t => t.ID == tab) != null;
        }
        public static bool ContainsTab(this ObservableCollection<TabModel> source, TabType type) {
            return source?.FirstOrDefault(t => t.Type == type) != null;
        }

    }

}