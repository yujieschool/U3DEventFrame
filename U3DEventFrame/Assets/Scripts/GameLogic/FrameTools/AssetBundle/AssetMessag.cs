using U3DEventFrame;

public enum AssetEvent
{


    CheckLoadAssetsFinish = ManagerID.AssetManager + 1,

    CheckLoad,
    HunkRes,
    HunkMutiRes,

    HunkScences,


    ReleaseSingleObj, //释放单个object

    ReleaseBundleObjes,//释放一个bundle　包里　所有的object

    ReleaseScenceObjes,//　释放　单个场景中所有的 object

    ReleaseBundleAndObject,// 释放一个bundle 和包里的object

    ReleaseSingleBundle,//释放单个　assetbundle

    ReleaseScenceBundle,//释放　一个场景中的所有的assetbundle

    ReleaseAll,//　释放　一个场景中所有的 bundle 和 objects

    

    MaxValue

}

public enum LAssetEvent
{

    HunkRes = ManagerID.LAssetManager + 1,

    HunkMutiRes,

    HunkMutiBundleAndRes,
    ReleaseSingleObj,  //--释放单个object

    ReleaseBundleObjes, //--释放一个bundle包里所有的object

    ReleaseScenceObjes,// --　释放单个场景中所有的 object
    ReleaseBundleAndObjects,     //     --释放一个bundle 和包里的obj    同时释放多用这个
    ReleaseSingleBundle, //--释放单个assetbundle


    ReleaseScenceBundle, // --释放一个场景中的所有的assetbundle

    ReleaseAll,  //--　释放一个场景中所有的 bundle 和 objects

    MaxValue,
}
