using Atemoya.Classes.Entities;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using static Atemoya.Classes.Helpers.Paths;

namespace Atemoya.Classes.Config {

    [DataContract]
    public class AppSettings {

        #region ISingleton

        static AppSettings() {
            Instance = new AppSettings();
        }
        public static AppSettings Instance {
            get;
            set;
        }

        #endregion

        [DataMember(Name = "version", Order = 0)]
        public int Version {
            get; set;
        }

        [DataMember(Name = "created", Order = 1)]
        public long Created {
            get; set;
        }

        [DataMember(Name = "updated", Order = 2)]
        public long LastUpdated {
            get; set;
        }

        [DataMember(Name = "exported", Order = 3)]
        public long LastExported {
            get; set;
        }

        [DataMember(Name = "gamertags", Order = 4)]
        public ObservableCollection<Gamertag> Gamertags {
            get; set;
        }

        public bool Load() {
            try {

                if (File.Exists(AppConfig)) {

                    var serializer = new DataContractJsonSerializer(typeof(AppSettings));
                    using (var reader = new FileStream(AppConfig, FileMode.Open, FileAccess.Read)) {
                        if (!(serializer.ReadObject(reader) is AppSettings temp))
                            return false;
                        else {
                            Version = temp.Version;
                            Created = temp.Created;
                            LastUpdated = temp.LastUpdated;
                            LastExported = temp.LastExported;
                            Gamertags = temp.Gamertags;
                        }
                    }

                }
                else {

                    Version = 1;
                    Created = 1;
                    LastUpdated = 0;
                    LastExported = 0;
                    Gamertags = new ObservableCollection<Gamertag>();

                    return Save();

                }

                return true;

            } catch {
                return false;
            }
        }
        public bool Save() {
            try {
                var serializer = new DataContractJsonSerializer(typeof(AppSettings));
                using (var stream = new FileStream(AppConfig, FileMode.Create, FileAccess.Write))
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, false, true))
                    serializer.WriteObject(writer, this);

                return true;

            } catch {
                return false;
            }
        }

    }

}
