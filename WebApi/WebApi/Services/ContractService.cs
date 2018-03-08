using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models.Contract;

namespace WebApi.Services
{
    public class ContractService
    {
        public readonly HexBigInteger DefaultGas = new HexBigInteger(3000000);
        public readonly HexBigInteger Zero = new HexBigInteger(0);

        private static string contractAddress = "0xcdfc14988ff19efedfd57d853dcd6e67d1bd536f";

        private static Web3 _web3;
        private static Account _account;

        public string ContractAddress { get; private set; }

        public void Deploy(string ownerPrivteKey, string nodeUrl)
        {
            _account = new Account(ownerPrivteKey);
            _web3 = new Web3(_account, nodeUrl);

            var contract = GetContractInfo();
            var senderAddress = _account.Address;
            var transactionHash = _web3.Eth.DeployContract.SendRequestAsync(contract.GetAbi(), contract.ByteCode, senderAddress, DefaultGas, new object[] { }).GetAwaiter().GetResult();

            var receipt = _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
            }

            contractAddress = receipt.ContractAddress;
        }

        private ContractMetaInfo GetContractInfo()
        {
            var json = System.IO.File.ReadAllText("C:\\Users\\volland\\source\\repos\\DApp-Rock-Paper-Scissors\\WebApi\\WebApi\\Contracts\\Simple.json");
            return JsonConvert.DeserializeObject<ContractMetaInfo>(json);
        }


    }
}
