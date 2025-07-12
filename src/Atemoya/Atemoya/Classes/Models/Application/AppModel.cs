using Atemoya.Classes.Commands;
using Atemoya.Classes.Helpers;
using Atemoya.Classes.Helpers.Extensions;
using Atemoya.Classes.Models.Base;
using Atemoya.Controls.Tabs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using static Atemoya.Classes.Helpers.Extensions.BaseExtensions;

namespace Atemoya.Classes.Models.Application {

    public class AppModel : Model {

        #region ISingleton

        static AppModel() {
            Instance = new AppModel() {
                Caption = $"Atemoya (MCC Gamertag Manager)",
                Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion,
                Status = "Ready"
            };
            Instance?.Initialize();
        }

        public static AppModel Instance {
            get;
            set;
        }

        #endregion ISingleton

        public string Status {
            get;
            set;
        }
        public string Caption {
            get;
            set;
        }
        public string Version {
            get;
            set;
        }
        public ObservableCollection<TabModel> Tabs {
            get;
            private set;
        }

        #region Tabs

        public ICommand OpenTabCommad => new RelayParamCommand((arg) => {
            try {
                var tab = new TabModel((TabType)arg);
                if (Tabs.ContainsTab(tab.Type))
                    return;

                switch (tab.Type) {
                    case TabType.Tags:
                        tab.Name = "Gamertags";
                        tab.Content = new GamertagContainer();
                        Tabs.Add(tab);
                        break;

                    default:
                        return;
                }
            } catch (Exception ex) {
                throw ex;
            }
        });

        public ICommand OnCloseTabCommand => new RelayParamCommand((arg) => {
            try {
                var source = (TabModel)arg;

                if (null == source || null == Tabs || Tabs.ContainsTab(source.ID) == false) {
                    return;
                }

                var index = Tabs.IndexOf(Tabs.FirstOrDefault(t => t.Name == source.Name && t.ID == source.ID));
                if (index >= 0) {
                    Tabs.RemoveAt(index);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
                throw ex;
            }
        });

        #endregion Tabs

        private void Initialize() {
            try {
                Status = "Initializing";
                Version = GetFileVersion(Assembly.GetExecutingAssembly());

                Tabs = new ObservableCollection<TabModel>();
            } catch (Exception ex) {
                Console.WriteLine(ex);
                throw ex;
            } finally {
                Status = "Ready";
            }
        }

    }

}