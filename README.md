# UsefulProgramSkill
以前项目中使用，以便再次使用和他人使用参考！

## <a name="toc"></a> 目录

- [winform可编辑ListView](src/cs/EditListView)
- [片段](#snippet)



<a id="#snippet">获取进程内存</a>
```
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

[⬆︎返回目录](#toc)

## 参考
[Editing-of-ListView codeproject](https://www.codeproject.com/Articles/6646/In-place-Editing-of-ListView-subitems)
