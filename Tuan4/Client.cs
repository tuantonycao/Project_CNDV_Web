using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tuan4Service;
using System.ServiceModel;

namespace TestClient
{
    public partial class Form1 : Form
    {

        ITuan4 app = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnlaytt_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbbkieukn.SelectedIndex == 0)
                {
                    EndpointAddress address = new EndpointAddress(new Uri("http://localhost:8888/BasicHttpBinding"));
                    ChannelFactory<ITuan4> factory = new ChannelFactory<ITuan4>(new BasicHttpBinding(), address);
                    app = factory.CreateChannel();
                    rtxtthongtin.Text = app.GetAuthors();
                }
                if (cbbkieukn.SelectedIndex==1)
                {
                    EndpointAddress address = new EndpointAddress(new Uri("http://localhost:8888/WSHttpBinding"));
                    ChannelFactory<ITuan4> factory = new ChannelFactory<ITuan4>(new WSHttpBinding(), address);
                    app = factory.CreateChannel();
                    rtxtthongtin.Text = app.GetAuthors();
                }
                if (cbbkieukn.SelectedIndex==2)
                {
                    EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:8888/NetTcpBinding"));
                    ChannelFactory<ITuan4> factory = new ChannelFactory<ITuan4>(new NetTcpBinding(), address);
                    app = factory.CreateChannel();
                    rtxtthongtin.Text = app.GetAuthors();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtxtthongtin.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rtxtthongtin.Text = "";
            cbbkieukn.SelectedIndex = 0;
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
                app = null;
                MessageBox.Show("Đã ngắt kết nối!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rtxtthongtin.Text = "";
        }
    }
}
