using UnityEngine;
using System.Collections;
using System.Collections.Generic ;



namespace U3DEventFrame
{



    public class ManagerBase : MonoBase
    {



      //  public static ManagerBase instance;


        #region   delay timer  back



        BackLinkList checkTimer;
       // Dictionary<ushort, CheckBackMsg> checkTimer;


        public void ProcessSendBackMsg(int  tmpId)
        {

            if (tmpId != -1)
            {
                LinkNode<CheckBackMsg> tmpMsg = checkTimer.FindObj(tmpId);


                if (tmpMsg != null)
                {
                    tmpMsg.Data.Reset();
                }
                else
                {

                    checkTimer.Append(new CheckBackMsg((ushort)tmpId));
                }
            }

        }


        public void BackMsgUpdate()
        {

            if (checkTimer != null)

            checkTimer.RemoveCondition(Time.deltaTime);

          

        }

        void Update()
        {

            BackMsgUpdate();

        }


      

        public void InitialBackMsg()
        {

            checkTimer = new BackLinkList();
    
        }


        #endregion

        void OnDestroy()
        {

            if(checkTimer != null)
            checkTimer.Dispose();
            List<ushort> tmpKeys = new List<ushort>(eventTree.Keys);

            for (int i = 0; i < tmpKeys.Count; i++)
            {

                EventNode tmpBase = eventTree[tmpKeys[i]];

                tmpBase = null;



            }


            eventTree.Clear();

            System.GC.Collect();

        }



        public override void ProcessEvent(MsgBase msg)
        {

            // 没有注册
            if (!eventTree.ContainsKey(msg.msgId))
            {
                Debug.LogWarning("msg not contain  msgid =" + msg.msgId);

                Debug.LogWarning("msg manager  =" + msg.GetManager());
                return;
            }
            else
            {


                // Debug.Log("  msgid =" + msg.msgId);
                EventNode tmp = eventTree[msg.msgId];


                do
                {

                    //ITools.Debugger(" asset Coming");
                    tmp.data.ProcessEvent(msg);


                    tmp = tmp.next;

                }
                while (tmp != null);



            }

        }









        public void UnRegistMsg(MonoBase mono, params ushort[] msgs)
        {

       
           
            for (int i = 0; i < msgs.Length; i++)
            {

                UnRegistMsg(msgs[i], mono);

            }

        }


        private void UnRegistMsg(ushort id, MonoBase node)
        {


            if (!eventTree.ContainsKey(id))
            {
                return;
            }
            else
            {

                EventNode tmp = eventTree[id];





                if (tmp.data == node)
                {

                    EventNode header = tmp;

                    //头部
                    if (header.next != null)
                    {
                        eventTree[id] = tmp.next; //直接指向下一个
                        header.next = null; // 把第一个直接指向空

                    }
                    else
                    {
                        eventTree.Remove(id);
                    }
                }
                else
                {


                    while (tmp.next != null && tmp.next.data != node)
                    {
                        tmp = tmp.next;
                    }//直到找到 这个node 为止

                    if (tmp.next.next != null)
                    {
                        EventNode curNode = tmp.next;
                        tmp.next = curNode.next; //指向的删除

                        curNode.next = null;// 下一个删除
                       // tmp.next = tmp.next.next;
                    }
                    else
                    {
                        //
                        tmp.next = null;
                    }




                }



            }



        }

        //注册消息   1-->monobase-->monobase...
        //       2-->monobase-->monobase ...  这种结构

        protected void RegistMsg(ushort id, EventNode node)
        {

            if (!eventTree.ContainsKey(id))
            {
                eventTree.Add(id, node);
            }
            else
            {

                EventNode tmp = eventTree[id];


                while (tmp.next != null)
                {
                    tmp = tmp.next;
                }
                tmp.next = node;


            }

        }


        public void RegistMsg(MonoBase mono, params ushort[] msgs)
        {

           //  Debug.Log("Regist =="+mono.name);

            //ITools.Debugger ("  Regist");

          //  RegistGameObject(mono.name,mono.gameObject);

            for (int i = 0; i < msgs.Length; i++)
            {

                EventNode tmp = new EventNode(mono);
                RegistMsg(msgs[i], tmp);
            }


            //	ITools.Debugger ("  Regist Count =="+eventTree.Count.ToString());
        }



        public Dictionary<ushort, EventNode> eventTree = new Dictionary<ushort, EventNode>();


      

    }

}

