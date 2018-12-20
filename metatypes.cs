using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpclean
{
    //the .pgm file's basic data
    struct data
    {
        public string filetype;
        public int width, height, maxgreyval;
        public int totalpixels;
    };
    //used for the menu's when the user selects to print certain data
    enum info
    {
        ALL, FILETYPE, DIMENSIONS, TOTALPIXELS, MAXGREYVAL
    };
    //a basic pixel class
    class pixel
    {
        public bool selected = false;  //used for selection
        public bool found = false;
        public byte value = 255;    //grey value
        public long id = -1;            //ID [0->totalpixels]
    };
    //edge and filler use this for navigation around the pixel map
    enum direction
    {
        none, up, down, left, right
    };
    //same deal, a little more abstract
    class pathDirection
    {
        public direction dir = direction.none;
        public int id = -1;

        pathDirection(direction d, int i) { dir = d; id = i; }
    };
    //each pixel has eight neighbors
    class octan
    {
        public int tl = -1, t = -1, tr = -1,
                   l = -1, r = -1,
                   bl = -1, b = -1, br = -1;
    };
    //same deal, different use
    enum neighbor
    {
        tl, t, tr,
        l, r,
        bl, b, br
    };
    //confidence class
    class conf
    {
        public double   obj = 0.0, dust = 0.0,
                        o_size = 0.0, d_size = 0.0,
                        o_edge = 0.0, d_edge = 0.0,
                        o_val = 0.0, d_val = 0.0;
        public bool isObj;
    };
    //don't remember, too scared to delete
    /*class Tup
    {
        public int s, e;
        Tup(int st, int en) { s = st; e = en; }
    };*/
}
