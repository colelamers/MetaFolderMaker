using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLogging;
using System.Windows.Forms;

namespace MetaFolderMaker
{
/*
 * Created by Cole Lamers 
 * Date: 2020-12-22
 * 
 * == Purpose ==
 * This code is to make folders based off file metadata and either copy or move them there 
 * 
 * Changes: (date,  comment)
 * 
 *              
 */

    /*
     * == Global Task List ==
     * 
     */
    public partial class MainForm : Form
    {
        DebugLogging debug = new DebugLogging();
        Configuration config = new Configuration();
        BLLProcessing _bllProcessing;

        public MainForm()
        {
            SetupConfigFile.LoadAndSaveFile(ref config);

            InitializeComponent();

            _bllProcessing = new BLLProcessing(ref config, ref debug);

            cbTagList.Items.AddRange(new string[] { 
                "Artist", 
                "Album", 
                "Title", 
            });
            cbTagList.SelectedIndex = 0; //Sets the default box value as Artist
        }

        private void btnSourceBrowse_Click(object sender, EventArgs e)
        {

            debug.LogAction("Source browse button clicked");
            using (var fileDialog = new FolderBrowserDialog())
            {
                fileDialog.SelectedPath = "c:\\"; //sets the default path for the browser to open
                if (fileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fileDialog.SelectedPath))
                {
                    tbSource.Text = fileDialog.SelectedPath;
                }
            }
        }

        private void btnOutputBrowse_Click(object sender, EventArgs e)
        {
            debug.LogAction("Output browse button clicked");
            using (var fileDialog = new FolderBrowserDialog())
            {
                fileDialog.SelectedPath = "c:\\"; //sets the default path for the browser to open
                if (fileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fileDialog.SelectedPath))
                {
                    tbOutput.Text = fileDialog.SelectedPath;
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            SaveToConfig();
            debug.LogAction("Run button clicked");
            DoProcessing();
        }

        private void SaveToConfig()
        {
            config.Source = tbSource.Text;
            config.Output = tbOutput.Text;
            config.Tag = cbTagList.Text;
            config.FileAction = gbRadioButtons.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
        }

        private void DoProcessing()
        {
            _bllProcessing.StartProcessing();
        }
    }
}
