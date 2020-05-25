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
        /// 藥歷編號
        /// </summary>
        [DisplayName("藥歷編號")]
        public int Item { get; set; }

        /// <summary>
        /// 病人編號
        /// </summary>
        [DisplayName("病人編號")]
        public string PatientId { get; set; }


        /// <summary>
        /// 藥物類別ID
        /// </summary>
        [DisplayName("藥物名稱ID")]
        public int MedNameId { get; set; }

        /// <summary>
        /// 藥物類別ID
        /// </summary>
        [DisplayName("藥物名稱")]
        public string MedName { get; set; }

        /// <summary>
        /// 藥物類別ID
        /// </summary>
        [DisplayName("藥物類別ID")]
        public string MedClassId { get; set; }

        /// <summary>
        /// 藥物類別ID
        /// </summary>
        [DisplayName("藥物類別")]
        public string MedClass { get; set; }

        /// <summary>
        /// 藥物類別ID
        /// </summary>
        [DisplayName("藥物開藥時間")]
        public string MedStart { get; set; }

        /// <summary>
        /// 藥物類別ID
        /// </summary>
        [DisplayName("藥物結束時間")]
        public string MedEnd { get; set; }

        /// <summary>
        /// 藥物類別ID
        /// </summary>
        [DisplayName("開藥來源")]
        public string MedSource { get; set; }

        public String[] ToArray()
        {
            List<string> arr = new List<string>();

            foreach (var prop in typeof(MedData).GetProperties())
            {
                string value = "";
                if (prop.GetValue(this, null) != null)
                {
                    value = prop.GetValue(this, null).ToString();
                }

                arr.Add(value);
            }

            return arr.ToArray();
        }
    }
}