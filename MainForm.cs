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
     * TODO: --g1-- bug where if you have a parent directory containing all the files, and you want to copy/move all those files into a child
     *              directory, it won't work because it ignores scanning the primary/parent directory and just looks for the child.
     *              current resolution to fix this is, have no path in the output, and just containt he source folder.
     * TODO: --g2-- bug where if there is no output path, it writes to wherever that program is running. this leads to a problem where you
     *              do no know where the files are now stored and have to look for them
     * TODO: --g3-- need to make a check for files and their type. the program throws an error when they are not mp3s. this is because taglib
     *              only supports mp3s.
     */
    public partial class MainForm : Form
    {
        DebugLogging debug = new DebugLogging();
        Configuration config = new Configuration();
        BLLProcessing _bllProcessing;

        public MainForm()
        {
            SetupConfigFile.LoadFromFile(ref config);
            InitializeComponent();


            //tbSource.Text = "C:\\Users\\Yex\\Desktop\\vaporwave sort with c# app";
            //tbOutput.Text = "C:\\Users\\Yex\\Desktop\\vaporwave sort with c# app\\SORTED";

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
                } // if
            } // using
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
                } // if
            } // using
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
            ProjectLogging.SetupConfigFile.SaveToFile(ref config);
        } // function SaveToConfig

        private void DoProcessing()
        {
            _bllProcessing.StartProcessing();
        } // function DoProcessing
    }
}
