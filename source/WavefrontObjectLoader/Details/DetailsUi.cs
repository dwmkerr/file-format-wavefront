using System.Windows.Forms;

namespace WavefrontObjectLoader.Details
{
    public abstract class DetailsUi<TModel> : IDetailsUi
    {
        public abstract void Create(TModel model);

        public abstract Control Ui { get; }
        public abstract void Destroy();
    }
}