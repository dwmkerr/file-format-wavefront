using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileFormatWavefront.Model;

namespace WavefrontObjectLoader.Details
{
    public static class DetailsUiBuilder
    {
        public static IDetailsUi BuildUi(object data)
        {
            var vertices = data as ReadOnlyCollection<Vertex>;
            var uvs = data as ReadOnlyCollection<UV>;
            var faces = data as ReadOnlyCollection<Face>;
            if (vertices != null)
            {
                var ui = new VerticesUi();
                ui.Create(vertices);
                return ui;
            }
            else if (uvs != null)
            {
                var ui = new UvsUi();
                ui.Create(uvs);
                return ui;
            }
            else if (faces != null)
            {
                var ui = new FacesUi();
                ui.Create(faces);
                return ui;
            }
            else
            {
                var ui = new PropertyGridUi();
                ui.Create(data);
                return ui;
            }
        }
    }
}
