using System;
using System.Runtime.Serialization;

namespace Atemoya.Classes.Helpers {

    public enum TabType {
        Tags = 0,
        Export = 1
    }

    [DataContract]
    public enum InputType {
        [EnumMember(Value = "mouse")]
        Mouse = 0,
        [EnumMember(Value = "controller")]
        Controller = 1,
        [EnumMember(Value = "unknown")]
        Unknown = 2
    }

}