using System.Windows.Forms;

namespace WavefrontObjectLoader.Details
{
    public class PropertyGridUi : DetailsUi<object>
    {
        private object model;
        private PropertyGrid propertyGrid;
        public override void Create(object model)
        {
            this.model = model;
            propertyGrid = new PropertyGrid {SelectedObject = model};
        }

        public override Control Ui
        {
            get {  return propertyGrid; }
        }

        public override void Destroy()
        {
        }
    }
}