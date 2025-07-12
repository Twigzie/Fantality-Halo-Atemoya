using System;
using Atemoya.Classes.Helpers;
using Atemoya.Classes.Helpers.Extensions;

namespace Atemoya.Classes.Models.Application {

    public class TabModel {

        public TabModel() {
            ID = GenerateID();
        }
        public TabModel(TabType type) {
            ID = GenerateID();
            Type = type;
        }

        public string ID {
            get;
        }
        public string Name {
            get;
            set;
        }
        public object Content {
            get;
            set;
        }
        public TabType Type {
            get;
            set;
        }

        private string GenerateID() {
            try {
                var id = Guid.NewGuid().ToString();
                if (Locator.Instance.Application.Tabs?.ContainsTab(id) == true)
                    return GenerateID();
                return id;
            }
            catch {
                throw new Exception("Error generating 'TabID'");
            }
        }

    }

}