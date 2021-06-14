using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldSoft.Identiter.Common
{
    public enum IdentityResultStateEnum
    {
        /// <summary>
        /// 无法报价
        /// </summary>
        Unable,
        /// <summary>
        /// 报价变化
        /// </summary>
        Changed,
        /// <summary>
        /// 报价差异
        /// </summary>
        Difference,
        /// <summary>
        /// 报价成功
        /// </summary>
        Success
    }

    public enum MatchMode
    {
        Equals, Contains, Regex
    }

    public enum ProfessionalEnum
    {
        /// <summary>
        /// 建筑装饰
        /// </summary>
        Decoration,
        /// <summary>
        /// 安装
        /// </summary>
        Installation,
        /// <summary>
        /// 市政
        /// </summary>
        Municipal,
        /// <summary>
        /// 园林
        /// </summary>
        Gardens,
        /// <summary>
        /// 绿化
        /// </summary>
        Green,
    }

    public enum ComplexSymbalEnum
    {
        /// <summary>
        /// 小于
        /// </summary>
        Less = 1, 
        /// <summary>
        /// 大于
        /// </summary>
        Great = 2, 
        /// <summary>
        /// 等于
        /// </summary>
        Equal = 4
    }

    public enum Operator
    {
        /// <summary>
        /// 加
        /// </summary>
        Addition,
        /// <summary>
        /// 减
        /// </summary>
        Subtraction,
        /// <summary>
        /// 乘
        /// </summary>
        Multiplication,
        /// <summary>
        /// 除
        /// </summary>
        Division
    }
}
