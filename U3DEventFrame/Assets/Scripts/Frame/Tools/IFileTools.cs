using System;

using System.IO ;


using System.Net;




namespace U3DEventFrame
{
	public class IFileTools
	{

		#region  ByteOperator


		public static int ByteToInt(byte[] tmpBytes)
		{

			return BitConverter.ToInt32(tmpBytes,0);

		}

        //public static byte[] NetWorkToHostOrder(byte[] array ,int offset ,int length)
        //{
        //      IPAddress.NetworkToHostOrder
            

        //}



		#endregion


		#region  MemCpy

		public static void MemCpy(byte[] des, byte[] sou, int desStart)
		{
			if (des==null || sou==null)
			{
				return;
			}

			Buffer.BlockCopy(sou,0,des, desStart, sou.Length);


		}


        public static void MemCpy(byte[] des, int desStart, byte[] sou, int souStart,int length)
        {
            if (des == null || sou == null)
            {
                return;
            }

            Buffer.BlockCopy(sou, souStart, des, desStart, length);


        }



        public static void MemCpy(byte[] des, byte[] sou, int desStart,int length)
		{


			Buffer.BlockCopy(sou,0,des,desStart,length);
			//	        for (int a = 0; a < length; ++a)
			//	        {
			//	            des[start + a] = sou[a];
			//	        }
		}

		public static void MemCpy(byte[] des, byte[] sou, int desStart, int souStart, int length)
		{
			if(des.Length-desStart<length||sou.Length-souStart<length)
			{
				return;
			}

			Buffer.BlockCopy(sou,souStart,des,desStart,length);
			//	        for (int a = 0; a < length; ++a)
			//	        {
			//	            des[start1 + a] = sou[start2+a];
			//	        }
		}

        #endregion


        public static bool IsExistFile(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;

        }


		public static void CopyFolder(string strFromPath, string strToPath)
		{
			//如果源文件夹不存在，则创建
			if (!Directory.Exists(strFromPath))
			{
				Directory.CreateDirectory(strFromPath);
			}

			//如果目标文件夹中没有源文件夹则在目标文件夹中创建源文件夹
			if (!Directory.Exists(strToPath))
			{
				Directory.CreateDirectory(strToPath);



			}

			DirectoryInfo dir = new DirectoryInfo(strFromPath);

			FileSystemInfo[] filesInfo = dir.GetFileSystemInfos();


			for (int i = 0; i < filesInfo.Length; i++)
			{

				FileSystemInfo tmpFile = filesInfo[i];


				string tmpSoure = strFromPath + "\\" + tmpFile.Name;
				tmpSoure = IPathTools.FixedPath(tmpSoure);


				string tmpDist = strToPath + "\\" + tmpFile.Name;

				tmpDist = IPathTools.FixedPath(tmpDist);


				if (tmpFile is FileInfo)
				{




					File.Copy(tmpSoure, tmpDist, true);
				}
				else if (tmpFile is DirectoryInfo)
				{


					CopyFolder(tmpSoure, tmpDist);

				}
			}





		}


		//public static void DeleteFolder(string dir)
		//{
		//    foreach (string d in Directory.GetFileSystemEntries(dir))
		//    {
		//        if (File.Exists(d))
		//        {
		//            FileInfo fi = new FileInfo(d);
		//            if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
		//                fi.Attributes = FileAttributes.Normal;
		//            File.Delete(d);//直接删除其中的文件   
		//        }
		//        else
		//            DeleteFolder(d);//递归删除子文件夹   
		//    }
		//    Directory.Delete(dir);//删除已空文件夹   
		//}


		//private void DeleteDir(string path)
		//{
		//    if (path.Trim() == "" || !Directory.Exists(path))
		//        return;
		//    DirectoryInfo dirInfo = new DirectoryInfo(path);

		//    FileInfo[] fileInfos = dirInfo.GetFiles();
		//    if (fileInfos != null && fileInfos.Length > 0)
		//    {
		//        foreach (FileInfo fileInfo in fileInfos)
		//        {
		//            //DateTime.Compare( fileInfo.LastWriteTime,DateTime.Now);
		//            File.Delete(fileInfo.FullName); //删除文件
		//        }
		//    }

		//    DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
		//    if (dirInfos != null && dirInfos.Length > 0)
		//    {
		//        foreach (DirectoryInfo childDirInfo in dirInfos)
		//        {
		//            this.DeleteDir(childDirInfo.FullName); //递归
		//        }
		//    }
		//    Directory.Delete(dirInfo.FullName, true); //删除目录
		//}




		public static void DeleteFolder(string strfrompath)
		{
			//如果源文件夹不存在，则创建
			if (!Directory.Exists(strfrompath))
			{
				

				return;

			}




			DirectoryInfo dir = new DirectoryInfo(strfrompath);

			FileSystemInfo[] filesinfo = dir.GetFileSystemInfos();







			for (int i = 0; i < filesinfo.Length; i++)
			{

				FileSystemInfo tmpfile = filesinfo[i];




				if (tmpfile is FileInfo)
				{

					File.Delete(tmpfile.FullName);
				}
				else if (tmpfile is DirectoryInfo)
				{

					string tmpsoure = strfrompath + "\\" + tmpfile.Name;
					tmpsoure = IPathTools.FixedPath(tmpsoure);
					DeleteFolder(tmpsoure);

				}
			}

			// Debug.Log(filesinfo.Length);
			if (filesinfo.Length <= 0)
			{

				dir = null;

				// Debug.Log(strfrompath + "== is deleting");


				//   FileUtil.DeleteFileOrDirectory(strfrompath);
				//Directory.Delete();
				//dir.Delete(true);
				Directory.Delete(strfrompath, true);
			}



		}






	}
}

