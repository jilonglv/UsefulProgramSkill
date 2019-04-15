# github开源

## <a name="toc">目录</a> 

- [C# hotkey](#hotkey) 应用[ListaryWithWinKey](<https://github.com/KevinWang15/ListaryWithWinKey>)	
- [listary C#版](#listaryDesktop) 
- [TreeListView](https://github.com/TangHanF/CSharp_TreeListView) [、TreeView](https://github.com/Feofilakt/ControlTreeViewLibrary)	
- [MultiLevelProgressBar](<https://github.com/Hiwen/MultiLevelProgressBar>) [、带文本ProgressBar](<https://github.com/taancer/aProgressBar>)

## <a id="#hotkey">C# hotkey</a>

```c#
 // keydown "Ctrl+Shift+Alt+Win+F" then keyup!
(new Thread(() =>
{
    keybd_event((int)System.Windows.Forms.Keys.LControlKey, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
    keybd_event((int)System.Windows.Forms.Keys.LShiftKey, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
    keybd_event((int)System.Windows.Forms.Keys.LMenu, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
    keybd_event((int)System.Windows.Forms.Keys.LWin, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
    keybd_event((int)System.Windows.Forms.Keys.F, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
    keybd_event((int)System.Windows.Forms.Keys.F, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
    keybd_event((int)System.Windows.Forms.Keys.LWin, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
    keybd_event((int)System.Windows.Forms.Keys.LMenu, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
    keybd_event((int)System.Windows.Forms.Keys.LShiftKey, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
    keybd_event((int)System.Windows.Forms.Keys.LControlKey, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
})).Start();
```

## <a id="#listaryDesktop">listaryDesktop</a>
***C#版listary;使用autohotkey组件实现快速启动、打开等功能*** 

更多配置调用规则 参考在线文档[AutoHotkey](https://lexikos.github.io/v2/docs/AutoHotkey.htm)

- 已实现快捷键功能

- 关键字功能



参考安装钩子[C#安装键盘钩子](<http://www.csframework.com/archive/2/arc-2-20110617-1636.htm>)	

钩子应用 [C#封装hook](<https://jingyan.baidu.com/article/ceb9fb10ebd9f68cad2ba03c.html>) 


# 参考

[AutoHotkey](<https://github.com/AutoHotkey/AutoHotkey>)	

