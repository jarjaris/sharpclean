using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapsweeper
{
    class edge
    {
        public edge(int w, int t)
        {
            sel = null;
            per = null;
            edg = null;
            numEdges = 0;
            tolerance = 0;
            perimSize = 0;
            fieldSet = false;
            width = w;
            total = t;
        }

        void public detect(List<int>& selection)
        {
        }


        ref node sel, ref per, ref edg;

	    int field[8];

        bool fieldSet;



        std::vector<int> perimeter, stack;

        int numEdges, perimSize, width, total, tolerance;



        std::string edge_err = "::EDGE::error : ";

        std::string edge_warn = "::EDGE::warning : ";
    }
}
