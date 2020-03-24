using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DisableGlobalShortcuts.Util;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        HotKeys h = new HotKeys();
        public MainWindow()
        {
            InitializeComponent();
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
            h.ProcessHotKey(msg, wParam);
            return hwnd;
        }
        private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            //这里注册了Ctrl+Alt+E 快捷键
            h.Register(new WindowInteropHelper(this).Handle, (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.D1, CallBack);
            MessageBox.Show("注册成功");
        }

        private void CancelRegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            h.UnRegister(new WindowInteropHelper(this).Handle);
            MessageBox.Show("卸载成功");
        }

        //按下快捷键时被调用的方法
        public void CallBack()
        {
            MessageBox.Show("快捷键被调用！");
        }
    }

}
