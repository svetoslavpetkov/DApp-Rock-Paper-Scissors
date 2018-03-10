<template>
  <div>
    <h1>Play game {{$route.params.gameid}}</h1>
    <h3>Other player:
       <router-link class="nav-link" href="#" :to="{ name: 'player', params: { playerAddress: game.player1 }}">{{game.player1}}</router-link>   
    </h3>  
    <div v-if="curentUserAccount == game.player1" class="alert alert-danger" role="alert">
      <h3>You are going to play against game created by yourself</h3>
    </div>
    <hr />
    <div class="row moveRow" v-for="move in moves" v-bind:key="move.id">
      <div class="col-md-3">
        <h3>
          Enter move {{move.id}}
        </h3>
      </div>
      <div class="col-md-6">
        <span  v-for="optionalMove in optionalMoves" v-bind:key="optionalMove.value">
          <label style="display:none;">
            <input type="radio" v-bind:value="optionalMove.value" v-model="move.selecetedMove"/>
            {{optionalMove.name}}
          </label>                   
            <img class="moveImage" v-bind:class="[move.selecetedMove ==  optionalMove.value ? optionalMove.activeClass : optionalMove.inactiveClass]" v-on:click="move.selecetedMove = optionalMove.value" />          
        </span>
      </div>
    </div>
    
    <div  v-if="isLoading">
        Creating contract
    </div>
    <button v-if="!isLoading" class="btn btn-lg btn-primary" v-on:click="createGame" >Play the game</button>
    <div v-if="gameCreated" class="alert alert-success" role="alert">
      Game played see result <router-link herclass="nav-link" href="#" :to="{ name: 'game', params: { gameid: $route.params.gameid }}">gameid #{{$route.params.gameid}}</router-link>
    </div>
    <div v-if="hasError" class="alert alert-danger" role="alert">
      {{errorText}}
    </div>

  </div>
</template>
<script>
import EthLightwallet from 'eth-lightwallet'
import Web3 from 'web3'
import toastr from 'toastr'
import apiService from '../services/apiService'


export default {
  name: 'PlayGame',
  data () {
        return {
            value: '0.1',
            contractInstance : {},
            moves:[ 
              {id:0,selecetedMove:0},
              {id:1,selecetedMove:0},
              {id:2,selecetedMove:0}
            ],
            optionalMoves: [
              {name:'Rock' ,value: 0, activeClass: 'rock', inactiveClass: 'rock-inactive' },
              {name:'Paper' ,value: 1 , activeClass: 'paper', inactiveClass: 'paper-inactive'},
              {name:'Scissors' ,value: 2 , activeClass: 'scissors', inactiveClass: 'scissors-inactive'}
            ],
            selectedOption: '',
            gameCreated: false,
            hasError: false,
            errorText: '',
            isLoading: false,
            isActive: false,
            game: {},
            curentUserAccount: ''
        }
      },
      methods : {
        createGame(){
            console.log(this.moves[0].selecetedMove);            
            var self = this;
            self.isLoading = true;
            this.hasError = false;
            self.gameCreated = false;             
            this.contractInstance.acceptGameRequest(this.$route.params.gameid,this.moves[0].selecetedMove,this.moves[1].selecetedMove,this.moves[2].selecetedMove
              ,{from: web3.eth.defaultAccount, gas: 3000000, value: web3.toWei(1000, 'finney')},function(error,result){
                console.log(error);
                if(error){
                    var trimlength = error.message.length < 100 ? error.message.length : 100;
                    self.errorText = error.message.substring(0,trimlength);
                    self.hasError = true;
                }
                else{
                  self.gameCreated = true;         
                }
                self.isLoading = false;
            });
        },
        initGameData(){
            apiService.get(this.$http, 'game', 'created/'+ this.$route.params.gameid).then(result => {
               this.game = result.body;
            }, error => {
              console.log(error)
            });
        }
    },
      created () {
        var self = this;
        apiService.get(this.$http, 'contract', 'metadata').then(result => {
          let contractMetadata = result.body;
          let contract = web3.eth.contract(JSON.parse(contractMetadata.abi));
          self.contractInstance = contract.at(contractMetadata.address);
        }, error => {
          console.log(error)
        });     
        this.initGameData();  
        this.curentUserAccount =  web3.eth.accounts[0];
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
</style>
