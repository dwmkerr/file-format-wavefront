using System;
using System.Linq;
using FileFormatWavefront;

namespace ObjValidator
{
    class Program
    {
        static int Main(string[] args)
        {
            //  Get the path.
            if(args.Length < 1 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("Please provide a path to an *.obj file.");
                return 1;
            }

            //  Use the File Format object to load the test data.
            var result = FileFormatObj.Load(args[0]);


            //  If there are no messages, then the file is completely valid.
            if(!result.Messages.Any())
            {
                Console.WriteLine("The file loaded with no validation errors or warnings.");
            }

            //  Show each message.
            foreach (var message in result.Messages)
            {
                Console.WriteLine("{0}: {1}", message.MessageType, message.Details);
                Console.WriteLine("{0}: {1}", message.FileName, message.LineNumber);
            }

            Console.WriteLine("Complete. Loaded:");
            Console.WriteLine("  Vertices: {0}", result.Model.Vertices.Count);
            Console.WriteLine("  UVS: {0}", result.Model.Uvs.Count);
            Console.WriteLine("  Normals: {0}", result.Model.Normals.Count);
            Console.WriteLine("  Ungrouped Faces: {0}", result.Model.UngroupedFaces.Count);
            Console.WriteLine("  Groups: {0}", result.Model.Groups.Count);
            Console.WriteLine("  Faces in Groups: {0}", result.Model.Groups.Select(g => g.Faces.Count).Sum());
            Console.WriteLine("  Materials: {0}", result.Model.Materials.Count);
            Console.WriteLine("with:");
            Console.WriteLine("  Warnings: {0}", result.Messages.Count(m => m.MessageType == MessageType.Warning));
            Console.WriteLine("  Errors: {0}", result.Messages.Count(m => m.MessageType == MessageType.Error));

            Console.WriteLine("Any key to close.");
            Console.ReadKey();

            return 0;
        }
    }
}
