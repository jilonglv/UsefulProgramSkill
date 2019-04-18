using System;
using System.Runtime.InteropServices;
public class NativeLibraryHandle : SafeHandle
{
    [System.Flags]
    public enum LoadLibraryFlags : uint
    {
        None = 0,
        DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
        LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
        LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
        LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
    }
    static string DllFloder = Environment.Is64BitProcess ? "x64" : "x86";
    public NativeLibraryHandle(string filename, LoadLibraryFlags flags= LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH) : base(IntPtr.Zero, true)
    {
        // LOAD_WITH_ALTERED_SEARCH_PATH
        base.SetHandle(LoadLibraryEx(filename, IntPtr.Zero, flags));
    }

    public override bool IsInvalid
    {
        get { return this.handle == IntPtr.Zero; }
    }

    protected override bool ReleaseHandle()
    {
        if (IsInvalid)
            return FreeLibrary(this.handle);
        return true;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool FreeLibrary(IntPtr hModule);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool SetDllDirectory(string lpPathName);
    public static void SetDllFloder()
    {
        SetDllDirectory(DllFloder);
    }
}
