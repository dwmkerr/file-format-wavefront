using System.Collections.ObjectModel;
using System.Windows.Forms;
using FileFormatWavefront.Model;

namespace WavefrontObjectLoader.Details
{
    public class VerticesUi : DetailsUi<ReadOnlyCollection<Vertex>>
    {
        private DataGridView dataGridView;
        private ReadOnlyCollection<Vertex> vertices; 

        public override void Create(ReadOnlyCollection<Vertex> model)
        {
            vertices = model;

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
                HeaderText = "Index"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "X"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Y"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Z"
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
                    dataGridViewCellValueEventArgs.Value = vertices[dataGridViewCellValueEventArgs.RowIndex].x;
                    break;
                case 2:
                    dataGridViewCellValueEventArgs.Value = vertices[dataGridViewCellValueEventArgs.RowIndex].y;
                    break;
                case 3:
                    dataGridViewCellValueEventArgs.Value = vertices[dataGridViewCellValueEventArgs.RowIndex].z;
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