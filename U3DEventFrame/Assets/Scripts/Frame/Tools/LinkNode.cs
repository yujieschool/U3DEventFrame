using UnityEngine;
using System.Collections;


namespace U3DEventFrame
{

    public class LinkNode<T>
    {
        private T data;
        private LinkNode<T> next;


        public LinkNode<T> Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;

            }
        }

        public T Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;

            }
        }
        public LinkNode()
        {
            data = default(T);
            next = null;
        }

        public LinkNode(T obj)
        {
            data = obj;
            next = null;
        }

        public LinkNode(T obj, LinkNode<T> pNext)
        {
            data = obj;
            next = pNext;
        }

        public void Dispose()
        {
            this.data = default(T);

            this.next = null;
        }

    }

    public class IlinkList<T> 
    {
        private LinkNode<T> head;


        public LinkNode<T> Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }

        public IlinkList()
        {
            head = null;
        }

        public IlinkList(T obj)
        {
            head = new LinkNode<T>(obj);
        }

        public void Append(T obj)
        {

            if (head == null)
            {
                head = new LinkNode<T>(obj);
                Debug.Log("11==" + obj.ToString());
                return;
            }
            if (head.Next == null)
            {
                head.Next = new LinkNode<T>(obj);

                Debug.Log("22=" + obj.ToString());

            }
            else
            {
                LinkNode<T> pNext = head ; //此时为更改原来的指针

              //  LinkNode<T> pNext = new LinkNode<T>();
               // pNext = head;

                while (pNext.Next != null)
                {

                    pNext = pNext.Next;
                }
            
                pNext.Next = new LinkNode<T>(obj);
            }

        }


        public virtual void Dispose()
        {

            if (IsEmpty())
                return;


            LinkNode<T> pNext = head; //此时为更改原来的指针

            //  LinkNode<T> pNext = new LinkNode<T>();
            // pNext = head;

            while (pNext.Next != null)
            {
                LinkNode<T> curNode = pNext.Next;

                pNext = pNext.Next;

                curNode.Dispose();
            }


            if (head != null)
            {
                this.head.Dispose();

               
            }
            


 
        }



        public void Insert(T obj, int i)//增  插入操作
        {
            if (IsEmpty())
                return;

            int length = GetLenght();

            if (i < 0 || i >= length - 1)
            {
                Debug.Log("position error");
            }
            else
            {
                int jj = 0;
                LinkNode<T> pNext = new LinkNode<T>();

                LinkNode<T> node = new LinkNode<T>(obj);
                pNext = head;

                do
                {
                    if (i == jj)
                    {
                        node.Next = pNext.Next;
                        pNext.Next = node;

                        break;
                    }
                    jj++;
                    pNext = pNext.Next;
                }
                while (pNext.Next != null && jj < length - 1);



            }


        }
        public void Remove(int i)//删  删除操作
        {
            if (IsEmpty())
                return;

            int length = GetLenght();

            if (i < 0 || i > length - 1)
            {
                Debug.Log("error point");
            }
            else
            {
                LinkNode<T> pNext = new LinkNode<T>();

                //  LinkNode<T> node  =new LinkNode<T>(obj);
                pNext = head;

                if (i == 0)
                {
                    head = head.Next;
                }
                else if (i == length - 1)
                {
                    for (int j = 0; j < length - 1; j++)
                    {
                        pNext = pNext.Next;
                    }

                    pNext.Next = null;

                }
                else
                {
                    for (int j = 1; j < length - 1; j++)
                    {
                        if (j == i)
                        {
                            pNext.Next = pNext.Next.Next;
                            break;

                        }

                        pNext = pNext.Next;
                    }
                }

            }

        }

        public T GetElemAt(int i) //改
        {

            if (IsEmpty())
                return default(T);


            int jj = 0;

            LinkNode<T> pNext = new LinkNode<T>();

            pNext = head;

            while (pNext.Next != null && jj < i)
            {
                pNext = pNext.Next;
                jj++;

            }

            if (jj == i)
                return pNext.Data;
            else
                return default(T);



        }
        public int Locate(T value) //查
        {
            if (IsEmpty())
                return -1;


            int jj = 0;

            LinkNode<T> pNext = new LinkNode<T>();

            pNext = head;

            while (!pNext.Data.Equals(value) && pNext.Next != null)
            {
                pNext = pNext.Next;

                jj++;
            }

            return jj;

        }

        public int GetLenght()  //求长度
        {

            if (IsEmpty())
                return -1;

            int i = 0;

            LinkNode<T> pNext = new LinkNode<T>();

            pNext = head;

            while (pNext.Next != null)
            {
                pNext = pNext.Next;
                i++;
            }

            return i + 1;
        }

        public void Clear()     // 清空 操作
        {
            if (IsEmpty())
                return;

            head = null;

        }

        public bool IsEmpty()   // 判断是否为空
        {
            if (head == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void ShowMe()  //求长度
        {

            if (IsEmpty())
                return;

            int i = 0;

            LinkNode<T> pNext = new LinkNode<T>();

            pNext = head;

            while (pNext.Next != null)
            {

                Debug.Log(pNext.Data);
                pNext = pNext.Next;
                i++;


            }


        }


    }




}

