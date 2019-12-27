using Microsoft.Win32;
using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;

namespace Mem_Show
{
    public partial class FormMain : Form
    {
        // 引用
        [DllImport("kernel32")]
        private static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
        //变量
        private System.Threading.Timer timer;
        private MEMORY_INFO mInfo = new MEMORY_INFO();
        private readonly Rectangle RECT = new Rectangle(0, 0, 16, 16);
        private Pen pen = new Pen(Color.White, 1);
        private SolidBrush brush = new SolidBrush(Color.White);
        private Font font = new Font("微软雅黑", 8);
        private Bitmap bmp;
        private Graphics g;
        private string strCfgFile;
        private readonly string path = Application.ExecutablePath;
        private readonly string strRegName = "MemShow";
        private string memNum = "99";
        //private bool theme = true;
        //private bool startup = true;

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
            strCfgFile = ".\\" + Path.GetFileNameWithoutExtension(path) + ".cfg";
            bmp = new Bitmap(RECT.Width, RECT.Height);
            timer = new System.Threading.Timer(new TimerCallback(timer_Tcb), this, 200, 500);

            g = Graphics.FromImage(bmp);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.ResetClip();

            readFile(strCfgFile);

            notifyIconMS.Visible = true;
            this.ShowInTaskbar = false;
            this.Hide();
            
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
            writeFile(strCfgFile);
        }

        // 右键菜单 自启
        private void tsmiBoot_Click(object sender, EventArgs e)
        {
            if (tsmiBoot.Checked)
                setStartUp(false);
            else
                setStartUp(true);

            writeFile(strCfgFile);
        }

        // 右键菜单 退出
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            g = null;
            bmp = null;
            pen = null;
            brush = null;
            font = null;
            timer.Dispose();

            Application.Exit();
        }

        //线程时钟
        private void timer_Tcb(object state)
        {
            // 得到内存信息
            GlobalMemoryStatus(ref mInfo);
            memNum = mInfo.dwMemoryLoad.ToString();

            drawIcon1();
            Icon icon = Icon.FromHandle(bmp.GetHicon());

            notifyIconMS.Icon = icon;
            notifyIconMS.Text = "内存使用: " + memNum + "%";
        }

        // 创建配置文件
        private void creadeFile(String file)
        {
            // 是否存在
            if (!File.Exists(file))
            {
                using (File.Create(file))
                {
                }
                writeFile(file);
            }
        }

        // 读取配置文件
        private void readFile(String file)
        {
            creadeFile(file);

            // 读取
            using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
            {
                Color color = Color.White;

                while (!sr.EndOfStream)
                {
                    switch (sr.ReadLine())
                    {
                        case "Theme=True":
                            setTheme(true);
                            color = Color.White;
                            tsmiTheme.Checked = true;
                            break;
                        case "Theme=False":
                            color = Color.Black;
                            tsmiTheme.Checked = false;
                            break;
                        case "StartUp=True":
                            setStartUp(true);
                            break;
                        case "StartUp=False":
                            setStartUp(false);
                            break;
                    }

                    pen = new Pen(color, 1);
                    brush = new SolidBrush(color);
                }
            }
        }

        private void setTheme(bool b)
        {
            tsmiTheme.Checked = b;
            Color color;
            if (b)
                color = Color.White;
            else
                color = Color.Black;

            pen = new Pen(color, 1);
            brush = new SolidBrush(color);
        }

        // 写入配置文件
        private void writeFile(String file)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine("[Config]");
                sw.WriteLine("Theme=" + tsmiTheme.Checked.ToString());
                sw.WriteLine("StartUp=" + tsmiBoot.Checked.ToString());
            }
        }

        //设置开机启动,。
        private void setStartUp(bool b)
        {
            tsmiBoot.Checked = b;
            RegistryKey reg = Registry.CurrentUser;
            RegistryKey run = reg.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");

            if (b)
                run.SetValue(strRegName, path);
            else
                run.DeleteValue(strRegName, false);

            run.Close();
            reg.Close();
        }

        // 画图标
        private Icon drawIcon1()
        {
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.ResetClip();
            g.DrawRectangle(pen, 0, 0, 15, 15);
            g.DrawString(memNum, font, brush, 0, 1);

            return Icon.FromHandle(bmp.GetHicon());
        }
    }
}
