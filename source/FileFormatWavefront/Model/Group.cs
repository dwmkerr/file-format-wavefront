using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FileFormatWavefront.Model
{
    /// <summary>
    /// Represents a group of faces.
    /// </summary>
    public class Group
    {
        private readonly List<string> names = new List<string>();
        private readonly List<Face> faces = new List<Face>(); 
        private int? smoothingGroup;

        internal Group(IEnumerable<string> names)
        {
            this.names.AddRange(names);
        }

        internal void SetSmoothingGroup(int? smoothingGroup)
        {
            this.smoothingGroup = smoothingGroup;
        }
        internal void AddFace(Face face)
        {
            faces.Add(face);
        }

        /// <summary>
        /// Gets the group names. A group doesn't necessarily have to have any names.
        /// Groups can also share names (i.e one group can be part of another).
        /// </summary>
        public List<string> Names
        {
            get { return names; }
        }

        /// <summary>
        /// Gets the smoothing group.
        /// </summary>
        public int? SmoothingGroup
        {
            get { return smoothingGroup; }
        }

        /// <summary>
        /// Gets the faces.
        /// </summary>
        public ReadOnlyCollection<Face> Faces
        {
            get { return faces.AsReadOnly(); }
        }
    }
}
