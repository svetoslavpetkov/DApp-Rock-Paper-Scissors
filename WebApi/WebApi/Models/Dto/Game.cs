using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Contract;
using WebApi.Util;

namespace WebApi.Models.Dto
{
    public class Game : GameInfo
    {
        public int Winner { get; set; }

        public List<int> Player1Moves { get; set; } = new List<int>();
        public List<int> Player2Moves { get; set; } = new List<int>();

        public static new Game FromGameInitated(GameInitiated gameInitiated)
        {
            return new Game()
            {
                GameID = gameInitiated.GameID,
                Player1 = gameInitiated.Player1,
                Status = GameStatus.Started,
                Value = (long)Nethereum.Util.UnitConversion.Convert.ToWei(0.1),
                CreatedDate = ConvertUtil.GetDateString(gameInitiated.CreatedDate)
            };
        }

        public static new Game FromCompletedGameData(CompletedGameData completedGameData)
        {
            return new Game()
            {
                GameID = completedGameData.GameID,
                Player1 = completedGameData.Player1,
                Player2 = completedGameData.Player2,
                Status = GameStatus.Completed,
                Value = (long)Nethereum.Util.UnitConversion.Convert.ToWei(0.1),
                CreatedDate = ConvertUtil.GetDateString(completedGameData.CreatedDate),
                CompletedDate = ConvertUtil.GetDateString(completedGameData.CompletedDate),
                Winner = completedGameData.Winner,
                Player1Moves = new List<int>() { completedGameData.Player1Move0, completedGameData.Player1Move1, completedGameData.Player1Move2 },
                Player2Moves = new List<int>() { completedGameData.Player2Move0, completedGameData.Player2Move1, completedGameData.Player2Move2 }
            };
        }
    }
}
