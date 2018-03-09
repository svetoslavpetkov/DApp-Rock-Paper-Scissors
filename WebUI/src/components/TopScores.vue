<template>
  <div>
    <h1>Top players</h1>    
    <table class="table">
        <thead class="thead-light">
            <tr>
                <th scope="col">#</th>
                <th scope="col">Player</th>
                <th scope="col">Wins</th>
                <th scope="col">Losses</th>
                <th scope="col">Wins vs Losses</th>            
            </tr>
        </thead>
        <tbody>
            <tr v-for="(player, index) in players" v-bind:key="player.address">
                <th scope="row">{{index+1}}</th>
                <td>{{player.address}}</td>
                <td>{{player.wins}}</td>
                <td>{{player.losses}}</td>
                <td>{{player.winsOverLosses}}</td>
            </tr>
        </tbody>
    </table>
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
            isLoading: false,
            players: []
        }
      },
      methods : {
        init(){
        }
    },
      created () {
        var self = this;
        this.isLoading = true;
        apiService.get(this.$http, 'player', 'top').then(result => {
          self.players = result.body;
          this.isLoading = false;
        }, error => {
          console.log(error)
        });     
    }
}
</script>
