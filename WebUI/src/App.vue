<template>
  <div id="app">      
    <div class="container-fluid">
      <div>
            <img src="./assets/RPS.png" class="logo">
      </div>
      <nav class="navbar navbar-expand-lg navbar-light bg-light justify-content-between">
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarSupportedContent">
              <ul class="navbar-nav mr-auto container-fluid justify-content-center">
                  <li class="nav-item" :class="{ active: isActive('home') }">
                      <router-link class="nav-link" href="#" to="/">Home</router-link>
                  </li>
                   <li class="nav-item" :class="{ active: isActive('new-game') }">
                      <router-link class="nav-link" href="#" to="/new-game">NewGame</router-link>
                  </li>
                  <li class="nav-item" :class="{ active: isActive('top-scores') }">
                      <router-link class="nav-link" href="#" to="/top-scores">Top scores</router-link>
                  </li>
                  <li class="nav-item" :class="{ active: isActive('account') }">
                      <router-link class="nav-link" href="#" to="/Account">MyGames</router-link>
                  </li>
                  <li class="nav-item ellipsis">
                      <router-link class="nav-link" href="#" :to="{ name: 'player', params: { playerAddress: currentAccount }}">{{currentAccount}}</router-link>                      
                  </li>
              </ul>
          </div>
      </nav>
  </div>
  <div class="app-container container">
      <router-view></router-view>
  </div>
  </div>
</template>

<script>
export default {
  name: 'App',
    methods: {
    isActive (name) {
      return name === this.$route.name
    }
    },
    data(){ 
        return {
            currentAccount: ''    
        }  
    },
    created(){             
        var self = this;
        self.currentAccount = web3.eth.defaultAccount ;
        setInterval(function() {                                 
            if (web3.eth.defaultAccount !== self.currentAccount) {
              self.currentAccount = web3.eth.defaultAccount;       
              console.log('changed');                     
            }
          }, 1000);
    }
  }
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
