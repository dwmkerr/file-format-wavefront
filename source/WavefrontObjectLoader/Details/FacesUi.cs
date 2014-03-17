using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;
using FileFormatWavefront.Model;

namespace WavefrontObjectLoader.Details
{
    public class FacesUi : DetailsUi<ReadOnlyCollection<Face>>
    {
        private DataGridView dataGridView;
        private ReadOnlyCollection<Face> faces;

        public override void Create(ReadOnlyCollection<Face> model)
        {
            faces = model;

            //  Create a datagrid view.
            dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;

            //  Enable virtual mode. 
            dataGridView.VirtualMode = true;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;

            //  Connect the virtual-mode events to event handlers.  
            dataGridView.CellValueNeeded += DataGridViewOnCellValueNeeded;

            //  Add the columns.
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Face Index"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Index Count"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Indices"
            });

            dataGridView.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.RowCount = model.Count;
        }

        private void DataGridViewOnCellValueNeeded(object sender, DataGridViewCellValueEventArgs dataGridViewCellValueEventArgs)
        {
            switch (dataGridViewCellValueEventArgs.ColumnIndex)
            {
                case 0:
                    dataGridViewCellValueEventArgs.Value = dataGridViewCellValueEventArgs.RowIndex;
                    break;
                case 1:
                    dataGridViewCellValueEventArgs.Value = faces[dataGridViewCellValueEventArgs.RowIndex].Indices.Count;
                    break;
                case 2:
                    {
                        var face = faces[dataGridViewCellValueEventArgs.RowIndex];
                        var builder = new StringBuilder();
                        foreach (var index in face.Indices)
                        {
                            builder.AppendFormat("{0}/{1}/{2} ", index.vertex,
                                                 index.uv.HasValue ? index.uv.ToString() : string.Empty,
                                                 index.normal.HasValue ? index.normal.ToString() : string.Empty);
                        }

                        dataGridViewCellValueEventArgs.Value = builder.ToString();
                    }
                    break;
            }
        }

        public override Control Ui
        {
            get { return dataGridView; }
        }

        public override void Destroy()
        {
            dataGridView.CellValueNeeded -= DataGridViewOnCellValueNeeded;
        }
    }
}