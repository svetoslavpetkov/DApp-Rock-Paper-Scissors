using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Contract
{
    [FunctionOutput]
    public class GameInitiated
    {
        [Parameter("address", "incrementer", 1)]
        public string Player1 { get; set; }

        [Parameter("uint256", "incrementer", 2)]
        public long Value { get; set; }

        public long GameID { get; set; }
    }
}
