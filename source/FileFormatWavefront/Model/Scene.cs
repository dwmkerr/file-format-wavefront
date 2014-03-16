using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FileFormatWavefront.Model
{
    /// <summary>
    /// Represent a scene of data loaded from an *.obj file.
    /// </summary>
    public class Scene
    {
        private readonly List<Vertex> vertices;
        private readonly List<UV> uvs;
        private readonly List<Vertex> normals;
        private readonly List<Face> ungroupedFaces;
        private readonly List<Group> groups; 
        private readonly List<Material> materials;

        internal Scene(List<Vertex> vertices, List<UV> uvs, List<Vertex> normals, List<Face> ungroupedFaces, List<Group> groups, List<Material> materials)
        {
            this.vertices = vertices;
            this.uvs = uvs;
            this.normals = normals;
            this.ungroupedFaces = ungroupedFaces;
            this.groups = groups;
            this.materials = materials;
        }

        /// <summary>
        /// Gets the vertices.
        /// </summary>
        public ReadOnlyCollection<Vertex> Vertices
        {
            get { return vertices.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the uvs.
        /// </summary>
        public ReadOnlyCollection<UV> Uvs
        {
            get { return uvs.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the normals.
        /// </summary>
        public ReadOnlyCollection<Vertex> Normals
        {
            get { return normals.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the faces which don't belong to any groups.
        /// </summary>
        public ReadOnlyCollection<Face> UngroupedFaces
        {
            get { return ungroupedFaces.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        public ReadOnlyCollection<Group> Groups
        {
            get { return groups.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the materials.
        /// </summary>
        public ReadOnlyCollection<Material> Materials
        {
            get { return materials.AsReadOnly(); }
        }
    }
}
