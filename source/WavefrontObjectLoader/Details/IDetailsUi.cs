using System.Windows.Forms;

namespace WavefrontObjectLoader.Details
{
    public interface IDetailsUi
    {
        Control Ui { get;  }
        void Destroy();
    }
}