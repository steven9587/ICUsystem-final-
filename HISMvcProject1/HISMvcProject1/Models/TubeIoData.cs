using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace HISMvcProject1.Models
{
    public class TubeIoData
    {

        /// <summary>
        /// 病人編號
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("病人編號")]
        public string PatientID { get; set; }

        /// <summary>
        /// 第一IO日期
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("日期")]
        public string IoDate { get; set; }

        /// <summary>
        /// 第一IO Intake
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("Intake")]
        public int IoIntake { get; set; }

        /// <summary>
        /// 第一IO Output
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("Output")]
        public int IoOutput { get; set; }

        /// <summary>
        /// 第一IO差異
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("差異")]
        public int Difference { get; set; }
        public static IEnumerable<DataRow> Rows { get; internal set; }
    }
}