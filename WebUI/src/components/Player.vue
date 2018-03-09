<template>
  <div>
    <h1>Play game {{$route.params.playerAddress}}</h1>     
    <div v-if="isLoading" class="pageLoader"></div>
  </div>
</template>
<script>
import EthLightwallet from 'eth-lightwallet'
import Web3 from 'web3'
import toastr from 'toastr'
import apiService from '../services/apiService'


export default {
  name: 'TopScores',
  data () {
        return {            
            isLoading: true,
            players: []
        }
      },
      methods : {
        init(){
        }
    },
      created () {
        // var self = this;
        // this.isLoading = true;
        // apiService.get(this.$http, 'player', 'top').then(result => {
        //   self.players = result.body;
        //   this.isLoading = false;
        // }, error => {
        //   console.log(error)
        // });     
    }
}
</script>
