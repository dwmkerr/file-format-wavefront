file-format-wavefront
=====================

A simple .NET library to load data from Wavefront *.obj and *.mlb files.

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
var result = FileFormatObj.Load("MyFile.obj");
````

The object that is returned is a ``FileLoadResult`` object. The object contains a property ``Model`` which is the Scene of data loaded. The object also contains a collection of ``Message`` objects describing any warnings or errors encountered loading the file:

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

Notes on Textures
-----------------

Currently textures are not loaded - only the path to the texture file is stored in the ``TextureMap`` object. This is to avoid dependencies to any image loading code - it is a trivial matter to load textures from files in .NET, but as many users will transform this data into their own domain, I leave this mapping step to the consumer of the library.
