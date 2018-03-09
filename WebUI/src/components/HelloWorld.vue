<template>
  <article>
        <div>        
            <h1>Pending games</h1>
            <div v-if="isLoading" class="pageLoader"></div>            
            <div class="row">
              <div class="col-sm-6 game" v-for="game in games" v-bind:key="game.gameID">
                <div class="card">
                  <div class="card-header">GameID: <strong>{{ game.gameID }}</strong></div>
                  <div class="card-body">
                    <p class="card-text">
                      Owner: 
                      <router-link class="nav-link" href="#" :to="{ name: 'player', params: { playerAddress: game.player1 }}">{{game.player1}}</router-link>   
                    </p>
                    <p class="card-text">Value: {{ game.value | toEthers }} eth</p>
                    <p class="card-text">Create date: {{ game.createdDate }}</p>                                      
                  </div>
                  <div class="card-footer">                    
                    <router-link class="btn btn-primary" :to="{ name: 'play-game', params: { gameid: game.gameID }}"><span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>Play game</router-link>
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
import apiService from '../services/apiService'

export default {
  name: 'HelloWorld',
  data () {
        return {
            contractInstance : {},
            games : [],
           isLoading : true
        }
      },
  methods : {      
        loadOpenGames(){
          var self = this;
            apiService.get(this.$http, 'game', 'open').then(result => {
               self.games = result.body;
               self.isLoading = false;
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
            this.loadOpenGames();
  },
  filters: {
    toEthers: function (value) {
      if (!value) return ''      
      return web3.fromWei(value, 'ether')
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
</style>
