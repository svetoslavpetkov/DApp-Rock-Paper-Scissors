<template>
  <div>
    <h1>Game # {{$route.params.gameid}}</h1>     
    <h2 v-if="game.status == 0" class="started">
        Started
    </h2>
    <h2 v-if="game.status == 1" class="completed">
        Completed
    </h2>
    <div v-if="isLoading" class="pageLoader"></div>
    <div class="row">
        <div class="col-md-5">
            <h3 :class="{ winner: game.winner == 1 }">Player 1 <span v-if="game.winner == 1" >is the winner</span></h3>
            <router-link class="nav-link" href="#" :to="{ name: 'player', params: { playerAddress: game.player1 }}">{{game.player1}}</router-link>
        </div>
        <div class="col-md-5">
            <h3 :class="{ winner: game.winner == 2 }">Player 2 <span v-if="game.winner == 2" >is the winner</span></h3>
            <router-link class="nav-link" href="#" :to="{ name: 'player', params: { playerAddress: game.player2 }}">{{game.player2}}</router-link>
        </div>
    </div>
    <div class="row" style="padding:10px" v-for="move in moves"  v-bind:key="move.moveid">
        <div class="col-md-5">  
            <img class="moveImage float-right" v-bind:class="move.p1class" /> 
        </div>
        <div class="col-md-5">
            <img class="moveImage" v-bind:class="move.p2class" />  
        </div>
    </div>
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
            game:{

            },
            moves: []
        }
      },
      methods : {
        init(){
        },
        getMoveClass(moveid){
            if(moveid == 0){
                return 'rock';
            }
            else if(moveid == 1){
                return 'paper';
            }
            return 'scissors';            
        }
    },
      created () {
        var self = this;
        this.isLoading = true;
        apiService.get(this.$http, 'game', '' + self.$route.params.gameid).then(result => {
          self.game = result.body;
          console.log(self.game.player1Moves.length);
          for(var i=0; i< self.game.player1Moves.length;i++){
            var p1 = self.getMoveClass(self.game.player1Moves[i]);
            var p2 = self.getMoveClass(self.game.player2Moves[i]);            
            self.moves.push({moveid :i ,p1class : p1, p2class:p2});
          }

          this.isLoading = false;
        }, error => {
          console.log(error)
        });     
    }
}
</script>
