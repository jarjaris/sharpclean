﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ImageMagick;
using System.Drawing;

namespace sharpclean
{
    // Handles file operations for the program, including opening and saving images, locating paths, and user interfacing
    public class fileOps
    {
        // Initialize the values for paths
        string dirPath;
        string imgPath;
        string trajPath;
        string offsetPath;
        string[] dirFiles;

        #region Public member functions

        // Constructor
        public fileOps()
        {
            dirPath = "";
            imgPath = "";
            trajPath = "";
            offsetPath = "";
        }

        #region Get initial files

        public string getImage()
        {
            // Open the file dialog and save the image and directory paths
            using (OpenFileDialog openFD = new OpenFileDialog())
            {
                // File dialog settings
                openFD.Title = "Select an image file";
                openFD.InitialDirectory = "c:\\Users\\";
                openFD.Filter = "pgm/png files (*.pgm/*.png)|*.pgm;*.png";
                openFD.RestoreDirectory = true;

                // Only save the file and directory path if the user selects "OK"
                if (openFD.ShowDialog() == DialogResult.OK)
                {
                    this.imgPath= openFD.FileName;
                }
            }
            return this.imgPath;
        }

        public string getDir()
        {
            // Gets and sets the directory name
            this.dirPath = Path.GetDirectoryName(this.imgPath);

            // Load files now that the directory has been found
            loadFiles();

            return this.dirPath;
        }

        public string getTraj() // Gets the full trajectory file path
        {
            this.trajPath = getFilebyType(".ply");
            return this.trajPath;
        }

        public string getOffset() // Gets the full info file path
        {
            this.offsetPath = getFilebyType(".info");
            return this.offsetPath;
        }

        #endregion

        public void generatePGMS() // Generates 2 .pgm files from originally selected image
        {
            // Get the length of the path
            int strLength = this.imgPath.Length;

            // Get the substring of the path that doesn't include the file type
            string filePath = this.imgPath.Substring(0, strLength - 4);

            // Using ImageMagick.NET, convert the .png images to .pgm
            using (MagickImage pngMap = new MagickImage(this.imgPath))
            {
                pngMap.Write(filePath + ".pgm");
                pngMap.Write(this.dirPath + "\\" + "temp.pgm");
            }
        }

        public string getStoreInfo(string info)
        {
            // Get the directory name
            string mapFolder = Path.GetFileName(this.dirPath);

            // Initialize return variable
            string storeInfo = "";

            // Get either the store name or the store number
            if (info == "name")
            {
                storeInfo = getStoreName(mapFolder);
            }
            else if (info == "number")
            {
                storeInfo = getStoreNumber(mapFolder);
            }
            return storeInfo;
        }

        #endregion

        #region Private member functions

        private string getStoreName(string folder)
        {
            string storeName;

            // Assign the store name based on their acronym
            if (folder.Substring(0, 2).ToUpper() == "GM")
            {
                storeName = "Giant Martin";
            }
            else if (folder.Substring(0, 2).ToUpper() == "SS")
            {
                storeName = "Stop & Shop";
            }
            else if (folder.Substring(0, 3).ToUpper() == "SNS")
            {
                storeName = "Stop & Shop";
            }
            else
            {
                storeName = "Unknown Store";
            }
            return storeName;
        }

        private string getStoreNumber(string folder)
        {
            string storeNumber = "";

            // The only number in the directory name should be the store number
            for (int i = 0; i < folder.Length; i++)
            {
                if (Char.IsDigit(folder[i]))
                {
                    storeNumber += folder[i];
                }
            }
            return storeNumber;
        }

        private void loadFiles()
        {
            // Save all the files in this directory
            this.dirFiles = Directory.GetFiles(this.dirPath, "*.*", SearchOption.TopDirectoryOnly);
        }

        private string getFilebyType(string fileExt)
        {
            // Iterate through the directory files to find either the trajectory or offset file
            for (int i = 0; i < this.dirFiles.Length; i++)
            {
                if (fileExt == Path.GetExtension(this.dirFiles[i]))
                {
                    return this.dirFiles[i];
                }
            }
            // Should not be reached unless the file is missing
            return "";
        }

        #endregion
    }
}
