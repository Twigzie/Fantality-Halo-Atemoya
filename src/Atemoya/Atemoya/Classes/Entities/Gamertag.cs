using Atemoya.Classes.Helpers;
using PropertyChanged;
using System.Runtime.Serialization;
using System.Security;

namespace Atemoya.Classes.Entities {

    [DataContract] 
    [AddINotifyPropertyChangedInterface]
    public class Gamertag {

        #region Ignored

        [IgnoreDataMember]
        public bool IsNew {
            get; set;
        }

        [IgnoreDataMember]
        public bool IsHidden {
            get; set;
        }

        [IgnoreDataMember]
        public bool IsEditing {
            get; set;
        }

        [IgnoreDataMember]
        public string NewEmail {
            get;
            set;
        }
        [IgnoreDataMember]
        public SecureString NewPassword {
            get;
            set;
        }

        #endregion Ignored

        [DataMember(Name = "name", Order = 0)]
        public string Name {
            get;
            set;
        }

        [DataMember(Name = "created", Order = 1)]
        public long Created {
            get;
            set;
        }

        [DataMember(Name = "updated", Order = 2)]
        public long Updated {
            get;
            set;
        }

        [DataMember(Name = "input", Order = 3)]
        public InputType Input {
            get;
            set;
        }

        [DataMember(Name = "email", Order = 4)]
        public string Email {
            get;
            set;
        }

        [DataMember(Name = "password", Order = 5)]
        public string Password {
            get;
            set;
        }

        public Gamertag() {
            Name = "";
            Input = InputType.Unknown;
            Email = "";
            Password = "";
            Created = 0;
            Updated = 0;
            IsNew = false;
            IsHidden = false;
            IsEditing = false;
        }

    }
}