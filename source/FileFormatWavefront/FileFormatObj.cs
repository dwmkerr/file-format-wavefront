using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FileFormatWavefront.Extensions;
using FileFormatWavefront.Internals;
using FileFormatWavefront.Model;

namespace FileFormatWavefront
{
    public static class FileFormatObj
    {

        /// <summary>
        /// Loads a scene into a FileLoadResult object from a Wavefromt *.obj file.
        /// All warning and error messages are in the returned object's 'Messages' collection.
        /// The actual scene data is in the returned object's 'Model' data.
        /// </summary>
        /// <param name="path">The path to the *.obj file.</param>
        /// <returns>A <see cref="FileLoadResult{TModel}"/> containing the file load result.</returns>
        public static FileLoadResult<Scene> Load(string path)
        {
            //  Create a stream reader.
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    //  Read the scene.
                    return ReadScene(reader, path);
                }
            }
        }

        /// <summary>
        /// Internally used to reads the scene.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="path">The path.</param>
        /// <returns>The file load result.</returns>
        private static FileLoadResult<Scene> ReadScene(StreamReader reader, string path)
        {
            //  Keep track of messages and the raw data we will use to build a scene.
            var messages = new List<Message>();
            var uvs = new List<UV>();
            var normals = new List<Vertex>();
            var vertices = new List<Vertex>();
            var interimFaces = new List<InterimFace>();
            var materials = new List<Material>();
            var groups = new List<Group>();

            //  State changing data is loaded as we go through the file - once loaded, state changing
            //  data applies to all subsequent elements until it is explicitly changed by introducing
            //  new state changing data.
            Group currentGroup = null;
            string currentMaterialName = null;

            //  Read line by line.
            string line = null;
            int lineNumberCounter = 0;
            while ((line = reader.ReadLine()) != null)
            {
                ++lineNumberCounter;

                //  Strip any comments from the line and skip empty lines.
                line = LineData.StripComments(line);
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                //  Try and read the line type and data.
                string lineType, lineData;
                if (LineData.TryReadLineType(line, out lineType, out lineData) == false)
                    continue;

                //  Read texture coordinates.
                if (lineType.IsLineType(LineTypeTextureCoordinate))
                {
                    try
                    {
                        //  Split the line data into texture coordinates.
                        var dataStrings = lineData.Split(dataSeparators, StringSplitOptions.RemoveEmptyEntries);

                        //  Add the UV.
                        uvs.Add(new UV
                                {
                                    u = float.Parse(dataStrings[0]),
                                    v = float.Parse(dataStrings[1])
                                });
                    }
                    catch (Exception exception)
                    {
                        messages.Add(new Message(MessageType.Error, path, lineNumberCounter,
                            "There was an error reading the texture coordinate data.", exception));
                    }
                }
                else if (lineType.IsLineType(LineTypeNormalCoordinate))
                {
                    try
                    {
                        //  Split the line data into normal coordinates.
                        var dataStrings = lineData.Split(dataSeparators, StringSplitOptions.RemoveEmptyEntries);
                        normals.Add(new Vertex
                                {
                                    x = float.Parse(dataStrings[0]),
                                    y = float.Parse(dataStrings[1]),
                                    z = float.Parse(dataStrings[2])
                                });
                    }
                    catch (Exception exception)
                    {
                        messages.Add(new Message(MessageType.Error, path, lineNumberCounter,
                            "There was an error reading the normal data.", exception));
                    }
                }
                else if (lineType.IsLineType(LineTypeVertex))
                {
                    try
                    {
                        //  Split the line data into vertex coordinates.
                        var dataStrings = lineData.Split(dataSeparators, StringSplitOptions.RemoveEmptyEntries);
                        vertices.Add(new Vertex
                                {
                                    x = float.Parse(dataStrings[0]),
                                    y = float.Parse(dataStrings[1]),
                                    z = float.Parse(dataStrings[2])
                                });
                    }
                    catch (Exception exception)
                    {
                        messages.Add(new Message(MessageType.Error, path, lineNumberCounter,
                            "There was an error reading the vertex data.", exception));
                    }
                }
                else if (lineType.IsLineType(LineTypeFace))
                {
                    try
                    {
                        var indices = new List<Index>();

                        //  Split the line data into index strings.
                        var indexStrings = lineData.Split(dataSeparators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var indexString in indexStrings)
                        {
                            //  Split the parts.
                            var parts = indexString.Split(new[] { '/' }, StringSplitOptions.None);
                            indices.Add(new Index
                                        {
                                            vertex = int.Parse(parts[0]) - 1,
                                            uv = (parts.Length > 1 && parts[1].Length > 0) ? (int?)int.Parse(parts[1]) - 1 : null,
                                            normal = (parts.Length > 2 && parts[2].Length > 0) ? (int?)int.Parse(parts[1]) - 1 : null
                                        });
                        }
                        interimFaces.Add(new InterimFace
                                         {
                                             materialName = currentMaterialName,
                                             indices = indices,
                                             group = currentGroup
                                         });
                    }
                    catch (Exception exception)
                    {
                        messages.Add(new Message(MessageType.Error, path, lineNumberCounter,
                            "There was an error reading the index data.", exception));
                    }
                }
                else if (lineType.IsLineType(LineTypeMaterialLibrary))
                {
                    //  The material file path is the line data.
                    var materialPath = lineData;

                    //  If the path is relative, make it absolute based on the current directory (if we've been passed a path).
                    if (Path.IsPathRooted(lineData) == false && path != null)
                        materialPath = Path.Combine(Path.GetDirectoryName(path), materialPath);

                    //  Read the material file.
                    try
                    {
                        var fileLoadResult = FileFormatMtl.Load(materialPath);
                        materials.AddRange(fileLoadResult.Model);
                        messages.AddRange(fileLoadResult.Messages);
                    }
                    catch (Exception exception)
                    {
                        messages.Add(new Message(MessageType.Error, path, lineNumberCounter,
                            string.Format("Failed to load material file '{0}'.", materialPath), exception));
                    }
                }
                else if (lineType.IsLineType(LineTypeUseMaterial))
                {
                    //  The material name is simply the line data.
                    currentMaterialName = lineData;
                }
                else if(lineType.IsLineType(LineTypeGroup))
                {
                    //  Create a new group.
                    var groupNames = lineData.Split(dataSeparators, StringSplitOptions.RemoveEmptyEntries);
                    currentGroup = new Group(groupNames);
                    groups.Add(currentGroup);
                }
                else if(lineType.IsLineType(LineTypeSmoothingGroup))
                {
                    //  If we have no current group, we cannot set a smoothing group.
                    if(currentGroup == null)
                    {
                        messages.Add(new Message(MessageType.Warning, path, lineNumberCounter,
                            string.Format("Cannot set smoothing group '{0}' as the current context has no group.", lineData)));
                    }
                    else
                    {
                        //  The smoothing group is an int, if we can get it.
                        int smoothingGroup;
                        if(int.TryParse(lineData, out smoothingGroup))
                            currentGroup.SetSmoothingGroup(smoothingGroup);
                        currentGroup.SetSmoothingGroup(null);
                    }
                }
                else
                {
                    messages.Add(new Message(MessageType.Warning, path, lineNumberCounter,
                            string.Format("Skipped unknown line type '{0}'.", lineType)));
                }
            }

            //  Currently we don't have faces, just indexes and material names. But now that we've loaded
            //  the entire file, we can map the material names to the actual materials.
            var ungroupedFaces = new List<Face>();
            foreach (var interimFace in interimFaces)
            {
                //  If we have a material named but not in the set of materials, warn.
                var material = materials.FirstOrDefault(m => m.Name == interimFace.materialName);
                if (material == null)
                    messages.Add(new Message(MessageType.Warning, path, lineNumberCounter,
                        string.Format("Material '{0}' is referenced for a face, but not included in any material files.", interimFace.materialName)));

                //  If the face is grouped, add it to the group. Otherwise add it to the ungrouped faces.
                var face = new Face(material, interimFace.indices);
                if (interimFace.group != null)
                    interimFace.group.AddFace(face);
                else
                    ungroupedFaces.Add(face);
            }

            return new FileLoadResult<Scene>(new Scene(vertices, uvs, normals, ungroupedFaces, groups, materials), messages);
        }

        private const string LineTypeTextureCoordinate = "vt";
        private const string LineTypeNormalCoordinate = "vn";
        private const string LineTypeVertex = "v";
        private const string LineTypeFace = "f";
        private const string LineTypeMaterialLibrary = "mtllib";
        private const string LineTypeUseMaterial = "usemtl";
        private const string LineTypeGroup = "g";
        private const string LineTypeSmoothingGroup = "s";

        /// <summary>
        /// The data separators, any valid value that can separate data in a line.
        /// </summary>
        private static readonly char[] dataSeparators = new[] { ' ' };

        internal class InterimFace
        {
            public Group group;
            public string materialName;
            public List<Index> indices;
            public bool smoothShading;
        }
    }
}