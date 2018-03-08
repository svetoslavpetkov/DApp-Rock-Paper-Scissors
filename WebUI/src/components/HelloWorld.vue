<template>
  <article>
        <div>
            <button class="btn" v-on:click="increment">increment</button>
            <button class="btn" v-on:click="getValue">getValue</button>
            <button class="btn" v-on:click="readEvents">readEvents</button>            
            <h1>Pending games</h1>        
            <div class="card mt-3 mb-3" v-for="game in games" v-bind:key="game.address">
                <div class="card-header">
                    {{ game.address }}
                </div>
                <div class="card-body row">
                <p class="col-6">Owner: {{game.player1}} </p>
                <p class="col-4">Value: {{ game.value }} eth</p>
                <p class="col-2">{{ game.createdDate }}</p>
                </div>
                <div class="card-footer">
                  <button class="btn">Play</button>
                </div>
            </div>
            <div class="row">
              <div class="col-sm-6" v-for="game in games" v-bind:key="game.address">
                <div class="card">
                  <div class="card-body">
                    <h5 class="card-title">{{ game.address }}</h5>
                    <p class="card-text">Owner: {{game.player1}}</p>
                    <p class="card-text">Value: {{ game.value }} eth</p>
                    <p class="card-text">{{ game.createdDate }}</p>                  
                    <button class="btn btn-primary">Play</button>
                  </div>
                </div>
              </div>
            </div>
        </div>
  </article>
</template>

<script>
import EthLightwallet from 'eth-lightwallet'
import HookedWeb3Provider from 'web3'
import Web3 from 'web3'
import Wallet from '../../static/js/wallet'


console.log(Web3);
console.log(Web3.providers);
console.log(Web3.providers.HttpProvider);
var web3instance = new Web3();

var  abi = [
    {
      "constant": true,
      "inputs": [],
      "name": "value",
      "outputs": [
        {
          "name": "",
          "type": "uint256"
        }
      ],
      "payable": false,
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "payable": false,
      "stateMutability": "nonpayable",
      "type": "constructor"
    },
    {
      "anonymous": false,
      "inputs": [
        {
          "indexed": false,
          "name": "incrementer",
          "type": "address"
        },
        {
          "indexed": false,
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "incrementers",
      "type": "event"
    },
    {
      "constant": false,
      "inputs": [
        {
          "name": "a",
          "type": "uint256"
        }
      ],
      "name": "increment",
      "outputs": [],
      "payable": false,
      "stateMutability": "nonpayable",
      "type": "function"
    },
    {
      "constant": true,
      "inputs": [],
      "name": "getValue",
      "outputs": [
        {
          "name": "",
          "type": "uint256"
        }
      ],
      "payable": false,
      "stateMutability": "view",
      "type": "function"
    }
  ];

var address = '0xd63cccacaeda7d376d2b1fa70a46c57fd0be6de1';

var SimpleContract = web3.eth.contract(abi);
var simpleContract = SimpleContract.at(address);

// Or pass a callback to start watching immediately
var event = simpleContract.incrementers(function(error, result) {
    if (!error){
        console.log('event arrived');
        console.log(result);
    }
    else
        console.log(error);
});

export default {
  name: 'HelloWorld',
  data () {
        return {
           games : [{
             address: 'sadaslkjfndjfnsdkjfnsjfnsdjfnsdj',
             player1: 'asdjasnfkjsadnfjasdnfjsdfnsdajfdnasdkj,',
             value: '0.001',
             createdDate: '2001.01.01'
           },{
             address: 'sadaslkjfndjfnsdkjfnsjfnsdjfnsdj',
             player1: 'asdjasnfkjsadnfjasdnfjsdfnsdajfdnasdkj,',
             value: '0.001',
             createdDate: '2001.01.01'
           },{
             address: 'sadaslkjfndjfnsdkjfnsjfnsdjfnsdj',
             player1: 'asdjasnfkjsadnfjasdnfjsdfnsdajfdnasdkj,',
             value: '0.001',
             createdDate: '2001.01.01'
           }
           ]       
        }
      },
      methods : {      
        increment(){
            simpleContract.increment(10,function(a,b){
              console.log('incremetn callback');              
              console.log(a);
              console.log(b);              
            });
        },
        getValue(){
            simpleContract.getValue(function(a,b){
               console.log('getValue callback');              
              console.log(a);
              console.log(b);  
            });
        },
        readEvents(){

        }
      }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
