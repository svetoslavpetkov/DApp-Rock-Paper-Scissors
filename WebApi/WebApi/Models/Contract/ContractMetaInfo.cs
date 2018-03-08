using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Contract
{
    public class ContractMetaInfo
    {
        public object Abi { get; set; }
        public string ByteCode { get; set; }

        public string GetAbi()
        {
            return this.Abi.ToString();
        }
    }
}
