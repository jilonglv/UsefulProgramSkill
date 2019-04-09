# UsefulProgramSkill
以前项目中使用，以便再次使用和他人使用参考！

## <a name="toc"></a> 目录

- [winform可编辑ListView](src/cs/EditableListView.cs) 使用 [例子](#EditListView)
- [C#执行cmd命令](src/cs/ShellManager.cs)
- [获取进程内存](#getMemorySize)
- [使用配置扩展](#microExt)



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


[⬆︎返回目录](#toc)

## 参考
[Editing-of-ListView codeproject](https://www.codeproject.com/Articles/6646/In-place-Editing-of-ListView-subitems)
