using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpclean
{
    class image
    {
        public bool load(string filename)
        {
            System.IO.StreamReader infile = null;

            try {
                infile = new System.IO.StreamReader(filename);
            }
            catch (System.IO.FileNotFoundException) {
                Console.WriteLine(image_err + "could not open file: " + filename + "\n");
                return false;
            }

            //first line is always version
            mdata.filetype = infile.ReadLine().Substring(0, 2);

            if (!(mdata.filetype != "P5" || mdata.filetype != "P2")) {
                Console.WriteLine(image_err + "invalid file type: " + mdata.filetype + "\n");
                return false;
            }

            //ignore comments
            string comments = "";
            while (true) {
                comments = infile.ReadLine();
                if (comments[0] != '#') break;
            }

            //get width, height, and total
            string[] ss = infile.ReadLine().Split();

            mdata.width  = int.Parse(ss[0]);
            mdata.height = int.Parse(ss[1]);
            mdata.totalpixels = mdata.width * mdata.height;

            //get maximum grey value in file
            mdata.maxgreyval = Convert.ToChar(infile.ReadLine());

            //get image data
            if (mdata.filetype == "P2") loadP2(infile);
            else loadP5(infile);

            
            Console.WriteLine("successfully loaded...\n");
            dataLoaded = true;
            
            return true;
        }

        public void write(string filename)
        {
            if (!dataLoaded) {
                Console.WriteLine(image_err + "image data not loaded\n");
                return;
            }

            if (mdata.filetype == "P2") {
                System.IO.File.WriteAllText(filename, mdata.filetype + "\n" + mdata.width + " " + mdata.height + "\n" + mdata.maxgreyval + "\n");

                for (int i = 0; i < mdata.totalpixels; i++)
                    System.IO.File.WriteAllText(filename, Convert.ToString(pixels[i].value) + "\n");
            }
            else {
                System.IO.File.WriteAllText(filename, mdata.filetype + "\n" + mdata.width + " " + mdata.height + "\n" + mdata.maxgreyval + "\n");

                for (int i = 0; i < mdata.totalpixels; i++) //ofile >> std::hex >> pixels[i].value;
                    System.IO.File.WriteAllText(filename, Convert.ToString(pixels[i].value));
            }
        }

        private void loadP2(System.IO.StreamReader f)
        {
            pixels = new pixel[mdata.totalpixels];
            string line;
            int i = 0;

            while ((line = f.ReadLine()) != null) {
                pixels[i].value = Convert.ToByte(line);
                pixels[i].id = i;
                i++;
            }
        }

        private void loadP5(System.IO.StreamReader f)
        {
            pixels = new pixel[mdata.totalpixels];

            char[] buffer = new char[mdata.totalpixels];

            buffer = f.ReadLine().ToCharArray();

            for (int i = 0; i < mdata.totalpixels; i++)
            {
                pixels[i].value = Convert.ToByte(buffer[i]);
                pixels[i].id = i;
            }
        }

        public void printmenu()
        {
            if (!dataLoaded) {
                Console.WriteLine(image_err + "image data not loaded\n");
                return;
            }

            int n = 0;
            if (cmd.getcmd("[1]print all, [2]image data menu, [q]quit - ", ref n, 1)) {
                switch (n)
                {
                    case 1: print(); break;
                    case 2: printChoice(); break;
                }
            }
        }

        private void print()
        {
            for (int i = 0; i < mdata.totalpixels; i++)
                Console.WriteLine((Convert.ToChar(pixels[i].value) + " ").PadRight(4));
        }

        private void printChoice()
        {
            int n = 0;
            if (cmd.getcmd("[1]all [2]file type, [3]dimensions, [4]total pixels, [5]max grey value - ", ref n, 2))
                print(n - 1);
        }

        private void print(int i)
        {
            switch (i)
            {
                case 0:
                    {
                        print(Convert.ToInt16(info.FILETYPE));
                        print(Convert.ToInt16(info.DIMENSIONS));
                        print(Convert.ToInt16(info.TOTALPIXELS));
                        print(Convert.ToInt16(info.MAXGREYVAL));
                        break;
                    }
                case 1: Console.WriteLine("file type: " + mdata.filetype + "\n"); break;
                case 2: Console.WriteLine("width: %i, height: %i\n", mdata.width, mdata.height); break;
                case 3: Console.WriteLine("total pixels: %i\n", mdata.totalpixels); break;
                case 4: Console.WriteLine("max grey value: %i\n", mdata.maxgreyval); break;
            }
        }

        public pixel[] getpixels()
        {
            return pixels;
        }

        public ref data getImageData()
        {
            return ref mdata;
        }

        public bool getDataLoaded()
        {
            return dataLoaded;
        }

        private bool dataLoaded = false;
        private data mdata;
        private pixel[] pixels;
        private command cmd;

        private readonly string image_err = "::IMAGE::error : ";
        private readonly string image_msg = "::IMAGE::message : ";
    }
}
