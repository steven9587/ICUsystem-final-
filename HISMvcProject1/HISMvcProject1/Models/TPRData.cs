using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISMvcProject1.Models
{
    public class TPRData
    {
        /// <summary>
        /// TPR開始日期
        /// </summary>
        [DisplayName("TPR開始日期")]
        public string TPRStart { get; set; }
        /// <summary>
        /// TPR結束日期
        /// </summary>
        [DisplayName("TPR結束日期")]
        public string TPREnd { get; set; }
        /// <summary>
        /// 病人編號
        /// </summary>
        [DisplayName("病人編號")]
        public string PatientId { get; set; }
    }
}