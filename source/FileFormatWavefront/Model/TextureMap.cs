using System.Drawing;

namespace FileFormatWavefront.Model
{
    /// <summary>
    /// Represents a texture map.
    /// </summary>
    public class TextureMap
    {
        /// <summary>
        /// Gets the path to the texture file.
        /// </summary>
        public string Path { get; internal set; }

        /// <summary>
        /// Gets the texture image.
        /// Note that this is only set if the file is loaded with the 'loadTextureImages' option set to <c>true</c>.
        /// </summary>
        public Image Image { get; internal set; } 
    }
}