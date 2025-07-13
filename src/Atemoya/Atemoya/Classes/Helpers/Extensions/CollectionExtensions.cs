using Atemoya.Classes.Models.Application;
using System.Collections.ObjectModel;
using System.Linq;

namespace Atemoya.Classes.Helpers.Extensions {

    public static class CollectionExtensions {

        public static bool ContainsTab(this ObservableCollection<TabModel> source, string tab) {
            return source?.FirstOrDefault(t => t.ID == tab) != null;
        }
        public static bool ContainsTab(this ObservableCollection<TabModel> source, TabType type) {
            return source?.FirstOrDefault(t => t.Type == type) != null;
        }

        public static void SortByIsNewFirst<T>(this ObservableCollection<T> collection) where T : class {
            if (collection == null)
                return;

            int count = collection.Count;
            for (int i = 0; i < count - 1; i++) {
                for (int j = 0; j < count - i - 1; j++) {
                    var itemA = collection[j] as dynamic;
                    var itemB = collection[j + 1] as dynamic;

                    // Swap if itemA is not new and itemB is new
                    if (itemA != null && itemB != null && itemA.IsNew == false && itemB.IsNew == true) {
                        var temp = collection[j];
                        collection[j] = collection[j + 1];
                        collection[j + 1] = temp;
                    }
                }
            }
        }

    }

}