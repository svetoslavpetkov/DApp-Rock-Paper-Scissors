using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using WebApi.Models.Contract;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly HexBigInteger _defaultGas = new HexBigInteger(3000000);
        private readonly HexBigInteger _zero = new HexBigInteger(0);

        private static string contractAddress = "";

        private ContractMetaInfo GetContractInfo()
        {
            var json = System.IO.File.ReadAllText("C:\\Users\\volland\\source\\repos\\DApp-Rock-Paper-Scissors\\Contracts\\build\\contracts\\GameBoard.json");
            return JsonConvert.DeserializeObject<ContractMetaInfo>(json);
        }

        private Contract GetContract(string address)
        {
            var info = GetContractInfo();
            return _web3.Eth.GetContract(info.GetAbi(), address);
        }

        private readonly Web3 _web3;
        private readonly Account _account;

        public ValuesController()
        {
            //ContractService service = new ContractService();
            //service.Deploy("c87509a1c067bbde78beb793e6fa76530b6382a4c0241e5e4a9ec0a0f44dc0d3", "http://localhost:9545");
            ///**/
            //_account = new Account("c87509a1c067bbde78beb793e6fa76530b6382a4c0241e5e4a9ec0a0f44dc0d3");
            //_web3 = new Web3(_account, "http://localhost:9545");

            //if (contractAddress == string.Empty)
            //{
            //    var contract = GetContractInfo();
            //    var senderAddress = _account.Address;
            //    var transactionHash = this._web3.Eth.DeployContract.SendRequestAsync(contract.GetAbi(), contract.ByteCode, senderAddress, _defaultGas, new object[] { }).GetAwaiter().GetResult();

            //    var receipt = _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
            //    while (receipt == null)
            //    {
            //        Thread.Sleep(1000);
            //        receipt = _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
            //    }

            //    contractAddress = receipt.ContractAddress;
            //}
        }

        // GET api/values
        [HttpGet]
        public void Get()
        {
            var contract = GetContract(contractAddress);
            //var contractBalance = contract.GetFunction("increment").CallAsync<BigInteger>().GetAwaiter().GetResult();
            var contractBalance = contract.GetFunction("getValue").CallAsync<BigInteger>().GetAwaiter().GetResult();
        }

        // POST api/values
        [HttpGet("events")]
        public void Events()
        {
            var contract = GetContract(contractAddress);
            var incrementers = contract.GetEvent("incrementers");
            var all = incrementers.CreateFilterAsync(new Nethereum.RPC.Eth.DTOs.BlockParameter(new HexBigInteger(0))).GetAwaiter().GetResult();

            var allResult = incrementers.GetFilterChanges<EventObject>(all).GetAwaiter().GetResult();
            var allRaw = incrementers.GetAllChanges<EventObject>(all).GetAwaiter().GetResult();
        }

        // DELETE api/values/5
        [HttpGet("increment")]
        public void Increment(int id)
        {
            var contract = GetContract(contractAddress);
            var increment = contract.GetFunction("increment");
            increment.SendTransactionAsync(_account.Address, _defaultGas, _zero, 10).GetAwaiter().GetResult();
        }

        [HttpGet("get-event")]
        public void GetEvent(int id)
        {
            var contract = GetContract(contractAddress);
            var increment = contract.GetFunction("getEvent");
            var data = increment.CallDeserializingToObjectAsync<DataObject>(0).GetAwaiter().GetResult();
        }

        [FunctionOutput]
        private class EventObject
        {
            [Parameter("address", "incrementer", 1)]
            public string Sender { get; set; }


            [Parameter("uint256", "value", 2)]
            public BigInteger Value { get; set; }
        }

        [FunctionOutput]
        private class DataObject
        {
            [Parameter("address", "incrementer", 1)]
            public string Sender { get; set; }


            [Parameter("uint256", "value", 2)]
            public BigInteger Value { get; set; }
        }
    }
}
