using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisableGlobalShortcuts.Util;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        HotKeys hotKeys = new HotKeys();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //这里注册了Ctrl+Alt+E 快捷键
            hotKeys.Register(this.Handle, (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.D1, CallBack);
            MessageBox.Show("注册成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hotKeys.UnRegister(this.Handle);
            MessageBox.Show("卸载成功");
        }

        protected override void WndProc(ref Message m)
        {
            //窗口消息处理函数
            hotKeys.ProcessHotKey(m);
            base.WndProc(ref m);
        }

        //按下快捷键时被调用的方法
        public void CallBack()
        {
            MessageBox.Show("快捷键被调用！");
        }
    }
}
