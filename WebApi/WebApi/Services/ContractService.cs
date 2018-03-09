using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models.Config;
using WebApi.Models.Contract;
using WebApi.Models.Dto;

namespace WebApi.Services
{
    public class ContractService
    {
        public readonly HexBigInteger DefaultGas = new HexBigInteger(3000000);
        public readonly HexBigInteger Zero = new HexBigInteger(0);

        private static string contractAddress = "";

        private static Web3 _web3;
        private static Account _account;

        internal ContractMetadata GetMetadata()
        {
            return new ContractMetadata()
            {
                Abi = JsonConvert.SerializeObject(_contractInfo.Abi),
                Address = contractAddress
            };
        }

        private static ContractMetaInfo _contractInfo;

        public string ContractAddress { get; private set; }

        public ContractService(ContractConfig config)
        {
            Deploy(config.PrivateKey, config.NetworkUrl);
        }

        public void Deploy(string ownerPrivteKey, string nodeUrl)
        {
            _account = new Account(ownerPrivteKey);
            _web3 = new Web3(_account, nodeUrl);

            _contractInfo = GetContractInfo();
            var senderAddress = _account.Address;
            var transactionHash = _web3.Eth.DeployContract.SendRequestAsync(_contractInfo.GetAbi(), _contractInfo.ByteCode, senderAddress, DefaultGas, new object[] { }).GetAwaiter().GetResult();

            var receipt = _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
            }

            contractAddress = receipt.ContractAddress;
            CreateGame(GameMove.Rock, GameMove.Rock, GameMove.Rock);
            CreateGame(GameMove.Paper, GameMove.Paper, GameMove.Paper);
            CreateGame(GameMove.Scissors, GameMove.Scissors, GameMove.Scissors);

            AcceptGame(0, GameMove.Paper, GameMove.Rock, GameMove.Rock);
            var res = GetCompletedByIndex(0);
        }

        private ContractMetaInfo GetContractInfo()
        {
            var json = System.IO.File.ReadAllText("C:\\Users\\volland\\source\\repos\\DApp-Rock-Paper-Scissors\\Contracts\\build\\contracts\\GameBoard.json");
            return JsonConvert.DeserializeObject<ContractMetaInfo>(json);
        }

        public void CreateGame(GameMove move1, GameMove move2, GameMove move3)
        {
            var value = Nethereum.Util.UnitConversion.Convert.ToWei(100, Nethereum.Util.UnitConversion.EthUnit.Finney);
            HexBigInteger valueInHex = new HexBigInteger(value);
            var placeGameRequest = GetContract().GetFunction("placeGameRequest");
            var result = placeGameRequest.SendTransactionAsync(_account.Address, DefaultGas, valueInHex, (int)move1, (int)move2, (int)move3).GetAwaiter().GetResult();
        }

        private Contract GetContract()
        {

            return _web3.Eth.GetContract(_contractInfo.GetAbi(), contractAddress);
        }

        public IList<GameInfo> GetAllGames()
        {
            List<GameInitiated> createdGames = new List<GameInitiated>(); 
            var createdCount = GetCreatedGamesCount();

            for (long i = 0; i < createdCount; i++)
            {
                createdGames.Add(GetCreatedGame(i));
            }

            var completedCount = GetCompletedGamesCount();
            List<CompletedGameData> completedGames = new List<CompletedGameData>();
            for (long i = 0; i < completedCount; i++)
            {
                completedGames.Add(GetCompletedByIndex(i));
            }

            List<GameInfo> result = new List<GameInfo>();
            //Staretd            
            result.AddRange(createdGames
                .Where(created => !completedGames.Any(compl => compl.GameID == created.GameID))
                .Select(r => GameInfo.FromGameInitated(r))
                .ToList());
            //Completed
            result.AddRange(completedGames
                .Where(compl => createdGames.Any(created => compl.GameID == created.GameID))
                .Select(r => GameInfo.FromCompletedGameData(r))
                .ToList());
            return result;
        }

        public long GetCreatedGamesCount()
        {
            var getCreatedGamesCount = GetContract().GetFunction("getCreatedGamesCount");
            var result = getCreatedGamesCount.CallAsync<long>().GetAwaiter().GetResult();
            return result;
        }

        public GameInitiated GetCreatedGame(long index)
        {
            var getCreatedGameData = GetContract().GetFunction("getCreatedGameData");
            var result = getCreatedGameData.CallDeserializingToObjectAsync<GameInitiated>().GetAwaiter().GetResult();
            result.GameID = index;
            return result;
        }

        public CompletedGameData GetCompletedByIndex(long index)
        {
            var getCreatedGameData = GetContract().GetFunction("getCompletedGameLog");
            var result = getCreatedGameData.CallDeserializingToObjectAsync<CompletedGameData>(index).GetAwaiter().GetResult();
            return result;
        }

        public CompletedGameData GetCompletedByGameID(long gameID)
        {
            var getCreatedGameData = GetContract().GetFunction("getCompletedGameData");
            var result = getCreatedGameData.CallDeserializingToObjectAsync<CompletedGameData>(gameID).GetAwaiter().GetResult();
            return result;
        }

        public long GetCompletedGamesCount()
        {
            var getCreatedGamesCount = GetContract().GetFunction("getCompletedGamesCount");
            var result = getCreatedGamesCount.CallAsync<long>().GetAwaiter().GetResult();
            return result;
        }

        public void AcceptGame(int gameID, GameMove move1, GameMove move2, GameMove move3)
        {
            var value = Nethereum.Util.UnitConversion.Convert.ToWei(100, Nethereum.Util.UnitConversion.EthUnit.Finney);
            HexBigInteger valueInHex = new HexBigInteger(value);
            var placeGameRequest = GetContract().GetFunction("acceptGameRequest");
            var result = placeGameRequest.SendTransactionAsync(_account.Address, DefaultGas, valueInHex, gameID, (int)move1, (int)move2, (int)move3).GetAwaiter().GetResult();
        }
    }
}
