using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sharpclean
{
    public class Filler
    {
        public Filler()
        {
            topmost = -1;
            bottommost = -1;
            leftmost = -1;
            rightmost =-1;
            pathSize = 0;
            boundsError = false;
            System.Console.WriteLine(filler_warn + "object created with out passing pixels and information.\n");
        }
        public Filler(pixel[] p, int imageWidth, int totalPixels)
        {
            topmost = totalPixels;
            bottommost = -1;
            leftmost = imageWidth;
            rightmost = -1;
            pathSize = 0;
            boundsError = false;
            getPixels(p);
            width = imageWidth;
            total = totalPixels;
        }
        public void getPixels(pixel[] p)
        {
            this->p = p;
        }
        public void getBounds(int id)
        {
            if ((id % width) < leftmost) leftmost = id % width;

            if ((id % width) > rightmost) rightmost = id % width;

            if (id < topmost) topmost = id;

            if (id > bottommost) bottommost = id;

        }
        private bool inbounds(int id)
        {
            if (id % width <= leftmost
            || id % width >= rightmost
            || id < topmost
            || id > bottommost)
                return false;

            return true;
        }
        public void start(int id)
        {
            path.Clear();
            pathSize = 0;
            boundsError = false;

            addtoPath(pathDirection(none, id));

            if (boundsError)
            {
                clearPath();
                return;
            }

            iteratePath();
        }
        private void addtoPath(pathDirection pd)
        {
            if (!inbounds(pd.id)) boundsError = true;
            else
            {
                path.Add(pd);
                pathSize++;
                p[pd.id].selected = true;
            }
        }
        private void iteratePath()
        {
            for (int i = 0; i < pathSize; i++)
            {
                check(path[i]);

                if (boundsError)
                {
                    clearPath();
                    return;
                }
            }
        }
        private void check(pathDirection pd)
        {
            if (pd.id < 0)
            {
                System.Console.WriteLine(pd.id + "\n");
            }

            switch (pd.dir) //0 none, 1 up, 2 down, 3 left, 4 right
            {
                case 1:
                    {
                        if (pd.id - width > 0)
                        {
                            if (!p[pd.id - width].selected && !p[pd.id - width].found) addtoPath(pathDirection(up, pd.id - width)); //up
                        }
                        if (pd.id % width != 0)
                        {
                            if (!p[pd.id - 1].selected && !p[pd.id - 1].found) addtoPath(pathDirection(left, pd.id - 1));       //left
                        }
                        if ((pd.id + 1) % width != 0)
                        {
                            if (!p[pd.id + 1].selected && !p[pd.id + 1].found) addtoPath(pathDirection(right, pd.id + 1));      //right
                        }
                        break;
                    }
                case 2:
                    {
                        if (pd.id + width < total)
                        {
                            if (!p[pd.id + width].selected && !p[pd.id + width].found) addtoPath(pathDirection(down, pd.id + width));   //down
                        }
                        if (pd.id % width != 0)
                        {
                            if (!p[pd.id - 1].selected && !p[pd.id - 1].found) addtoPath(pathDirection(left, pd.id - 1));       //left
                        }
                        if ((pd.id + 1) % width != 0)
                        {
                            if (!p[pd.id + 1].selected && !p[pd.id + 1].found) addtoPath(pathDirection(right, pd.id + 1));      //right
                        }
                        break;
                    }
                case 3:
                    {
                        if (pd.id - width > 0)
                        {
                            if (!p[pd.id - width].selected && !p[pd.id - width].found) addtoPath(pathDirection(up, pd.id - width)); //up
                        }
                        if (pd.id + width < total)
                        {
                            if (!p[pd.id + width].selected && !p[pd.id + width].found) addtoPath(pathDirection(down, pd.id + width));   //down
                        }
                        if (pd.id % width != 0)
                        {
                            if (!p[pd.id - 1].selected && !p[pd.id - 1].found) addtoPath(pathDirection(left, pd.id - 1));       //left
                        }
                        break;
                    }
                case 4:
                    {
                        if (pd.id - width > 0)
                        {
                            if (!p[pd.id - width].selected && !p[pd.id - width].found) addtoPath(pathDirection(up, pd.id - width)); //up
                        }
                        if (pd.id + width < total)
                        {
                            if (!p[pd.id + width].selected && !p[pd.id + width].found) addtoPath(pathDirection(down, pd.id + width));   //down
                        }
                        if ((pd.id + 1) % width != 0)
                        {
                            if (!p[pd.id + 1].selected && !p[pd.id + 1].found) addtoPath(pathDirection(right, pd.id + 1));      //right
                        }
                        break;
                    }
                case 0:
                    {
                        if (pd.id - width > 0)
                        {
                            if (!p[pd.id - width].selected && !p[pd.id - width].found) addtoPath(pathDirection(up, pd.id - width)); //up
                        }
                        if (pd.id + width < total)
                        {
                            if (!p[pd.id + width].selected && !p[pd.id + width].found) addtoPath(pathDirection(down, pd.id + width));   //down
                        }
                        if (pd.id % width != 0)
                        {
                            if (!p[pd.id - 1].selected && !p[pd.id - 1].found) addtoPath(pathDirection(left, pd.id - 1));       //left
                        }
                        if ((pd.id + 1) % width != 0)
                        {
                            if (!p[pd.id + 1].selected && !p[pd.id + 1].found) addtoPath(pathDirection(right, pd.id + 1));      //right
                        }
                        break;
                    }
            }
        }
        public void printBounds()
        {
            int x, y;
            x = leftmost % width;
            y = leftmost / width;
            System.Console.WriteLine("\nl: %i(%i, %i),", leftmost, x, y);

            x = topmost % width;
            y = topmost / width;
            System.Console.WriteLine(" u: %i(%i, %i),", topmost, x, y);

            x = rightmost % width;
            y = rightmost / width;
            System.Console.WriteLine(" r: %i(%i, %i),", rightmost, x, y);

            x = bottommost % width;
            y = bottommost / width;
            System.Console.WriteLine(" b: %i(%i, %i)\n", bottommost, x, y);
        }
        public ref List<pathDirection> getPath()
        {
            return path;
        }
        private void clearPath()
        {
            for (int i = 0; i < path.Count(); i++)
            {
                if (p[path[i].id].value <= uint8_t(255))
                {
                    p[path[i].id].selected = false;
                }
                else
                {
                    p[path[i].id].found = true;
                    foundBuffer.Add(path[i].id);
                }
            }

            path.Clear();
            pathSize = 0;
        }
        public void clearFoundBuffer()
        {
            for (int i = 0; i < foundBuffer.Count(); i++)
            {
                p[foundBuffer[i]].found = false;
                p[foundBuffer[i]].selected = false;
            }
            foundBuffer.Clear();
        }
        pixel[] p;
        int width, total;
        int topmost, bottommost, leftmost, rightmost;
        List<pathDirection> path;
        List<int> foundBuffer;
        int pathSize;
        bool boundsError;
        private readonly string filler_err = "::FILLER::error : ";
        private readonly string filler_warn = "::FILLER::warning : ";
    }

}
