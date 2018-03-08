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

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly HexBigInteger _defaultGas = new HexBigInteger(3000000);
        private readonly HexBigInteger _zero = new HexBigInteger(0);

        public class ContractMetaInfo
        {
            public object Abi { get; set; }
            public string ByteCode { get; set; }

            public string GetAbi()
            {
                return this.Abi.ToString();
            }
        }

        private static string contractAddress = "0x92fd05360bd54ec9b1773d28395ee547245e3290";

        private ContractMetaInfo GetContractInfo()
        {
            var json = System.IO.File.ReadAllText("C:\\Users\\volland\\source\\repos\\DApp-Rock-Paper-Scissors\\WebApi\\WebApi\\Contracts\\Simple.json");
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
            /**/
            _account = new Account("8bf588cfcd9daa57ada648e1a97cd421b3883951523d3a23756bcc32c50d7e5f");
            _web3 = new Web3(_account, "http://localhost:8545");

            if (contractAddress == string.Empty)
            {
                var contract = GetContractInfo();
                var senderAddress = _account.Address;
                var transactionHash = this._web3.Eth.DeployContract.SendRequestAsync(contract.GetAbi(), contract.ByteCode, senderAddress, _defaultGas, new object[] { }).GetAwaiter().GetResult();

                var receipt = _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
                while (receipt == null)
                {
                    Thread.Sleep(1000);
                    receipt = _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash).GetAwaiter().GetResult();
                }

                contractAddress = receipt.ContractAddress;
            }
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
            var all = incrementers.CreateFilterAsync().GetAwaiter().GetResult();

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


        private class EventObject
        {
            [Parameter("address", "incrementer", 1, true)]
            public string Sender { get; set; }


            [Parameter("int", "value", 2, true)]
            public int Value { get; set; }
        }
    }
}
