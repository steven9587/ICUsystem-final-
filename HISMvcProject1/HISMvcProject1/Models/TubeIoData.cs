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
        /// Location_X
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("X座標")]
        public int LocationX { get; set; }


        /// <summary>
        /// Location_Y
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("Y座標")]
        public int LocationY { get; set; }

        /// <summary>
        /// IO日期
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("日期")]
        public string IoDate { get; set; }

        /// <summary>
        /// IO 輸入者
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("日期加一")]
        public string IoDateAddOne { get; set; }

        /// <summary>
        /// IO Intake
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("Intake")]
        public int IoIntake { get; set; }

        /// <summary>
        /// IO Output
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("Output")]
        public int IoOutput { get; set; }

        /// <summary>
        /// IO差異
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("差異")]
        public int Difference { get; set; }
      //  public static IEnumerable<DataRow> Rows { get; internal set; }

        /// <summary>
        /// IO 輸入者
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("輸入者")]
        public string Operator { get; set; }

        /// <summary>
        /// 總IO查詢開始日期
        /// </summary>
        [DisplayName("總IO查詢開始日期")]
        public string IoStartDate { get; set; }

        /// <summary>
        /// 總IO查詢結束日期
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("總IO查詢結束日期")]
        public string IoEndDate { get; set; }

        /// <summary>
        /// 總io日期
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("總io日期")]
        public string FullIoDate { get; set; }

        /// <summary>
        /// 總io時間
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("總io時間")]
        public string FullIoTime { get; set; }

        /// <summary>
        /// 總io項目名稱
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("總io項目名稱")]
        public string FullIoName { get; set; }

        /// <summary>
        /// 總io輸入量
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("總io輸入量")]
        public string FullIoIn { get; set; }

        /// <summary>
        /// 總io輸入者
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("總io輸入者")]
        public string FullIoUser { get; set; }

        /// <summary>
        /// 總io輸出量
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("總io輸出量")]
        public string FullIoOut { get; set; }

    }
}