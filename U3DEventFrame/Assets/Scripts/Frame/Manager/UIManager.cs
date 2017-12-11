using UnityEngine;
using System.Collections;

using System.Collections.Generic ;






namespace U3DEventFrame
{

    public class UIManager : ManagerBase
    {

        // Use this for initialization
        void Awake()
        {

            instance = this;

            this.InitialBackMsg();
        }

        /// <summary>
        /// 可以让消息 多少秒内有回调 消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="backId"></param>
        public void SendMsg(MsgBase msg,int  backId =-1)
        {

           
              this.ProcessSendBackMsg(backId);

            if (msg.GetManager() == (int)ManagerID.UIManager)
            {


                // 处理 UI 模块的消息

                ProcessEvent(msg);
            }
            else
            {
                // sever 
                MsgCenter.instance.SendToMsg(msg);

            }

        }
        //private Queue<MsgBase>    eventQueue ;
       public static UIManager instance;

        #region  GameObject

        //  * private MsgBody           recvMsg ;
        //first  string  is  panel name  sencond string  is sonchild
        public Dictionary<string, Dictionary<string, GameObject>> SonMembers
        {
            get
            {
                if (sonMembers == null)
                    sonMembers = new Dictionary<string, Dictionary<string, GameObject>>();

                return sonMembers;
            }

        }

        private Dictionary<string, Dictionary<string, GameObject>> sonMembers = null;



        public void RegistGameObject(string panleName,string objName, GameObject tmpObj)
        {
            if (SonMembers.ContainsKey(panleName))
            {

                if (!SonMembers[panleName].ContainsKey(objName))
                {
                    SonMembers[panleName].Add(objName, tmpObj);
                }

            }
            else
            {
                Dictionary<string, GameObject> tmpPanel = new Dictionary<string, GameObject>();

                tmpPanel.Add(objName, tmpObj);
                SonMembers.Add(panleName, tmpPanel);
            }
        }


        public void UnRegistGameObject(string panelName,string objName)
        {

            if (SonMembers.ContainsKey(panelName) )
            {

                if (SonMembers[panelName].ContainsKey(objName))
                {
                    SonMembers[panelName].Remove(objName);
                }

            }

        }
        public void UnRegistPanelGameObject(string panelName)
        {

            if (SonMembers.ContainsKey(panelName))
            {

                SonMembers[panelName].Clear();

                SonMembers.Remove(panelName);

            }

        }


        public GameObject GetGameObject(string panelName,string objName)
        {
            if (SonMembers.ContainsKey(panelName))
            {
                if (sonMembers[panelName].ContainsKey(objName))
                    return sonMembers[panelName][objName];

            }
            return null;
            
        }

        #endregion
    }

}







