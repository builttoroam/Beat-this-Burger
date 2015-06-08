using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace - Needs to match server namespace for consistency
namespace Microsoft.Azure.Mobile.Server
{
    public class EntityData// : MvxNotifyPropertyChanged
    {
        //
        // Summary:
        //     Gets or sets the date and time the entity was created.
        [JsonProperty(PropertyName = "__createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }
        //
        // Summary:
        //     Gets or sets a value indicating whether the entity has been deleted.
        [JsonProperty(PropertyName = "__deleted")]
        public bool Deleted { get; set; }
        //
        // Summary:
        //     Gets or sets the unique ID for this entity.
        public string Id { get; set; }
        //
        // Summary:
        //     Gets or sets the date and time the entity was last modified.
        [JsonProperty(PropertyName = "__updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }
        //
        // Summary:
        //     Gets or sets the unique version identifier which is updated every time the entity
        //     is updated.
        [JsonProperty(PropertyName = "__version")]
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "This is part of the data model.")]
        public byte[] Version { get; set; }
    }
}
