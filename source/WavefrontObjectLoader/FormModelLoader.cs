using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileFormatWavefront;
using FileFormatWavefront.Model;
using WavefrontObjectLoader.Details;
using WavefrontObjectLoader.Extensions;

namespace WavefrontObjectLoader
{
    public partial class FormModelLoader : Form
    {
        public FormModelLoader()
        {
            InitializeComponent();

            AddOutput("Choose File > Load to load a Wavefront *.obj file");
        }

        private async void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            //  Create a file open dialog to load the file.
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Wavefront Object Files (*.obj)|*.obj|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                DestroyDetailsUi();
                treeViewModel.Nodes.Clear();


                var loadingMessage = string.Format("Loading {0}", Path.GetFileName(openFileDialog.FileName));

                AddOutput(loadingMessage);
                StartBusy(loadingMessage);
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                //  Load the model.
                var result = await LoadFile(openFileDialog.FileName);
                StopBusy();
                stopwatch.Stop();

                AddOutput(string.Format("Loaded {0} in {1}, {2} warning(s), {3} error(s).", 
                    Path.GetFileName(openFileDialog.FileName),
                    stopwatch.Elapsed.ToString(),
                    result.Messages.Count(m => m.MessageType == MessageType.Warning),
                    result.Messages.Count(m => m.MessageType == MessageType.Error)));

                ShowResult(result);
            }
        }

        private async Task<FileLoadResult<Scene>> LoadFile(string path)
        {
            return await Task.Run(() => FileFormatObj.Load(path));
        }

        private void StartBusy(string busyText)
        {
            isBusy = true;
            toolStripStatusLabelBusyStatus.Text = busyText;
            toolStripStatusLabelBusyStatus.Visible = true;
            toolStripProgressBarBusy.Visible = true;

            //  Disable UI we don't want offered when busy.
            loadToolStripMenuItem.Enabled = false;
        }

        private void StopBusy()
        {
            toolStripStatusLabelBusyStatus.Text = string.Empty;
            toolStripStatusLabelBusyStatus.Visible = false;
            toolStripProgressBarBusy.Visible = false;
            isBusy = false;

            //  Re-eanble UI we don't want offered when busy.
            loadToolStripMenuItem.Enabled = true;
        }

        private void ShowResult(FileLoadResult<Scene> result)
        {
            //  Write results to the output window.
            int count = 0;
            foreach (var message in result.Messages)
            {
                var messageColour = message.MessageType == MessageType.Error ? Color.Red : Color.DarkOrange;
                AddOutput(string.Format("{0}: {1}", message.MessageType, message.Details), messageColour);
                AddOutput(string.Format("{0}: {1}", message.FileName, message.LineNumber), messageColour);
                count++;
                if (count == 1000)
                {
                    AddOutput("Message count exceeds 1000, no more messages will be displayed.", Color.Red);
                    break;
                }
            }

        PopulateModelTreeView(result.Model);
        }

        private void PopulateModelTreeView(Scene scene)
        {
            //  Populate the model tree view.
            treeViewModel.Nodes.Clear();

            //  Add the vertices, normals and uvs.
            treeViewModel.Nodes.Add(new TreeNode
            {
                Text = "Vertices",
                Tag = scene.Vertices
            });
            treeViewModel.Nodes.Add(new TreeNode
            {
                Text = "UVs",
                Tag = scene.Uvs
            });
            treeViewModel.Nodes.Add(new TreeNode
            {
                Text = "Normals",
                Tag = scene.Normals
            });
            treeViewModel.Nodes.Add(new TreeNode
            {
                Text = "Ungrouped Faces",
                Tag = scene.UngroupedFaces
            });

            //  Add the groups.
            var treeNodeGroups = new TreeNode
            {
                Text = "Groups",
                Tag = scene.Groups
            };
            foreach (var group in scene.Groups)
            {
                var treeNodeGroup = new TreeNode
                {
                    Text = group.Names.Any() ? string.Join(" ", group.Names) : "<unnamed>",
                    Tag = group
                };
                treeNodeGroup.Nodes.Add(new TreeNode
                {
                    Text = "Faces",
                    Tag = group.Faces
                });
                treeNodeGroups.Nodes.Add(treeNodeGroup);
            }
            treeViewModel.Nodes.Add(treeNodeGroups);

            //  Add the materials.
            var treeNodeMaterials = new TreeNode
            {
                Text = "Materials",
                Tag = scene.Materials
            };
            foreach (var material in scene.Materials)
            {
                treeNodeMaterials.Nodes.Add(new TreeNode
                {
                    Text = !string.IsNullOrEmpty(material.Name) ? material.Name : "<unnamed>",
                    Tag = material
                });
            }
            treeViewModel.Nodes.Add(treeNodeMaterials);
        }

        private void ClearOutput()
        {
            richTextBoxOutput.ResetText();
        }

        private void AddOutput(string content)
        {
            richTextBoxOutput.AppendText(content + "\r\n");
        }

        private void AddOutput(string content, Color color)
        {
            richTextBoxOutput.AppendText(content + "\r\n", color);
        }

        private volatile bool isBusy = false;
        private IDetailsUi currentDetailsUi;

        private void treeViewModel_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CreateDetailsUi( e.Node != null ? e.Node.Tag: null);

            
        }

        private void DestroyDetailsUi()
        {
            if (currentDetailsUi != null)
            {
                //  Delete old ui.
                splitContainerTreeAndDetails.Panel2.Controls.Clear();
                currentDetailsUi.Destroy();
            }
        }

        private void CreateDetailsUi(object model)
        {
            DestroyDetailsUi();
            if (model == null)
                return;
            currentDetailsUi = DetailsUiBuilder.BuildUi(model);
            currentDetailsUi.Ui.Dock = DockStyle.Fill;
            splitContainerTreeAndDetails.Panel2.Controls.Add(currentDetailsUi.Ui);
        }
    }
}
