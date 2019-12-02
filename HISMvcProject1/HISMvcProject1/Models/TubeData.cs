using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISMvcProject1.Models
{
    public class TubeData
    {
        /// <summary>
        /// 圖書類別
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("管路位置")]
        public string TubePartName { get; set; }

        /// <summary>
        /// 圖書類別ID
        /// </summary>
        [DisplayName("管路位置")]
        [Required(ErrorMessage = "此欄位必填")]
        public string TubePartID { get; set; }
    }
}