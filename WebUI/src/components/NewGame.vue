<template>
  <div>
    <h1>Create new game</h1>
    <div v-for="move in moves" v-bind:key="move.id">
      <label v-for="optionalMove in optionalMoves" v-bind:key="optionalMove.value">
        <input type="radio" v-bind:value="optionalMove.value" v-model="move.selecetedMove"/>
        {{optionalMove.name}}
      </label>
      <div>selcted option {{move.selecetedMove}}</div>
    </div>
    

    <button class="btn btn-primary" v-on:click="createGame" >CreateGame</button>
    <div v-if="gameCreated" class="alert alert-success" role="alert">
      Game created!
    </div>
  </div>
</template>
<script>
import EthLightwallet from 'eth-lightwallet'
import Web3 from 'web3'
import toastr from 'toastr'


export default {
  name: 'NewGame',
  data () {
        return {
            value: '0.1',
            moves:[ 
              {id:0,selecetedMove:''},
              {id:1,selecetedMove:''},
              {id:2,selecetedMove:''},
              {id:3,selecetedMove:''},
              {id:4,selecetedMove:''}
            ],
            optionalMoves: [
              {name:'Rock' ,value: 0 },
              {name:'Paper' ,value: 1 },
              {name:'Scissors' ,value: 2 }
            ],
            selectedOption: '',
            gameCreated: false
        }
      },
      methods : {
        createGame(){
            console.log(this.moves[0].selecetedMove);
            this.gameCreated = true;
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
