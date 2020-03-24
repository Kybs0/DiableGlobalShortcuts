using DisableGlobalShortcuts.Util;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DiableGlobalShortcuts_Background
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
        //1 无感知禁用全局快捷键
    HotKeys hotKeys = new HotKeys();
    hotKeys.Register((int)HotkeyModifiers.Control, Keys.N);
    hotKeys.Register((int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.D1);
    hotKeys.Register((int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.D2);
    hotKeys.Register((int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.D3);
    hotKeys.Register((int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.D4);
            //2 无感知禁用全局快捷键后回调
            //var hotKeyHandleWindow = new HotKeyHandleWindow();
            //hotKeyHandleWindow.Show();
            //hotKeyHandleWindow.Hide();
        }

    }

    public class HotKeyHandleWindow : Window
    {
        private readonly HotKeys _hotKeys = new HotKeys();
        public HotKeyHandleWindow()
        {
            WindowStyle = WindowStyle.None;
            Width = 0;
            Height = 0;
            Loaded += (s, e) =>
            {
                //这里注册了Ctrl+Alt+1 快捷键
                _hotKeys.Register(new WindowInteropHelper(this).Handle,
                    (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.D1, CallBack);
            };
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            hwndSource?.AddHook(new HwndSourceHook(WndProc));
        }
        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //窗口消息处理函数
            _hotKeys.ProcessHotKey(msg, wParam);
            return hwnd;
        }
        //按下快捷键时被调用的方法
        public void CallBack()
        {
        }
    }
}
