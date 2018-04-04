using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolDataBase<T> where T :new ()
{

    private T data;

    bool isUsing;


    public PoolDataBase(T  tmpData)
    {
        data = tmpData;
        isUsing = false;
    }

    public PoolDataBase()
    {
        data =  default(T);

        if (data == null)
        {
            data = new T();
        }
        isUsing = false;
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

    public bool IsUsing
    {
        get
        {
            return isUsing;
        }
        set
        {
            isUsing = value;
        }
    }
 
}

public class ObjectPoolManager<T>  where T: new ()
{


    private static ObjectPoolManager<T> instance =null;

    public static ObjectPoolManager<T> Instance
    {
        get
        {

            if (instance == null)
            {
                instance = new ObjectPoolManager<T>();
            }

            return instance;

        }
    }


    List<PoolDataBase<T>> objectManager;



    public ObjectPoolManager()
    {
        objectManager = new List<PoolDataBase<T>>();
    }


    public void Destroy()
    {
        objectManager.Clear();

        objectManager = null;

        instance = null;
    }

     ~ObjectPoolManager()
    {
        Destroy();

    }

    public T GetFreeObject()
    {
        for (int i = 0; i < objectManager.Count; i++)
        {
            if (!objectManager[i].IsUsing)
            {
                objectManager[i].IsUsing = true;
                 return  objectManager[i].Data ;
            }
            
        }

        PoolDataBase<T> tmpData = new PoolDataBase<T>();

        tmpData.IsUsing = true;
        objectManager.Add(tmpData);


        return tmpData.Data;
       
    }


    public void ReleaseObject( T  data )
    {
        for (int i = 0; i < objectManager.Count; i++)
        {
            if (data.Equals( objectManager[i].Data))
            {

                objectManager[i].IsUsing = false;
                break;
            }

        }

          ReduceObject();
 
    }

    public void ReduceObject()
    {

        ushort tmpCount = 0;
        for (int i = 0; i < objectManager.Count; i++)
        {
            if (!objectManager[i].IsUsing)
            {
                tmpCount++;

                if (tmpCount > 2)
                {
                    objectManager.Remove(objectManager[i]);
                }
            }
        }

    }


   



}


