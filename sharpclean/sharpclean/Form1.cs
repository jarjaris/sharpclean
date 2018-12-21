using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sharpclean
{
    public partial class Form1 : Form
    {
        fileOps mapCleanup = new fileOps();
        string mapPath;
        string dirPath;
        string trajPath;
        string offsetPath;

        public Form1()
        {
            InitializeComponent();
        }

        // This is the button click to save the file paths and load the image into the picture box
        private void button1_Click(object sender, EventArgs e)
        {
            // Assign map path by bringing up file dialog
            this.mapPath = this.mapCleanup.getImage();

            //Assign the image to the picture box
            pictureBox1.Image = Image.FromFile(this.mapPath);
            
            // Assign the directory path and loads the trajectory and offset files
            this.dirPath = this.mapCleanup.getDir();

            // Assign the paths for the offset and trajectory files
            this.offsetPath = mapCleanup.getOffset();
            this.trajPath = mapCleanup.getTraj();

            // Generate 2 .pgm files (one is temporary, the other may or may not be saved)
            mapCleanup.generatePGMS();

            // Make the store info headers visible
            label2.Visible = true;
            label3.Visible = true;

            // Populate the labels with the store name and the store number
            label4.Text = mapCleanup.getStoreInfo("name");
            label5.Text = mapCleanup.getStoreInfo("number");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This cleans the map, replace this with actual code!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This saves the cleaned map, replace this with actual code!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This saves the cleaning data to an excel file, replace this with actual code!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Maybe we want to have a readme doc for FAQ and assistance on GITHUB and this links to it???
            MessageBox.Show("This displays the help feature, replace this with actual help instructions!");
        }
    }
}
