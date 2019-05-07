using System.Runtime.Serialization;

namespace Microsoft.Scripting {
    [DataContract]
    internal class LanguagePackItem {
        [DataMember(IsRequired = true)]
        public string Key { get; set; }
        [DataMember(IsRequired = true)]
        public string Value { get; set; }
        [DataMember()]
        public string Solution { get; set; }
    }
}
