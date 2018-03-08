using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Contract
{
    [FunctionOutput]
    public class CompletedGameData
    {
        /*
         (address player1, uint8 player1Move0, uint8 player1Move1, uint8 player1Move2, 
                address player2, uint8 player2Move0, uint8 player2Move1, uint8 player2Move2,  uint8 winner)
         */

        [Parameter("address", "player1", 1)]
        public string Player1 { get; set; }

        [Parameter("uint8", "player1Move0", 2)]
        public int Player1Move0 { get; set; }

        [Parameter("uint8", "player1Move1", 3)]
        public int Player1Move1 { get; set; }

        [Parameter("uint8", "player1Move2", 4)]
        public int Player1Move2 { get; set; }

        [Parameter("address", "player2", 5)]
        public string Player2 { get; set; }

        [Parameter("uint8", "player2Move0", 6)]
        public int Player2Move0 { get; set; }

        [Parameter("uint8", "player2Move1", 7)]
        public int Player2Move1 { get; set; }

        [Parameter("uint8", "player2Move2", 8)]
        public int Player2Move2 { get; set; }

        [Parameter("uint8", "winner", 9)]
        public int Winner { get; set; }

        [Parameter("uint256", "returnGameID", 10)]
        public long GameID { get; set; }
    }
}
