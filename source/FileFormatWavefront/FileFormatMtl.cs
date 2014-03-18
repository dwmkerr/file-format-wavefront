using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FileFormatWavefront.Extensions;
using FileFormatWavefront.Internals;
using FileFormatWavefront.Model;

namespace FileFormatWavefront
{
    /// <summary>
    /// A file loader for the Wavefront *.mtl file format.
    /// </summary>
    public static class FileFormatMtl
    {
        /// <summary>
        /// Loads materials from the specified stream.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="loadTextureImages">if set to <c>true</c> texture images
        /// will be loaded and set in the <see cref="TextureMap.Image"/> property.</param>
        /// <returns>The results of the file load.</returns>
        public static FileLoadResult<List<Material>> Load(string path, bool loadTextureImages)
        {
            //  Create a streamreader and read the data.
            using(var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var streamReader = new StreamReader(stream))
                return Read(streamReader, path, loadTextureImages);
        }

        /// <summary>
        /// Read the material data from the specified stream.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="path">The path. This can be null - it is only used for recording diagnostic information.</param>
        /// <param name="loadTextureImages">if set to <c>true</c> [load texture images].</param>
        /// <returns>
        /// The results of the file load.
        /// </returns>
        private static FileLoadResult<List<Material>> Read(TextReader reader, string path, bool loadTextureImages)
        {
            //  The model we are loading is a list of materials. During loading, we'll keep
            //  track of messages that may be useful to consumers.
            var materials = new List<Material>();
            var messages = new List<Message>();

            //  As we load, we're enriching the data of a Material object.
            Material currentMaterial = null;

            //  Go through each line, keeping track of the line number.
            var lineNumberCounter = 0;
            string line;
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

                if (lineType.IsLineType(LineTypeNewMaterial))
                {
                    //  Add a new material to the list, store it as the current one and set the name.
                    currentMaterial = new Material { Name = lineData };
                    materials.Add(currentMaterial);
                }
                else if (currentMaterial != null)
                {
                    if (lineType.IsLineType(LineTypeMaterialAmbient))
                    {
                        currentMaterial.Ambient = ReadColour(lineData);
                    }
                    else if (lineType.IsLineType(LineTypeMaterialDiffuse))
                    {
                        currentMaterial.Diffuse = ReadColour(lineData);
                    }
                    else if (lineType.IsLineType(LineTypeMaterialSpecular))
                    {
                        currentMaterial.Specular = ReadColour(lineData);
                    }
                    else if (lineType.IsLineType(LineTypeMaterialShininess))
                    {
                        currentMaterial.Shininess = float.Parse(lineData);
                    }
                    else if (lineType.IsLineType(LineTypeOpticalDensity))
                    {
                        currentMaterial.OpticalDensity = float.Parse(lineData);
                    }
                    else if (lineType.IsLineType(LineTypeBumpStrength))
                    {
                        currentMaterial.BumpStrength = float.Parse(lineData);
                    }
                    else if (lineType.IsLineType(LineTypeTextureMapAmbient))
                    {
                        currentMaterial.TextureMapAmbient = ReadTextureMap(path, lineNumberCounter, messages, lineData, loadTextureImages);
                    }
                    else if (lineType.IsLineType(LineTypeTextureMapDiffuse))
                    {
                        currentMaterial.TextureMapDiffuse = ReadTextureMap(path, lineNumberCounter, messages, lineData, loadTextureImages);
                    }
                    else if (lineType.IsLineType(LineTypeTextureMapSpecular))
                    {
                        currentMaterial.TextureMapSpecular = ReadTextureMap(path, lineNumberCounter, messages, lineData, loadTextureImages);
                    }
                    else if (lineType.IsLineType(LineTypeTextureMapSpecularHighlight))
                    {
                        currentMaterial.TextureMapSpecularHighlight = ReadTextureMap(path, lineNumberCounter, messages, lineData, loadTextureImages);
                    }
                    else if (lineType.IsLineType(LineTypeTextureMapAlpha))
                    {
                        currentMaterial.TextureMapAlpha = ReadTextureMap(path, lineNumberCounter, messages, lineData, loadTextureImages);
                    }
                    else if (lineType.IsLineType(LineTypeTextureMapBump))
                    {
                        currentMaterial.TextureMapBump = ReadTextureMap(path, lineNumberCounter, messages, lineData, loadTextureImages);
                    }
                    else if (lineType.IsLineType(LineTypeDissolve) || lineType.IsLineType(LineTypeTransparent))
                    {
                        //  Read the transparency.
                        currentMaterial.Transparency = float.Parse(lineData);
                    }
                    else if(lineType.IsLineType(LineTypeIlluminationModel))
                    {
                        currentMaterial.IlluminationModel = int.Parse(lineData);
                    }
                    else
                    {
                        //  Anything we encounter here we don't understand.
                        messages.Add(new Message(MessageType.Warning, path, lineNumberCounter,
                                                         string.Format("Skipped unknown line type '{0}'.", lineType)));
                    }
                }
                else
                {
                    //  Anything we encounter here we don't understand.
                    messages.Add(new Message(MessageType.Warning, path, lineNumberCounter,
                                                     string.Format("Skipped unknown or out of context line type '{0}'.", lineType)));
                }

            }

            //  Return the model and messages as a file load result.
            return new FileLoadResult<List<Material>>(materials, messages);
        }

        private static TextureMap ReadTextureMap(string fileName, int lineNumber, List<Message> messages, string lineData, bool loadTextureBitmaps)
        {
            //  TODO: Support texture map options. http://paulbourke.net/dataformats/mtl/
            var textureMap = new TextureMap { Path = lineData };
            if(loadTextureBitmaps == false)
                return textureMap;

            try
            {
                var path = Path.Combine(Path.GetDirectoryName(fileName), lineData);
                textureMap.Image = Image.FromFile(path);
            }
            catch (Exception exception)
            {
                messages.Add(new Message(MessageType.Error, fileName, lineNumber,
                    string.Format("Failed to load the texture map image file '{0}'", lineData), exception));
            }
            return textureMap;
        }

        private static Colour ReadColour(string lineData)
        {

            //  Get the colour parts.
            var colourParts = lineData.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Colour
            {
                r = float.Parse(colourParts[0]),
                g = float.Parse(colourParts[1]),
                b = float.Parse(colourParts[2]),
                a = colourParts.Length == 4 ? float.Parse(colourParts[3]) : 1.0f,
            };
        }

        private const string LineTypeNewMaterial = "newmtl";
        private const string LineTypeMaterialAmbient = "Ka";
        private const string LineTypeMaterialDiffuse = "Kd";
        private const string LineTypeMaterialSpecular = "Ks";
        private const string LineTypeMaterialShininess = "Ns";
        private const string LineTypeOpticalDensity = "Ni";
        private const string LineTypeBumpStrength = "Km";
        private const string LineTypeDissolve = "d";
        private const string LineTypeTransparent = "Tr";
        private const string LineTypeTextureMapAmbient = "map_Ka";
        private const string LineTypeTextureMapDiffuse = "map_Kd";
        private const string LineTypeTextureMapSpecular = "map_Ks";
        private const string LineTypeTextureMapSpecularHighlight = "map_Ns";
        private const string LineTypeTextureMapAlpha = "map_d";
        private const string LineTypeTextureMapBump = "map_bump";
        private const string LineTypeIlluminationModel = "illum";

    }
}