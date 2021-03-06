﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpclean
{
    public static class confidence
    {
        public static conf getconfidence(double[] d)
        {
            //avg, size, edge
            conf c = new conf();
            value(c, d[0]);
            size(c, d[1]);
            edges(c, d[2]);
            c.dust = c.d_edge + c.d_size + c.d_val;
            c.obj = c.o_edge + c.o_size + c.o_val;
            if (c.dust > c.obj)
                c.isObj = false;
            else
                c.isObj = true;
            return c;
        }
        private static void edges(conf c, double d)
        {
            if (d < 18.2) c.d_edge += 95.00;
            else if (d < 30.2) c.d_edge += 77.35;
            else if (d < 42.2) c.d_edge += 61.13;
            else if (d < 54.2) c.d_edge += 40.75;
            else if (d < 66.2) c.d_edge += 20.37;
            else if (d < 78.2) c.d_edge += 7.54;
            else if (d < 90.2) c.d_edge += 1.88;
            else c.d_edge += 1.00;

            if (d < 44.85) c.o_edge += 14.28;
            else if (d < 66.85) c.o_edge += 29.67;
            else if (d < 88.85) c.o_edge += 47.36;
            else if (d < 98.85) c.o_edge += 59.56;
            else c.o_edge += 95.00;
        }

        private static void size(conf c, double d)
        {
            if (d < 30) c.d_size += 99.00;
            else if (d < 331) c.d_size += 45.68;
            else if (d < 631) c.d_size += 32.42;
            else if (d < 931) c.d_size += 25.41;
            else if (d < 1200) c.d_size += 15.00;
            else if (d < 1800) c.d_size += 11.00;
            else if (d < 2100) c.d_size += 7.00;
            else c.d_size += 1.00;

            if (d < 100) c.o_size += 1.00;
            else if (d < 897) c.o_size += 10.65;
            else if (d < 1637) c.o_size += 35.30;
            else if (d < 2377) c.o_size += 73.23;
            else if (d < 2757) c.o_size += 90.00;
            else c.o_size += 99.00;

            c.d_size -= 25;
            c.o_size -= 25;
        }

        private static void value(conf c, double d)
        {
            if (d < 30) c.d_val += 1.00;
            else if (d < 84) c.d_val += 8.41;
            else if (d < 102) c.d_val += 19;
            else if (d < 120) c.d_val += 30.84;
            else if (d < 138) c.d_val += 41;
            else if (d < 156) c.d_val += 52.99;
            else if (d < 192) c.d_val += 82.89;
            else if (d < 228) c.d_val += 95.32;
            else c.d_val += 99.00;

            if (d < 40) c.o_val += 99.00;
            else if (d < 70) c.o_val += 75.82;
            else if (d < 78) c.o_val += 68.82;
            else if (d < 85) c.o_val += 60.00;
            else if (d < 93) c.o_val += 53.00;
            else if (d < 99) c.o_val += 45.05;
            else if (d < 128) c.o_val += 35.7;
            else if (d < 157) c.o_val += 20.48;
            else if (d < 186) c.o_val += 10.59;
            else c.o_val += 1.00;

            c.d_val += 15;
            c.o_val += 15;
        }
    }
}