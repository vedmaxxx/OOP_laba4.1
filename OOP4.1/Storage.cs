using System;
using System.Drawing;


namespace OOP4
{
    public class Base
    {
		public virtual char getCode()
        {
            return 'B';
        }
	    public virtual void print(int i,Graphics gr)
        {

        }
    }
    class MyBaseFactory
	{
        private const char V = 'C';

        public MyBaseFactory() { }
		public Base createBase(Base p)
		{
			Base _base = null;
			switch (p.getCode())
			{
                case 'C':
					_base = new CCircle((CCircle)p);
					break;
				default:
                    break;
			}
			return _base;
		} 
	}

    public class Mylist
    {

        public class Node
        {
            public Base base_=null;
            public Node next=null; //указатель на следующую ячейку списка

            public Node(Base _base)
            {
                MyBaseFactory factory = new MyBaseFactory();
                base_ = factory.createBase(_base);
            }

            public bool isEOL() { return Convert.ToBoolean(this == null ? 1 : 0); }
        };

        public void delete_first()
        {
            if (isEmpty()) return;

            Node temp = first;
            first =temp.next;
        }
        public void delete_last()
        {
            if (isEmpty()) return;
            if (last == first)
            {
                delete_first();
                return;
            }

            Node temp =first;
            while (temp.next != last)
            {
                temp = temp.next;
            }
            temp.next = null;
            last = temp;
        }

        public Node first=null;

        public Node last=null;

        public void add(Base _base)
        {
            Node another = new Node(_base);
            //("\tЭлемент добавлен в хранилище\n");

            if (isEmpty())
            {
                first = another;
                last = another;
                return;
            }
            last.next = another;
            last = another;
        }
        public bool isEmpty()
        {
            return first == null;
        }
        public void deleteObj(Base _base)
        {
            if (isEmpty())
            {
                Console.WriteLine("\tХранилище пусто, удалить не удалось\n");
                return;
            }
            if (last.base_ == _base) {
                delete_last();
                Console.WriteLine("\tЭлемент удален\n");
                return;
            }
            if (first.base_ == _base) {
                delete_first();
                Console.WriteLine("\tЭлемент удален\n");
                return;
            }

            Node current = first;
            while (current.next != null && current.next.base_ != _base) {
                current = current.next;
            }
            if (current.next == null)
            {
                Console.WriteLine("\tТакого элемента нет в списке\n");
                return;
            }
            Node tmp_next = current.next;
            current.next =
                current.next.next;
                
            Console.WriteLine("\tЭлемент удален\n");
        }
        public int getSize()
        {
            if (first == null) return 0;
            Node node = first;
            int i = 1;
            //было while (node!=null)
            while (node.next!=null)
            {
                i++;
                node = node.next;
            }
            return i;
        }

        public Base getObj(int i)
        {
            if (isEmpty())
            {
                Console.WriteLine("Хранилище пусто, возвращать нечего\n");
                return null;//исправить на исключение
            }
            int j = 0;
            Node current = first;
            //while (j < (i + 1) && !(current.isEOL())) {
            while (j < i && !(first.isEOL()))
            {

                current = current.next;
                j++;
            }
            Console.WriteLine("\tОбъект передан\n");
            return (current.base_);
        }
        public Base getObjAndDelete(int i)
        {
            if (isEmpty())
            {
                Console.WriteLine("\tХранилище пусто, возвращать нечего\n");
                return null;//исправить на исключение
            }
            Base ret = getObj(i);
            Base tmp;
            MyBaseFactory factory = new MyBaseFactory();
            tmp = factory.createBase(ret);
            deleteObj(ret);
            Console.WriteLine("\tОбъект передан\n");
            return tmp;
        }
    };

    public class CCircle : Base
    {
        
        Pen blackpen;
        Pen redpen;
        Pen darkGoldpen;
        

        private int x, y;
        private int R;
        public bool Selected;
        public override char getCode()
        {
            return 'C';
        }
        public void initcomp()
        {
            blackpen = new Pen(Color.Black);
            blackpen.Width = 2;
            redpen = new Pen(Color.Red);
            redpen.Width = 2;
            darkGoldpen = new Pen(Color.DarkGoldenrod);
            darkGoldpen.Width = 2;
            R = 20;
        }


        public CCircle(int x, int y,Mylist mylist, bool isCTRL)
        {

            initcomp();
            bool flag = true;
            int i;
            double tmp = 0 ;
            for ( i = 0; i < mylist.getSize(); i++)
            {
                tmp = Math.Pow((((CCircle)mylist.getObj(i)).x - x), 2) + Math.Pow(((CCircle)mylist.getObj(i)).y - y, 2);
                if (tmp <= (4*R*R))
                {
                    flag = false;
                    
                    break;
                } 
            }
            if (flag)
            {
                this.x = x;
                this.y = y;
                RefreshSelectedCircles();
                Selected = true;
                mylist.add(this);
            }
            else
            {
                if (tmp < R * R)
                {
                    if (!isCTRL)
                    {
                        RefreshSelectedCircles();
                    }
                    ((CCircle)mylist.getObj(i)).Selected = true;
                }
            }

            void RefreshSelectedCircles()
            {
                for (int j = 0; j < mylist.getSize(); j++)
                {
                    ((CCircle)mylist.getObj(j)).Selected = false;
                }
            }
        }
        public CCircle(CCircle copy)
        {
            initcomp();
            x = copy.x;
            y = copy.y;
            Selected = copy.Selected;
        }


        public void deleteSelected(Mylist list)
        {
            if (Selected) list.deleteObj(this);
        }

        public void drawCircle(int x, int y, Graphics gr)
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackpen, (x - R), (y - R), 2 * R, 2 * R);
        }
        
        public void drawSelectedVert(int x, int y, Graphics gr)
        {
            gr.DrawEllipse(redpen, (x - R), (y - R), 2 * R, 2 * R);
        }
        public override void print(int i, Graphics gr) 
        {
            drawCircle(x, y, gr);
            if (Selected)
            {
                drawSelectedVert(x,y,gr);
            }
        }
    }
};
