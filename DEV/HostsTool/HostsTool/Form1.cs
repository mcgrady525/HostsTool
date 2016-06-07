using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tracy.Frameworks.Common.Extends;
using System.IO;

namespace HostsTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// hosts设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //读取配置内容更新hosts文件
            //执行cmd命令刷新dns
            try
            {
                //读取配置内容更新hosts文件
                var hostPath = @"C:\Windows\System32\drivers\etc\hosts";
                var config = File.ReadAllText(@"config.txt", Encoding.UTF8);
                if (config.IsNullOrEmpty())
                {
                    MessageBox.Show("配置模板内容不能为空!", "Hosts设置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                File.WriteAllText(hostPath, config, Encoding.UTF8);

                //执行cmd命令刷新dns
                var outPut= string.Empty;
                CmdHelper.RunCmd(@"ipconfig /flushdns", out outPut);

                MessageBox.Show("OK", "Hosts设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Hosts设置", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

        }
    }
}
