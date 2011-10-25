using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using WS_Service;

namespace Client
{
    public partial class Form1 : Form
    {
        IService1 service = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbkieukn.SelectedIndex = 0;
            rtxtthongtin.Text = "";
        }

        private void btnketnoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbbkieukn.SelectedIndex == 0)
                {
                    EndpointAddress address = new EndpointAddress(new Uri("http://localhost:8888/BasicHttpBinding/Test1"));
                    ChannelFactory<IService1> factory = new ChannelFactory<IService1>(new BasicHttpBinding(), address);
                    service = factory.CreateChannel();
                    rtxtthongtin.Text = service.GetAuthors();
                }
                if (cbbkieukn.SelectedIndex == 1)
                {
                    EndpointAddress address = new EndpointAddress(new Uri("http://localhost:8888/WSHttpBinding/Test2"));
                    ChannelFactory<IService1> factory = new ChannelFactory<IService1>(new WSHttpBinding(), address);
                    service = factory.CreateChannel();
                    rtxtthongtin.Text = service.GetAuthors();
                }
                if (cbbkieukn.SelectedIndex == 2)
                {
                    EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:8888/NetTcpBinding/Test3"));
                    ChannelFactory<IService1> factory = new ChannelFactory<IService1>(new NetTcpBinding(), address);
                    service = factory.CreateChannel();
                    rtxtthongtin.Text = service.GetAuthors();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtxtthongtin.Text = "";
            }
        }

        private void btnngat_Click(object sender, EventArgs e)
        {
            service = null;
            MessageBox.Show("Đã ngắt kết nối tới máy chủ...","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            rtxtthongtin.Text = "";
        }
    }
}
