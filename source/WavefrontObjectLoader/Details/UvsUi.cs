using System.Collections.ObjectModel;
using System.Windows.Forms;
using FileFormatWavefront.Model;

namespace WavefrontObjectLoader.Details
{
    public class UvsUi : DetailsUi<ReadOnlyCollection<UV>>
    {
        private DataGridView dataGridView;
        private ReadOnlyCollection<UV> uvs;

        public override void Create(ReadOnlyCollection<UV> model)
        {
            uvs = model;

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
                HeaderText = "U"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "V"
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
                    dataGridViewCellValueEventArgs.Value = uvs[dataGridViewCellValueEventArgs.RowIndex].u;
                    break;
                case 2:
                    dataGridViewCellValueEventArgs.Value = uvs[dataGridViewCellValueEventArgs.RowIndex].v;
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