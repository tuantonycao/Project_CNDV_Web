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
using Tuan4Service;

namespace Tuan4Host
{
    public partial class Form1 : Form
    {
                
        bool serviceStarted = false;
        ServiceHost myhost = null;
        ServiceMetadataBehavior behavior;
        public string Address;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnstar_Click(object sender, EventArgs e)
        {
            if (!serviceStarted)
            {
                Uri baseAddress;

                try
                {
                    if (rbbbasic.Checked == true)
                    {
                        Address="http://"+txtbaseadd.Text+"/"+txtadd1.Text;
                        baseAddress = new Uri(Address);
                        myhost = new ServiceHost(typeof(Tuan4Service.Service1), baseAddress);
                        myhost.AddServiceEndpoint(typeof(Tuan4Service.ITuan4), new BasicHttpBinding(), baseAddress);
                       
                        if (ckshowmex.Checked == true)
                        {
                            behavior = new ServiceMetadataBehavior();
                            behavior.HttpGetEnabled = true;
                            myhost.Description.Behaviors.Add(behavior);
                            behavior.HttpGetUrl = baseAddress;
                            myhost.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX1");
                        }
                        myhost.Open();
                        txtthongbao.Text = "Host thành công theo giao thức BasicHttpBinding!!!";
                    }

                    if (rbbws.Checked == true)
                    {
                        Address = "http://" + txtbaseadd.Text + "/" + txtadd2.Text;
                        baseAddress = new Uri(Address);
                        myhost = new ServiceHost(typeof(Tuan4Service.Service1), baseAddress);
                        myhost.AddServiceEndpoint(typeof(Tuan4Service.ITuan4), new WSHttpBinding(), baseAddress);

                        if (ckshowmex.Checked == true)
                        {
                            behavior = new ServiceMetadataBehavior();
                            behavior.HttpGetEnabled = true;
                            myhost.Description.Behaviors.Add(behavior);
                            behavior.HttpGetUrl = baseAddress;
                            myhost.AddServiceEndpoint(typeof(IMetadataExchange), new WSHttpBinding(), "MEX2");
                        }
                        myhost.Open();
                        txtthongbao.Text = "Host thành công theo giao thức WSHttpBinding!!!";
                    }

                    if (rbbnet.Checked == true)
                    {
                        Address = "net.tcp://" + txtbaseadd.Text + "/" + txtadd3.Text;
                        baseAddress = new Uri(Address);
                        myhost = new ServiceHost(typeof(Tuan4Service.Service1), baseAddress);
                        myhost.AddServiceEndpoint(typeof(Tuan4Service.ITuan4), new NetTcpBinding(), baseAddress);
                        myhost.Open();
                        txtthongbao.Text = "Host thành công theo giao thức NetTcpBinding!!!";
                    }

                    serviceStarted = true;
                    btnstop.Enabled = true;
                    btnstar.Enabled = false;
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
            btnstar.Enabled = true;
            btnstop.Enabled = false;
            txtthongbao.Text = "Ngắt kết nối!!!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnstop.Enabled = false;
        }
    }
}
