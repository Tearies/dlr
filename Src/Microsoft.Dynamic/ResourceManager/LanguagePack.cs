using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Scripting {
    [DataContract]
    internal class LanguagePack : ICloneable {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public LanguagePack() {
            Items = new List<LanguagePackItem>();
        }

        [DataMember(IsRequired = true, EmitDefaultValue = false, Name = "Culture", Order = 0)]
        public string CultureInfo { get; set; }

        [DataMember(IsRequired = true, EmitDefaultValue = false, Name = "Items", Order = 1)]
        public List<LanguagePackItem> Items { get; set; }

        #region Implementation of ICloneable

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone() {
            var clone = new LanguagePack();
            clone.CultureInfo = this.CultureInfo;
            this.Items.ForEach(o => {
                clone.Items.Add(new LanguagePackItem() {
                    Key = o.Key,
                    Value = o.Value
                });
            });
            return clone;
        }


    #endregion
}
}
