using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Tuan4Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : ITuan4
    {
        public string GetAuthors()
        {
            return "Cao Quốc Tuấn, Lê Thanh Tùng. Đề tài ứng dụng WS cung cấp thông tin quảng cáo.";
        }
    }
}
