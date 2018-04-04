using UnityEngine ;

using U3DEventFrame;



	public class Debuger
	{

        static public int sendPort = 18009;

		static public bool EnableLog = true;

        static public bool EnableConsolLog = false;
    static public bool EnableClient = false;


    static public bool EnableError = false;

    static public bool EnableWarning = false;


    static public AsyncUdpClient client =null;


    static public string[] TestAarray(int  tmpLenth)
    {

        return  new string[tmpLenth] ;
 

    }


		static public void Log(object message)
		{
			Log(message,null);
		}
		static public void Log(object message, Object context)
		{
			if(EnableLog)
			{
                if (Application.platform == RuntimePlatform.WindowsEditor ||
                   Application.platform == RuntimePlatform.OSXEditor)
                {

                  //  Debug.Log(message, context);

                 Debug.Log(message, context);
                //  Debug.Log(message, context);

                }
                else if (EnableConsolLog)
                {

                   Debug.LogError(message, context);
                }



            if (EnableClient)
            {
                if (client == null)
                {
                    client = new AsyncUdpClient();

                }

                client.BroadCastMsg(sendPort, System.Text.Encoding.UTF8.GetBytes(message.ToString()));


            }


        }
		}

        static public void UdpLog(object message)
        {
            if (client == null)
            {
                client = new AsyncUdpClient();

            }



            client.BroadCastMsg(sendPort, System.Text.Encoding.UTF8.GetBytes(message.ToString()));
            client.BroadCastMsg(sendPort+1, System.Text.Encoding.UTF8.GetBytes(message.ToString()));
            client.BroadCastMsg(sendPort+2, System.Text.Encoding.UTF8.GetBytes(message.ToString()));
        }


        static public void LogError(object message)
		{
			LogError(message,null);
		}
		static public void LogError(object message, Object context)
		{
			if(EnableLog||EnableError)
			{

                if (Application.platform == RuntimePlatform.WindowsEditor ||
                        Application.platform == RuntimePlatform.OSXEditor)
                {

                   Debug.LogError(message, context);
               }
                else if (EnableConsolLog)
                {

                    Debug.LogError(message, context);
                }




            if (EnableClient)
            {

                if (client == null)
                {
                    client = new AsyncUdpClient();

                }


                client.BroadCastMsg(sendPort + 2, System.Text.Encoding.UTF8.GetBytes(message.ToString()));

            }



          }



   


		}
		static public void LogWarning(object message)
		{
			LogWarning(message,null);
		}
		static public void LogWarning(object message, Object context)
		{
			if(EnableLog||EnableWarning)
			{

                if (Application.platform == RuntimePlatform.WindowsEditor ||
                          Application.platform == RuntimePlatform.OSXEditor)
                {

                Debug.LogWarning(message, context);
                }
                else if (EnableConsolLog)
                {

                    Debug.LogError(message, context);
                }



            if (EnableClient)
            {
                if (client == null)
                {
                    client = new AsyncUdpClient();

                }




                client.BroadCastMsg(sendPort + 1, System.Text.Encoding.UTF8.GetBytes(message.ToString()));

            }






        }
		}

	}


