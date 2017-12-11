

#define  AssetUseLua



public class GloableConfig
{

    //是否是网络版
    public static bool IsNetPlayer = false;

    //socket  IP  port

    public static string IpAddr = "192.168.0.64";

    public static ushort Port = 20050;

    //记录streamasset下的全部 md5  和版本号
    public static string LuaFileListName = "File.txt";

    //版本号
    public static string VersionName   ="1.0.7";

    //版本号
    public static ushort  VersionId = 7;

    // 公司名称
    public static string CompanyName = "JFYD";

    //应用名

    public static string ProductName = "3DFish";

	public static string GameDataPath = "/ExcelText/GameData.db";
	public static string PlayerDataPath = "/ExcelText/PlayerData.db";

    //打开背景音乐
    public static bool IsOpenBackGroundAudio = true;

    //打开音效
    public static bool IsOpenAudio = true;

	//跳过行.
	public static string MarkSkipLine = "//";
	
	//跳过列.
	public static string MarkSkipCell = "&&";
	
	//单元格标记.
	public static char MarkDivideCell = '|';
	
	//行标记.
	public static char MarkDivideLine = '\n';
	
	//工作簿标记.
	public static char MarkDivideSheet = '#';
	
	//数组标记.
	public static char MarkDivideArray = ';';




}
