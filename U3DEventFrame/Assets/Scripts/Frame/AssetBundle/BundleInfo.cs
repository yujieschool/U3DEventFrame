using UnityEngine;

using System.Collections;



namespace U3DEventFrame
{

    /// <summary>
    ///  一个res 请求
    /// </summary>
    public class BundleInfo
    {
        public string resName;

        public string bundleName;

        public bool isSingle;

        public Object[] resObj;

        public string scenceName;

        //AB 的全称 like   test.ld
        public string fullBundleName;



        public string ScenceName
        {
            get
            {

                return scenceName;
            }
            set
            {

                scenceName = value;
            }
        }




        public string BundleName
        {
            get
            {

                return bundleName;
            }
            set
            {

                bundleName = value;
            }
        }



        public string FullBundleName
        {
            get
            {

                return fullBundleName;
            }
            set
            {

                fullBundleName = value;
            }
        }




        public delegate void BundleLoadFinish(BundleInfo tmpBundle);


        BundleLoadFinish LoadFinish;

        public BundleInfo(bool signle, string scence, string bundle, string res, BundleLoadFinish callBack)
        {

            this.scenceName = scence;



            this.isSingle = signle;

            this.resName = res;

            this.bundleName = bundle;


            LoadFinish = callBack;

        }


        public void AddReses(params Object[] objs)
        {

           // resObj = objs;


            // Debuger.Log(" ADDD OBJ  bundleName ==" + bundleName);


           


           resObj = new Object[objs.Length];

       //     resObj = objs;
           objs.CopyTo(resObj,0);

         

          //  if (resObj[0].GetType() == typeof(GameObject))

          //  Debuger.Log(" ADDD  OBJ resName ==" + resObj[0].name);
        }

        public void ReleaseObj()
        {
          //  Debug.Log("begin  release ");
            LoadFinish(this);

           // Debug.Log("release finish ");
           // resObj = null;
        }


        public void Dispose()
        {


         //   Debuger.Log("Dis pose bundleName ==" + bundleName);


          //  Debuger.Log("Dis pose resName ==" + resName);

            fullBundleName = null;
            scenceName = null;

            bundleName = null;

            resName = null;
            //已经缓存下来了 不用释放
            resObj = null;



        }




    }


}

