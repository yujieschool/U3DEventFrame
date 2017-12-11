

using UnityEngine;
using System.Collections;

using System.Threading ;

using System.Collections.Generic  ;








namespace U3DEventFrame
{


	public enum ManagerID
	{
        LuaManager   = 0,

		LNetManger = FrameTools.MsgSpan * 1,
		LUIManager = FrameTools.MsgSpan*2,
		LNPCManager = FrameTools.MsgSpan *3,
		LCharatorManager = FrameTools.MsgSpan * 4,
		LAssetManager = FrameTools.MsgSpan * 5,
		LGameManager  = FrameTools.MsgSpan * 6,
		LDataManager = FrameTools.MsgSpan * 7,
		LAudioManager  = FrameTools.MsgSpan*8,
        LDBManager = FrameTools.MsgSpan * 9,

		
		NetManager = FrameTools.MsgSpan * 12,
        UIManager = FrameTools.MsgSpan*13,
        NPCManager = FrameTools.MsgSpan * 14,
        CharatorManager = FrameTools.MsgSpan * 15,
        AssetManager = FrameTools.MsgSpan * 16,
        GameManager  = FrameTools.MsgSpan * 17,
		DataManager = FrameTools.MsgSpan * 18,
        AudioManager  = FrameTools.MsgSpan*19,
        DBManager = FrameTools.MsgSpan * 20

		
	}








    public class MsgCenter : MonoBehaviour
    {


        // 消息分类 不要超过 此间隔
       // public const int MsgSpan = 3000;
        // [RuntimeInitializeOnLoadMethod]
        // Use this for initialization
        void Awake()
        {
           
          //  Application.targetFrameRate = 60;
            instance = this;
            gameObject.AddComponent<InputManager>();
#if USE_MutiMSGQueue
            msgQueue = new Queue<MsgBase>();
#endif
            gameObject.AddComponent<GameManager>();
            gameObject.AddComponent<UIManager>();

            gameObject.AddComponent<AudioManager>();


            gameObject.AddComponent<CharaterManager>();



            gameObject.AddComponent<NPCManager>();
          

            gameObject.AddComponent<AssetManager>();
			gameObject.AddComponent<LuaEventProcess>();

			gameObject.AddComponent<NetManager>();
			

        }







        void Update()
        {


#if  USE_MutiMSGQueue
        while (msgQueue.Count > 0)
        {




            MsgBase tmpBody = msgQueue.Dequeue();

            AnasysisMsg(tmpBody);

        }
#endif
        }



        //	public void RecvThread()
        //	{
        //		
        //		while (recvIsRunning)
        //		{
        //			lock (this)
        //			{
        //				while(msgQueue.Count > 0)
        //				{
        //
        //
        //		
        //					MsgBody tmpBody = msgQueue.Dequeue();
        //					
        //					AnasysisMsg(tmpBody);
        //					
        //
        //					
        //					
        //				}
        //				
        //			}
        //			
        //			Thread.Sleep(15);
        //			
        //		}
        //	}
        //	

        public void SendToMsg(MsgBase tmpBody)
        {




#if USE_MutiMSGQueue
        if (tmpBody != null)
        {


            //ITools.Debugger("msgcenter");
            lock (this)
            {

                msgQueue.Enqueue(tmpBody);

            }
        }
#else

            AnasysisMsg(tmpBody);
#endif


        }

         

        private void AnasysisMsg(MsgBase tmpBody)
        {
            if (tmpBody == null)
                return;


            //0  3000   6000
            int  tmpid = tmpBody.GetManager();
//			Debug.Log("AnasysisMsg  ============="+ tmpBody.msgId);

            if (tmpid < (int)ManagerID.NetManager)
            {
                //lua
               // Debuger.Log("sendmsg  to lua =="+tmpBody.msgId);
                LuaEventProcess.instance.ProcessEvent(tmpBody);
            }
            else
            {
                switch (tmpid)
                {
                        //16*3000 +2999
                    case  (ushort)ManagerID.AssetManager:

                        //  转发给  asset 模块 
                        AssetManager.instance.ProcessEvent(tmpBody);
                        break;

                    case (ushort)ManagerID.AudioManager:

                        //  ITools.Debugger("audio2");
                        AudioManager.instance.ProcessEvent(tmpBody);
                        break;

                    case (ushort)ManagerID.CharatorManager:

                        //Debug.Log("charactor =="+tmpBody.msgId);
                        CharaterManager.instance.ProcessEvent(tmpBody);
                        break;

                    case (ushort)ManagerID.DataManager:
                       

                        break;

                    case (ushort)ManagerID.GameManager:

                        GameManager.instance.ProcessEvent(tmpBody);
                        break;

                    case (ushort)ManagerID.NetManager:

                        NetManager.instance.ProcessEvent(tmpBody);
                        break;


                    case (ushort)ManagerID.NPCManager:
                        NPCManager.instance.ProcessEvent(tmpBody);
                        break;

                    case (ushort)ManagerID.UIManager:


                        //    Debug.Log("ui is coming");
                        UIManager.instance.ProcessEvent(tmpBody);
                        break;

                    //case ManagerID.LuaManager:


                    //    //    Debug.Log("ui is coming");
                    //    LuaEventProcess.instance.ProcessEvent(tmpBody);
                    //    break;

                    default:
                        break;


                }
            }


 



        }








		#if  USE_MutiMSGQueue
        private Queue<MsgBase> msgQueue;
#endif

        public static MsgCenter instance;



        //private	Thread recThread;
        //	private ArrayList msgArray;
        //	bool recvIsRunning = false;



    }

}





