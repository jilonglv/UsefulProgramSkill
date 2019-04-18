# UsefulProgramSkill
以前项目中使用，以便再次使用和他人使用参考！

# Why this project?

之前也尝试使用过其他工具记录知识点，如OneDrive、iCloud、Youdao等，这些工具能存储，但是不能很好整理导航。github支持markdown就能很好的存储片段代码和导航资源的功能，也以便同大家分享。

# 在线常用工具

[google翻译](<https://translate.google.cn/>)		

[json在线编辑](<http://www.bejson.com/jsoneditoronline/>)	
[easyicon图标](https://www.easyicon.net/)	

#  开源项目	

[开源汇总](doc/projects.md)

## <a name="toc">目录</a> 

- [winform可编辑ListView](src/cs/EditableListView.cs) 使用 [例子](#EditListView)
- [C#执行cmd命令](#cmdCall) 封装 [ShellManager.cs](src/cs/ShellManager.cs)
- [开机启动项](#Run)
- [获取进程内存](#getMemorySize)
- [使用配置扩展](#microExt)
- [自动加载x86、x64（C++、C#）](##loadx64x86)
- [混合编程](##mixedprogram)

## <a id="#cmdCall">C#执行cmd命令</a>

```c#

ProcessStartInfo startInfo = new ProcessStartInfo(fileName)
{
    Arguments = arguments,
    WorkingDirectory = Path.GetDirectoryName(fileName),
    CreateNoWindow = true,
    UseShellExecute = false,
    RedirectStandardOutput = false,
    RedirectStandardError = false,
    Verb= "RunAs"//管理员身份时，设定该属性值
};

Process.Start(startInfo)
```

*注：以管理员身份调用时，宿主程序也必须已管理员身份运行中，否正调用会失败。*

常用cmd命令:

```bash
; Following line opens Control Panel > Display Properties > Settings:
rundll32.exe shell32.dll,Control_RunDLL desk.cpl,, 3
;Lock Windows
rundll32.exe user32.dll, LockWorkStation
;Shutdown Computer
shutdown /s /t 4
;Reboot Computer
shutdown /r /t 4
;Uninstall a Program
control.exe appwiz.cpl
;Network Connections
control.exe ncpa.cpl
;PowerShell
powershell.exe -ExecutionPolicy RemoteSigned -noexit -command "cd ""%path%"""
;Edit Hosts
notepad.exe "C:\Windows\System32\drivers\etc\hosts"
```
## <a id="#Run">开机启动项</a>
```bash
计算机\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
```
win10执行计划，win+r执行：

```bash
control schedtasks
```




## <a id="#EditListView">可编辑ListView</a>
新增SubItemClick事件:
```c#
listView.StartEditing(Editors[e.SubItem], e.Item, e.SubItem);
```
其中Editors需要是数组(每个列类型不同,拖动到ListView上,修改Visible为false).

## <a id="#getMemorySize">获取进程内存</a>

```c#
/// <summary>
/// 获取进程内存
/// </summary>
/// <returns></returns>
public static string GetCurProcessMem()
{
    var ps = Process.GetCurrentProcess();
    var pf1 = new PerformanceCounter("Process", "Working Set - Private", ps.ProcessName);   //第二个参数就是得到只有工作集

    return (pf1.NextValue() / 1024 / 1024).ToString(CultureInfo.InvariantCulture);
}
```

## <a id="#microExt">使用配置扩展</a>
Microsoft.Extensions.Configuration依赖：
- Microsoft.Extensions.Configuration.Abstractions.dll
- Microsoft.Extensions.Configuration.dll
- Microsoft.Extensions.Configuration.FileExtensions.dll
- Microsoft.Extensions.Configuration.Json.dll
- Microsoft.Extensions.FileProviders.Abstractions.dll
- Microsoft.Extensions.FileProviders.Physical.dll
- Microsoft.Extensions.FileSystemGlobbing.dll
- Microsoft.Extensions.Primitives.dll

使用：
```c#
 IConfigurationBuilder builder = new ConfigurationBuilder()
           .AddJsonFile("AppInfos.json", false);
var root = builder.Build();
foreach (IConfigurationSection item in root.GetChildren())
{
}
var r= root["root"];
```
skill：两种方式读取
```c#
var defaultcon = Configuration.GetConnectionString("DefaultConnection");
var devcon = Configuration["ConnectionStrings:DevConnection"];
```
json文件：

```json
{
  "Data": "LineZero",
  "ConnectionStrings": {
    "DefaultConnection": "数据库1",
    "DevConnection": "数据库2"
  }
}
```

## <a id="#loadx64x86">加载x64、x86</a>

### 加载C# x86 、x64 dll解决方案

如果有C#版本有x64和x86时，可以编译两个版本，把其中一个dll修改名称即可。
*场景：*有一个dll使用了pinvoke(即DllImport)调用api，可动态进行程序是x86还是x64进行动态加载；
*实践：*本人使用了ioc（autofac）进行加载，在加载失败时进行指定dll加载即可：

```C#

var currentDomain = AppDomain.CurrentDomain;
var location = Assembly.GetExecutingAssembly().Location;
var assemblyDir = Path.GetDirectoryName(location);
if (assemblyDir != null)
{
	currentDomain.AssemblyResolve += (sender, arg) =>
    {//autofac 加载64版本时，会触发解析失败，运行下面代码进行加载指定的dll
        //默认版本为(Assembly name).dll，64版本为(Assembly name)x64.dll
        if (arg.Name.StartsWith("(Assembly name)", StringComparison.OrdinalIgnoreCase))
        {
            string fileName = Path.Combine(assemblyDir, string.Format("(Assembly name){0}.dll", (IntPtr.Size == 4) ? "" : "x64"));
            return Assembly.LoadFile(fileName);
        }
        return null;
	};
}
```
### 加载C++ x86、x64 dll解决方案
按位数将dll放置不同的文件夹，使用api SetDllDirectory进行加载即可 [NativeLibraryHandle.cs](src\cs\NativeLibraryHandle.cs)

主函数LoadDlls加载指定的dll即可：

```c#

static void LoadDlls()
{
    NativeLibraryHandle.SetDllFloder();
    LoadDll("libuv.dll");
}
static void LoadDll(string filename)
{
    try
    {
        nativeLibraries.Add(new NativeLibraryHandle(filename));
    }
    catch (Exception ex)
    {
    }
}
```

[privateBinPath的使用](https://www.cnblogs.com/zhesong/p/pbpcf.html)

## <a id="#mixedprogram">混合编程</a>
```c#
//将8字节转为日期
byte[] val=new byte[8];
DateTime.FromBinary(BitConverter.ToInt64(val, 0));
```
[C#与C++类型互转](https://www.jianshu.com/p/d3ac316104f8)	

[p/invoke封送处理](<https://docs.microsoft.com/zh-cn/dotnet/framework/interop/default-marshaling-for-arrays?view=netframework-4.7.2>) 

[⬆︎返回目录](#toc)

## 参考
[Editing-of-ListView codeproject](https://www.codeproject.com/Articles/6646/In-place-Editing-of-ListView-subitems)
