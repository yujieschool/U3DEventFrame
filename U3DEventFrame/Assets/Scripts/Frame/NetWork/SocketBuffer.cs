using UnityEngine;
using System.Collections;


namespace U3DEventFrame
{


    public class SocketBuffer
    {


        public delegate void CallBackRecvOver(byte[] allData);



        CallBackRecvOver callBackRecvOver;

        public SocketBuffer(byte headLength, CallBackRecvOver recvOver)
        {

            callBackRecvOver = recvOver;
            headByte = new byte[headLength];

            this.headLength = headLength;

        }

        private byte[]  headByte ;

        private byte headLength =7;

        private int  curRecLength = 0;//当前接收长度
        private int   allDataLengh = 0; //这类数据总长度
        private byte[] allRecData;//数据拼接用

        public void RecevByte(byte[] recByte,int reallength)
        {

            if (reallength == 0)
                return;


            if (curRecLength < headByte.Length)
            {
                RecvHead(recByte, reallength);
            }
            else
            {

                //头接收完了
            

                    //if (curRecLength == headByte.Length)
                    //{

                    //    allDataLengh = IFileTools.ByteToInt(headByte) + headLength;

                    //    Debug.Log("recv data lenght  socket Buffer ==" + allDataLengh);

                    //    allRecData = new byte[allDataLengh];

                    //    IFileTools.MemCpy(allRecData, headByte, 0, curRecLength);
                    //}



                    int tmpLength = curRecLength + reallength;//已经接收到　加上　现在接收的
                    if (tmpLength == allDataLengh)
                    {

                //       Debug.Log(" all finish");
                        RecvOneAll(recByte, reallength);
                    }
                    else if (tmpLength > allDataLengh) //接收到的比要接的大
                    {

                  //  Debug.Log("larger   body");
                    RecvLarger(recByte, reallength);
                    }
                    else  //实际接收到的　比要的小
                    {

                //    Debug.Log(" all  small ");
                    RecvSmall(recByte, reallength);
                    }
                
            }






        }

        private void RecvHead(byte[] recByte, int reallength)
        {
           // if (curRecLength < headByte.Length) //小于头长度
        //    {
                int tmpReal = headByte.Length - curRecLength;


                int tmpLength = curRecLength + reallength;

                if (tmpLength < headByte.Length) //比头小
                {
                  //  Debug.Log("small  head");
                    IFileTools.MemCpy(headByte, recByte, curRecLength, reallength);
                    curRecLength += tmpReal;
                }
                else 
                {
              //  Debug.Log("big head");

                    IFileTools.MemCpy(headByte, recByte, curRecLength, tmpReal);
                    curRecLength += tmpReal;



                    allDataLengh = IFileTools.ByteToInt(headByte) + headLength;
                    allRecData = new byte[allDataLengh];

               
                    //头考入
                    IFileTools.MemCpy(allRecData, 0, headByte, 0, headByte.Length);


                    // 剩下的　在放入
                    int tmpRemain = reallength - tmpReal;
                if (tmpRemain > 0)
                {
                    byte[] tmpBytes = new byte[tmpRemain];

                    IFileTools.MemCpy(tmpBytes, 0, recByte, tmpReal, tmpRemain);


                    RecevByte(tmpBytes, tmpRemain);
                }
                else
                {
                    RecvOneMsgOver();
                }


                }

       //     }
            //else//　大于等于头长度
            //{
            //    //IFileTools.MemCpy(headByte, recByte, 0, headByte.Length);
            //    //curRecLength += headByte.Length;



            //    //allDataLengh = IFileTools.ByteToInt(headByte) + headLength;

            //    //Debug.Log("Big   allDataLengh =="+ allDataLengh);
            //    //allRecData = new byte[allDataLengh];

              

            //    ////头考入
            //    //IFileTools.MemCpy(allRecData, 0, headByte, 0, headByte.Length);


          
            //    //// 剩下的　在放入
            //    //int tmpRemain = reallength -  headByte.Length;
            //    //if (tmpRemain > 0)
            //    //{
            //    //    byte[] tmpBytes = new byte[tmpRemain];
            //    //    IFileTools.MemCpy(tmpBytes, 0, recByte, headByte.Length, tmpRemain);
            //    //    RecevByte(tmpBytes, tmpRemain);
            //    //}



            //}



        }

        private void RecvSmall(byte[] recvByte, int realLength)
        {
            IFileTools.MemCpy(allRecData, recvByte, curRecLength, realLength);
            curRecLength += realLength;



        }



        private void RecvLarger(byte[] recvByte, int realLength)
        {

            int tmpLength = allDataLengh - curRecLength;
            IFileTools.MemCpy(allRecData, recvByte, curRecLength, tmpLength);
            curRecLength += tmpLength;
            RecvOneMsgOver();

            int remainLenth = realLength - tmpLength;

            byte[] remainByte = new byte[remainLenth];

         //   Debug.Log("recvByte larger =="+ recvByte.Length);

            //Debug.Log("recvByte larger tmpLength ==" + tmpLength);


            //Debug.Log("recvByte larger tmpLength 2222==" + remainLenth);


            //Debug.Log("remainByte  444444==" + remainByte.Length);

            IFileTools.MemCpy(remainByte,0,recvByte,tmpLength,remainLenth);

            RecevByte(remainByte,remainLenth);


        }

        private void  RecvOneAll(byte[] recvByte ,int realLength)
        {
            IFileTools.MemCpy(allRecData, recvByte, curRecLength, realLength);
            curRecLength += realLength;

            RecvOneMsgOver();
        }


        private void RecvOneMsgOver()
        {

           // Debug.Log("RecvOneMsgOver over");

            if (callBackRecvOver != null)
            {
                callBackRecvOver(allRecData);
            }
            curRecLength = 0;
            allDataLengh = 0;
            allRecData = null;
        }

        public byte[] GetOneMsg()
        {
            return allRecData;
        }

    }





}

