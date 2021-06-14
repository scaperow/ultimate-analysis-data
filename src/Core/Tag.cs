using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldSoft.Identiter.Core
{
    /// <summary>
    /// 标识
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// 定额集合
        /// </summary>
        public List<SubheadingTag> Subheading { set; get; }

        /// <summary>
        /// 清单编号
        /// </summary>
        public string QDBH { set; get; }

        public Tag()
        {
            Subheading = new List<SubheadingTag>();
        }
    }

    public class SubheadingTag
    {
        /// <summary>
        /// 定额号
        /// </summary>
        public string DEH { set; get; }

        /// <summary>
        /// 工程量系数
        /// </summary>
        public int GCLXS { set; get; }

        /// <summary>
        /// 换算前材料编号
        /// </summary>
        public string HSQCLBH { set; get; }

        /// <summary>
        /// 换算后材料编号
        /// </summary>
        public string HSHCLBH { set; get; }
    }
}
