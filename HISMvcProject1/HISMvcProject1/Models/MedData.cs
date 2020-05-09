using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISMvcProject1.Models
{
    public class MedData
    {
        /// <summary>
        /// 病人編號
        /// </summary>
        [DisplayName("病人編號")]
        public string PatientID { get; set; }

        /// <summary>
        /// 藥物類別ID
        /// </summary>
        [DisplayName("藥物類別ID")]
        public string MedID { get; set; }


    }
}