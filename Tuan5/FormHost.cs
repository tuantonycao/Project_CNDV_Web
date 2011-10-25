using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using WS_Service;

namespace Host
{
    public partial class FormHost : Form
    {
        bool serviceStarted = false;
        ServiceHost myhost = null;
        ServiceMetadataBehavior behavior;
        public string Url1;
        public string Url2;
        public string Url3;

        public FormHost()
        {
            InitializeComponent();
        }

        private void FormHost_Load(object sender, EventArgs e)
        {
            btnstop.Enabled = false;
            txtmessage.Text = "Chưa có kết nối...!";
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            if (!serviceStarted)
            {
                Uri baseAddress;

                try
                {
                    if (rdbbasicbinding.Checked == true)
                    {
                        Url1 = "http://" + txtbaseadd.Text + "/"+ txtcontract1.Text+"/"+txtadd1.Text;
                        baseAddress = new Uri(Url1);
                        BasicHttpBinding basic = new BasicHttpBinding();
                        basic.OpenTimeout = System.TimeSpan.Parse("00:00:30");
                        basic.CloseTimeout = System.TimeSpan.Parse("00:00:30");
                        basic.SendTimeout = System.TimeSpan.Parse("00:00:30");
                        basic.ReceiveTimeout = System.TimeSpan.Parse("00:00:30");
                        basic.MaxBufferPoolSize = 30000;
                        basic.MaxBufferSize = 30000;
                        basic.MaxReceivedMessageSize = 30000;
                        myhost = new ServiceHost(typeof(WS_Service.Service1), baseAddress);
                        myhost.AddServiceEndpoint(typeof(WS_Service.IService1),basic, baseAddress);

                        if (ckmex.Checked == true)
                        {
                            behavior = new ServiceMetadataBehavior();
                            behavior.HttpGetEnabled = true;
                            myhost.Description.Behaviors.Add(behavior);
                            behavior.HttpGetUrl = baseAddress;
                            myhost.AddServiceEndpoint(typeof(IMetadataExchange), basic, "MEX1");
                        }
                        myhost.Open();
                        txtmessage.Text = "Host thành công theo giao thức BasicHttpBinding!!!";
                    }

                    if (rdbwsbinding.Checked == true)
                    {
                        Url2 = "http://" + txtbaseadd.Text + "/" +txtcontract2.Text+"/" +txtadd2.Text;
                        baseAddress = new Uri(Url2);
                        WSHttpBinding ws = new WSHttpBinding();
                        ws.CloseTimeout = System.TimeSpan.Parse("00:00:30");
                        ws.OpenTimeout = System.TimeSpan.Parse("00:00:30");
                        ws.ReceiveTimeout = System.TimeSpan.Parse("00:00:30");
                        ws.SendTimeout = System.TimeSpan.Parse("00:00:30");
                        ws.MaxBufferPoolSize = 30000;
                        ws.MaxReceivedMessageSize = 30000;
                        myhost = new ServiceHost(typeof(WS_Service.Service1), baseAddress);
                        myhost.AddServiceEndpoint(typeof(WS_Service.IService1), ws, baseAddress);

                        if (ckmex.Checked == true)
                        {
                            behavior = new ServiceMetadataBehavior();
                            behavior.HttpGetEnabled = true;
                            myhost.Description.Behaviors.Add(behavior);
                            behavior.HttpGetUrl = baseAddress;
                            myhost.AddServiceEndpoint(typeof(IMetadataExchange), ws, "MEX2");
                        }
                        myhost.Open();
                        txtmessage.Text = "Host thành công theo giao thức WSHttpBinding!!!";
                    }

                    if (rdbtcpbinding.Checked == true)
                    {
                        Url3 = "net.tcp://" + txtbaseadd.Text + "/" +txtcontract3.Text+"/"+ txtadd3.Text;
                        baseAddress = new Uri(Url3);
                        NetTcpBinding net = new NetTcpBinding();
                        net.CloseTimeout = System.TimeSpan.Parse("00:00:30");
                        net.OpenTimeout = System.TimeSpan.Parse("00:00:30");
                        net.SendTimeout = System.TimeSpan.Parse("00:00:30");
                        net.ReceiveTimeout = System.TimeSpan.Parse("00:00:30");
                        net.MaxBufferPoolSize = 30000;
                        net.MaxBufferSize = 30000;
                        net.MaxReceivedMessageSize = 30000;
                        myhost = new ServiceHost(typeof(WS_Service.Service1), baseAddress);
                        myhost.AddServiceEndpoint(typeof(WS_Service.IService1), net, baseAddress);
                        myhost.Open();
                        txtmessage.Text = "Host thành công theo giao thức NetTcpBinding!!!";
                    }

                    serviceStarted = true;
                    btnstop.Enabled = true;
                    btnstart.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            myhost.Close();
            serviceStarted = false;
            btnstart.Enabled = true;
            btnstop.Enabled = false;
            txtmessage.Text = "Ngắt kết nối!!!";
        }
   }
}
