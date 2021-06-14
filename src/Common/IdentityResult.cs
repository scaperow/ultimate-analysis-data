using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GoldSoft.Identiter.Common;
using Newtonsoft.Json;

namespace GoldSoft.Identiter.Common
{
    public class IdentityResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public IdentityResultStateEnum State
        {
            set;
            get;
        }
        public IdentityResult()
        {
            RulesMatched = new List<DataRow>();
            ExcelsMatched = new List<Excel>();
        }
        public IdentityResult(string relation)
        {
            ExcelsMatched = new List<Excel>();
            RulesMatched = new List<DataRow>();
            Relation = relation;
        }

        [JsonIgnore]
        public List<DataRow> RulesMatched
        {
            set;
            get;
        }
        public List<Excel> ExcelsMatched
        {
            set;
            get;
        }

        public string Relation { set; get; }
        public string Message
        {
            set;
            get;
        }
        public string ResultForUser
        {
            get;
            set;
        }
        public string ResultForProgram
        {
            get;
            set;
        }
    }
}
