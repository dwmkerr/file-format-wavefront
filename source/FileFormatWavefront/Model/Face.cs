using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileFormatWavefront.Model
{
    /// <summary>
    /// Represents a face.
    /// </summary>
    public class Face
    {
        private readonly List<Index> indices;
        private readonly Material material;

        internal Face(Material material, List<Index> indices)
        {
            this.material = material;
            this.indices = indices;
        }

        /// <summary>
        /// Gets the material.
        /// </summary>
        public Material Material
        {
            get { return material; }
        }


        /// <summary>
        /// Gets the indices.
        /// </summary>
        public ReadOnlyCollection<Index> Indices
        {
            get { return indices.AsReadOnly(); }
        }
    }
}