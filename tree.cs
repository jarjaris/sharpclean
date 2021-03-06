﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpclean
{
    public class node
    {
        public node left, right;
        public int id;
        public node() { left = null; right = null; id = -1; }
        public node(int i) { left = null; right = null; id = i; }
    };

    public static class tree
    {
        public static bool insert(node n, int i)
        {
            if (n == null)
            {
                n = new node(i);
                return true;
            }
            node r = n;
            while (n != null)
            {
                if (i < n.id)
                {
                    if (n.left != null)
                        n = n.left;
                    else
                    {
                        n.left = new node(i);
                        n = r;
                        return true;
                    }
                }
                else if (i > n.id)
                {
                    if (n.right != null)
                        n = n.right;
                    else
                    {
                        n.right = new node(i);
                        n = r;
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            n = r;
            return false;
        }

        public static int findNode(node n, int id)
        {
            if (n == null) return -1;
            node r = n;
            while (n != null)
            {
                if (id == n.id)
                {
                    n = r;
                    return id;
                }
                if (id < n.id) n = n.left;
                else if (id > n.id) n = n.right;
            }
            n = r;
            return -1;
        }

        public static void buildTree(List<int> v, node r)
        {
            List<tup> stack = new List<tup>();
            int m, s, e;
            m = (0 + v.Count - 1) / 2;
            insert(r, v[m]);
            tup right = new tup(m + 1, v.Count);
            tup left = new tup(0, m);
            stack.Add(right);
            stack.Add(left);
            while (stack.Count > 0)
            {
                s = stack[stack.Count - 1].s;
                e = stack[stack.Count - 1].e;
                stack.RemoveAt(stack.Count - 1);
                if (s < e)
                {
                    m = (s + e) / 2;
                    insert(r, v[m]);
                    right.change(m + 1, v.Count);
                    left.change(0, m);
                    stack.Add(right);
                    stack.Add(left);
                }
            }
        }

        public static void getInOrder(node n, List<int> v)
        {
            node r = n;
            List<node> s = new List<node>();
            while (n != null || s.Count > 0)
            {
                while (n != null)
                {
                    s.Add(n);
                    n = n.left;
                }
                n = s[s.Count - 1];
                s.RemoveAt(s.Count - 1);
                v.Add(n.id);
                n = n.right;
            }
            n = r;
        }

        /*
        void Tree::print(node*& n)
        {
	        if (n == null) return;
	        print(n.left);
	        std::cout << n.id << " ";
	        print(n.right);
        }

        int Tree::total(node*& n)
        {
	        if (n == null) return 0;
	        int lt = total(n.left);
	        int rt = total(n.right);
	        return rt + lt + 1;
        }

        int tree::get(node * n, pixel& c)
        {
	        switch (c) {
	        case 1: return height(n);
	        case 2: return min(n);
	        case 3: return max(n);
	        case 4: return total(n);
	        }
	        return -1;
        }

        int tree::min(node* n)
        {
	        if (n.left != null) return min(n.left);
	        return n.data[0];
        }

        int tree::max(node* n)
        {
	        if (n.right != null) return max(n.right);
	        return n.data[0];
        }
        */
    }
}