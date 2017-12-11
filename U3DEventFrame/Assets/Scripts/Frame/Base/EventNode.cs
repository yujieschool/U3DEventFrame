using System;
using System.Collections.Generic;


namespace U3DEventFrame
{


    public class NodeBase
    {

        public NodeBase next;

        public NodeBase()
        {
            this.next = null;
        }
        public NodeBase(NodeBase tmpNext)
        {

            this.next = tmpNext;
        }

        public virtual void Dispose()
        {
            next = null;
        }

        /// <summary>
        /// 子类最好都重写 == 这个方法
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        //public static bool operator ==(NodeBase lhs, NodeBase rhs)
        //{
        //    if (lhs == null || rhs == null)
        //        return false;

        //    if (lhs.next == rhs.next)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public static bool operator !=(NodeBase lhs, NodeBase rhs)
        //{

        //    if (lhs == null || rhs == null)
        //        return true;
        //    if (lhs.next == rhs.next)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}


    }

    public class NodeManagerBase
    {
        public   Dictionary<string, NodeBase> manager = null;


        public NodeManagerBase()
        {
            manager = new Dictionary<string, NodeBase>();
        }


        public bool ContainsKey(string name)
        {
            return manager.ContainsKey(name);
        }



        public void   AddNode(string nodeKey,NodeBase values )
        {
            if (manager.ContainsKey(nodeKey))
            {
                NodeBase topNode = manager[nodeKey];

                while (topNode.next != null)
                {
                    topNode = topNode.next;
                }

                topNode.next = values;


            }
            else
            {
                manager.Add(nodeKey, values);
            }
        }


        /// <summary>
        ///  释放一个key 链
        /// </summary>
        /// <param name="bundle"></param>
        public void ReleaseNode(string bundle)
        {
            if (manager.ContainsKey(bundle))
            {
                NodeBase topNode = manager[bundle];

                // 挨个释放
                while (topNode.next != null)
                {
                    NodeBase curNode = topNode;

                    topNode = topNode.next;


                    curNode.Dispose();
                }
                // 最后一个结点的释放
                topNode.Dispose();


                manager.Remove(bundle);
            }
        }


        public  virtual void VisitorListNodes(string  key)
        {
            if (manager.ContainsKey(key))
            {

                NodeBase topNode = manager[key];


                do
                {
  
                    topNode = topNode.next;
                }
                while (topNode != null);

            }

        }




        /// <summary>
        /// 释放具体的一个node  
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"> 这个node 最好重写 == 这个符号</param>
        public void ReleaseNode(string key,NodeBase node)
        {
            if (!manager.ContainsKey(key))
            {
                return;
            }
            else
            {

                NodeBase tmp = manager[key];





                if (tmp == node)
                {

                    NodeBase header = tmp;

                    //头部
                    if (header.next != null)
                    {
                        manager[key] = tmp.next; //直接指向下一个
                        header.next = null; // 把第一个直接指向空

                    }
                    else
                    {
                        manager.Remove(key);
                    }
                }
                else
                {


                    while (tmp.next != null && tmp.next != node)
                    {
                        tmp = tmp.next;
                    }//直到找到 这个node 为止

                    if (tmp.next.next != null)
                    {
                        NodeBase curNode = tmp.next;
                        tmp.next = curNode.next; //指向的删除

                        curNode.next = null;// 下一个删除
                      
                    }
                    else
                    {
                        //
                        tmp.next = null;
                    }

                }



            }
        }



        public void Dispose()
        {

            manager.Clear();
        }




    }



    public class EventNode
    {

        // mono behaviours
        public MonoBase data;


        public EventNode next;

        public EventNode(MonoBase tmp)
        {
            this.data = tmp;

            this.next = null;
        }


    }
}




