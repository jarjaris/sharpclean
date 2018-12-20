using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpclean
{
    class toolbox
    {
        toolbox(pixel[] p, int width, int total)
        {
            pixels = p;
            imageWidth = width;
            totalPixels = total;
        }
               
        //gets some info for saving data, then taps run()
        public void clean()
        {
            if (pixels == null) { std::cout << toolbox_err << "no pixels loaded\n"; return; }

            int n;
            ofilename = "none";

            if (cmd.getcmd("write data to .csv file? [1]yes, [2]no, [q]quit - ", n, 2))
            {
                if (n == 1)
                    cmd.getfile("enter data output file name : ", ofilename, ".csv", 2);

                run();
            }
        }

        //the big boy, iterates through the pixels and drives algorithms
        private void run();

        //cleans a selection of pixels
        private void cleanSelection(const uint8_t, const int);

        //colors a selection of pixels
        private void colorbuffer(const uint8_t, const int);

        //colors the edges of a selection of pixels
        private void coloredges(const uint8_t, const int);

        //writes some data to a csv, if the user wants
        private void printcsv(conf&);

        //gets some data on the selection
        private double getAverageValue(const int);  

        private image img = null;
        private pixel[] pixels = null;
        private command cmd;

        private int imageWidth, totalPixels;
        private List<int> buffer, perimeter;
        private double[] data = new double[3]; //average value, size, number of edges

        private string ofilename;
        private System.IO.StreamReader ofile;

        private readonly string toolbox_err = "::TOOLBOX::error : ";
        private readonly string toolbox_msg = "::TOOLBOX::message : ";
    }
}
