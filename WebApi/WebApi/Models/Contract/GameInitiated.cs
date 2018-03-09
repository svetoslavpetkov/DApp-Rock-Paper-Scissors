﻿using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Contract
{
    [FunctionOutput]
    public class GameInitiated
    {
        [Parameter("address", "", 1)]
        public string Player1 { get; set; }

        [Parameter("uint256", "", 2)]
        public long Value { get; set; }

        [Parameter("uint256", "", 3)]
        public long CreatedDate { get; set; }

        public long GameID { get; set; }
    }
}
