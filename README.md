file-format-wavefront
=====================

A simple .NET library to load data from Wavefront *.obj and *.mlb files.

❗ **Deprecated** - this library bas been deprecated. I recommened using the [`SharpGL.Serialization`](https://github.com/dwmkerr/sharpgl/tree/master/source/SharpGL/Core/SharpGL.Serialization) package which supports Wavefront, Discreet, and more.

Installation
------------

You can use Nuget to install the library.

````
PM> Install-Package FileFormatWavefront
````

Usage
-----

To load data from a Wavefront Object File, use the ``FileFormatObj`` class:

````csharp
//  Use the File Format object to load the test data.
bool loadTextureImages = true;
var result = FileFormatObj.Load("MyFile.obj", loadTextureImages);
````

The object that is returned is a ``FileLoadResult`` object. The object contains a property ``Model`` which is the Scene of data loaded. 

Texture map images can optionally be loaded, but you can skip this if you are not going to need the image or will load it yourself (whether or not the image is loaded, a ``TextureMap`` object is still created with the image path as part of it).

The object also contains a collection of ``Message`` objects describing any warnings or errors encountered loading the file. An example of iterating through the vertices and messages would look like this:

````csharp

//  Show each vertex.
foreach(Vertex v in result.Model.Vertices)
{
    Console.Write("{0} {1} {2}", v.x, v.y, v.z);
}

//  Show each message.
foreach (var message in result.Messages)
{
    Console.WriteLine("{0}: {1}", message.MessageType, message.Details);
    Console.WriteLine("{0}: {1}", message.FileName, message.LineNumber);
}

````

The ``Model`` property is of type ``Scene`` and contains the following members:

 * ``Vertices`` all vertex data.
 * ``Uvs`` all material coordinate data.
 * ``Normals`` all normal data.
 * ``Materials`` all material data.
 * ``Groups`` all objects (groups of faces).
 * ``UngroupedFaces`` all faces not grouped into objects.

Associated *.mlb (material library) files are loaded automatically. If you want to load a material library only, you can use the ``FileFormatMlb`` class in the same way as the ``FileFormatObj`` class.
