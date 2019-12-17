using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mem_Show
{
    public partial class FormMain : Form
    {
        // 引用
        [DllImport("kernel32")]
        private static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
        //变量
        MEMORY_INFO mInfo = new MEMORY_INFO();
        readonly Rectangle RECT = new Rectangle(0, 0, 16, 16);

        Pen pen = new Pen(Color.White, 1);
        SolidBrush brush = new SolidBrush(Color.White);
        Font font = new Font("微软雅黑", 8);
        Bitmap bmp;
        Graphics g;
        readonly string strAppName = "MemShow";
        string memNum = "99";

        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }

        public FormMain()
        {
            InitializeComponent();
        }

        // APP 初始化 & 加载
        private void formMain_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(RECT.Width, RECT.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.ResetClip();

            if (isAutoBoot())
                tsmiBoot.Checked = true;
            else
                tsmiBoot.Checked = false;

            this.ShowInTaskbar = false;
            notifyIconMS.Visible = true;
        }

        // 双击 弹出关于
        private void notifyIconMS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(
                "本作品由：人贱人爱花贱花开的三个臭皮匠，联合制作！",
                "关于 内存显示 v1.3.2020.1",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // 右键菜单 主题
        private void tsmiTheme_Click(object sender, EventArgs e)
        {
            Color color;

            if (tsmiTheme.Checked)
            {
                color = Color.Black;
                tsmiTheme.Checked = false;
            }
            else
            {
                color = Color.White;
                tsmiTheme.Checked = true;
            }
            pen = new Pen(color, 1);
            brush = new SolidBrush(color);
        }

        // 右键菜单 自启
        private void tsmiBoot_Click(object sender, EventArgs e)
        {
            if (tsmiBoot.Checked)
            {
                setAutoBoot(true);
                tsmiBoot.Checked = false;
            }
            else
            {
                setAutoBoot(false);
                tsmiBoot.Checked = true;
            }
        }

        // 右键菜单 退出
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            pen = null;
            brush = null;
            font = null;
            bmp = null;
            g = null;

            Application.Exit();
        }

        // 主时钟
        private void timerMS_Tick(object sender, EventArgs e)
        {
            getMemInfo();
            drawBmp();

            Icon icon = Icon.FromHandle(bmp.GetHicon());

            notifyIconMS.Icon = icon;
            notifyIconMS.Text = "内存使用: " + memNum + "%";
        }

        // 检测是否为开机启动
        private bool isAutoBoot()
        {
            try
            {
                string path = Application.ExecutablePath;
                RegistryKey reg = Registry.CurrentUser;
                RegistryKey run = reg.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                object key = run.GetValue(strAppName);
                run.Close();
                reg.Close();

                if (null == key || !path.Equals(key.ToString()))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        //设置或取消开机启动,isAutoBoot=true为开机启动。
        private void setAutoBoot(bool isAutoBoot)
        {
            string path = Application.ExecutablePath;
            RegistryKey reg = Registry.CurrentUser;
            RegistryKey run = reg.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");

            if (isAutoBoot)
                run.DeleteValue(strAppName, false);
            else
                run.SetValue(strAppName, path);

            run.Close();
            reg.Close();
        }

        // 得到内存信息
        private void getMemInfo()
        {
            GlobalMemoryStatus(ref mInfo);
            memNum = mInfo.dwMemoryLoad.ToString();
        }

        // 画图
        private void drawBmp()
        {
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.ResetClip();
            g.DrawRectangle(pen, 0, 0, 15, 15);
            g.DrawString(memNum, font, brush, 0, 0);
        }
    }
}
