using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int EId { get; set; }
    }
}
