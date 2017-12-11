using UnityEngine;
using System.Collections;

namespace U3DEventFrame

{
    public class BackLinkList : IlinkList<CheckBackMsg>
    {



        MsgBase sendBackMsg;



        public BackLinkList()
        {

            sendBackMsg = new MsgBase();


        }


        public LinkNode<CheckBackMsg> FindObj(int tmpId)
        {

            
            LinkNode<CheckBackMsg> pNext = this.Head;


            if (pNext.Data.msgId == tmpId)
            {

                return pNext;
            }


            while (pNext.Next != null)
            {
                LinkNode<CheckBackMsg> tmpNode = pNext.Next;

                if (tmpNode.Data.msgId == tmpId)
                {

                    return tmpNode;
                }
                pNext = pNext.Next;


            }


            return null;



        }

        public void RemoveCondition(float timer)
        {


            if (IsEmpty())
                return;


            LinkNode<CheckBackMsg> pNext = this.Head;

            //  LinkNode<T> node  =new LinkNode<T>(obj);






            // 首先释放头部以后   
            while (pNext.Next != null)
            {


                LinkNode<CheckBackMsg> tmpNode = pNext.Next;

                if (tmpNode.Data.ReduceTime(timer))
                {
                    sendBackMsg.ChangeEventId(tmpNode.Data.msgId);
                    MsgCenter.instance.SendToMsg(sendBackMsg);
                    pNext.Next = tmpNode.Next;
                    tmpNode.Dispose();


                }
                else
                {
                    pNext = pNext.Next;
                }



            }

            // 判断头部是否
            if (this.Head.Data.ReduceTime(timer))
            {

                LinkNode<CheckBackMsg> tmpNode = this.Head;

                sendBackMsg.ChangeEventId(tmpNode.Data.msgId);
                MsgCenter.instance.SendToMsg(sendBackMsg);

                this.Head = this.Head.Next;
                tmpNode.Dispose();

            }



        }
    }



}

