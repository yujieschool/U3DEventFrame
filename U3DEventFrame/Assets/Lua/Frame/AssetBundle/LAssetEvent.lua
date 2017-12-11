--region *.lua
--Date
--此文件由[BabeLua]插件自动生成



--endregion
 local LAssetBegin = LManagerID.LAssetManager+1

LAssetEvent = {
    
    "HunkRes",
    "HunkMutiRes" ,
     "HunkMutiBundleAndRes" ,
    "ReleaseSingleObj",
    "ReleaseBundleObjes",
    "ReleaseScenceObjes",
    "ReleaseBundleAndObjects",
    "ReleaseSingleBundle",
    "ReleaseScenceBundle",
    "ReleaseAll" ,
    "MaxValue"  ,
    HunkRes =0,
    HunkMutiRes =0,

    HunkMutiBundleAndRes =0 ,
   
    ReleaseSingleObj  =0, --释放单个object

    ReleaseBundleObjes=0, --释放一个bundle　包里　所有的object

    ReleaseScenceObjes=0, --　释放　单个场景中所有的 object
    ReleaseBundleAndObjects=0 ,          --释放一个bundle 和包里的obj    同时释放多用这个
    ReleaseSingleBundle=0, --释放单个　assetbundle
  
    ReleaseScenceBundle=0,  --释放　一个场景中的所有的assetbundle

    ReleaseAll=0,  --　释放　一个场景中所有的 bundle 和 objects

    MaxValue = 0,
}

ResetTableKeyValue(LAssetBegin, LAssetEvent);








