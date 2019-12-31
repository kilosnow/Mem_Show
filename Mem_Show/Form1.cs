using Microsoft.Win32;
using System;
using System.IO;
using System.Drawing;
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
        private static System.Threading.Timer timer;
        private MEMORY_INFO mInfo = new MEMORY_INFO();
        private Pen pen = new Pen(Color.White, 1);
        private SolidBrush brush = new SolidBrush(Color.White);
        private readonly Font font = new Font("微软雅黑", 8);

        private string strCfgFile;
        //private readonly Rectangle RECT = new Rectangle(0, 0, 16, 16);
        private readonly string path = Application.ExecutablePath;
        private readonly string strRegName = "内存显示";
        private readonly string strRegRun = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private string memNum = "99";


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
            timer = new System.Threading.Timer(timer_Callback, null, 100, 1000);

            readFile(strCfgFile);

            notifyIconMS.Visible = true;
            this.ShowInTaskbar = false;
            this.Visible = false;
        }



        // 双击 弹出关于
        private void notifyIconMS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(
                "本作品由：人贱人爱花贱花开的三个臭皮匠，联合制作！",
                "关于 内存显示 v1.5.2020.3",
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
            setStartUp(!tsmiBoot.Checked);
        }

        // 右键菜单 退出
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            pen.Dispose();
            brush.Dispose();
            font.Dispose();
            timer.Dispose();

            Application.Exit();
        }

        // 时钟
        private void timer_Callback(object state)
        {
            // 得到内存信息
            GlobalMemoryStatus(ref mInfo);
            memNum = mInfo.dwMemoryLoad.ToString();

            notifyIconMS.Icon = drawIcon1();
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
            bool st = true, ss = true;
            // 读取
            using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    switch (sr.ReadLine())
                    {
                        case "Theme=True":
                            st = (true);
                            break;
                        case "Theme=False":
                            st = (false);
                            break;
                        case "StartUp=True":
                            ss = (true);
                            break;
                        case "StartUp=False":
                            ss = (false);
                            break;
                    }
                }
            }
            setTheme(st);
            setStartUp(ss);
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

        // 设置主题
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

        // 设置自启
        private void setStartUp(bool b)
        {
            tsmiBoot.Checked = b;
            using (RegistryKey reg = Registry.CurrentUser)
            {
                using (RegistryKey run = reg.CreateSubKey(strRegRun))
                {
                    if (b)
                        run.SetValue(strRegName, path);
                    else
                        run.DeleteValue(strRegName, false);
                }
            }
            writeFile(strCfgFile);
        }

        // 画图标
        private Icon drawIcon1()
        {
            using (Bitmap bmp = new Bitmap(16, 16))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.Clear(Color.FromArgb(0, 0, 0, 0));
                    g.ResetClip();
                    g.Clear(Color.FromArgb(0, 0, 0, 0));
                    g.ResetClip();
                    g.DrawRectangle(pen, 0, 0, 15, 15);
                    g.DrawString(memNum, font, brush, 0, 1);
                }
                using (Icon icon = Icon.FromHandle(bmp.GetHicon()))
                {
                    return icon;
                }
            }
        }
    }
}
