using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Contract;

namespace WebApi.Models.Dto
{
    public class GameInfo
    {
        public long GameID { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }

        public GameStatus Status { get; set; }

        public long Value { get; set; }

        public static GameInfo FromGameInitated(GameInitiated gameInitiated )
        {
            return new GameInfo() {
                GameID = gameInitiated.GameID,
                Player1 = gameInitiated.Player1,
                Status = GameStatus.Started,
                Value = (long)Nethereum.Util.UnitConversion.Convert.ToWei(0.1)
            };
        }

        public static GameInfo FromCompletedGameData(CompletedGameData completedGameData)
        {
            return new GameInfo()
            {
                GameID = completedGameData.GameID,
                Player1 = completedGameData.Player1,
                Player2 = completedGameData.Player2,
                Status = GameStatus.Completed,
                Value = (long)Nethereum.Util.UnitConversion.Convert.ToWei(0.1)
            };
        }
    }
}
