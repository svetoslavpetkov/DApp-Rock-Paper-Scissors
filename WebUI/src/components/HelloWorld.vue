<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <router-link :to="{ name: 'new-wallet'}">NewWallet</router-link>
    <button v-on:click="createVault" >Create wallet</button>

    <h1>Address</h1>
  </div>
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

export default {
  name: 'HelloWorld',
  data () {
        document.aaa = new Web3(new Web3.providers.HttpProvider('https://ropsten.infura.io/AH6vEglIKyYCdZcnF3jE'));
        console.log(document.aaa);        
        console.log(document.aaa.eth.accounts);
        return {
          msg: Wallet.getMessage(),
          addresses : [],
          keyStore : {}          
        }
      },
      methods : {      
        createVault : function(){
          var password = prompt('Enter password for encryption', 'password');

          Wallet.create(password,'seed',function(msg){ alert(msg);});

/*
          var lightwallet = EthLightwallet;
          
          var randomSeed = EthLightwallet.keystore.generateRandomSeed('home thick');

          EthLightwallet.keystore.createVault({
            password: password,
            seedPhrase: randomSeed, // Optionally provide a 12-word seed phrase
            // salt: fixture.salt,     // Optionally provide a salt.
                                      // A unique salt will be generated otherwise.
             hdPathString: "m/44'/60'/0'/0"    // Optional custom HD Path String
          }, function (err, ks) {
            

            console.log(err);
            console.log(ks);
            // Some methods will require providing the `pwDerivedKey`,
            // Allowing you to only decrypt private keys on an as-needed basis.
            // You can generate that value with this convenient method:
            ks.keyFromPassword(password, function (err, pwDerivedKey) {
              if (err) throw err;

              // generate five new address/private key pairs
              // the corresponding private keys are also encrypted
              ks.generateNewAddress(pwDerivedKey, 5);
              var addr = ks.getAddresses();

              console.log(addr);              
              console.log(ks);
              
              //this.addresses = addr;

              ks.passwordProvider = function (callback) {
                var pw = prompt("Please enter password", "Password");
                callback(null, pw);
              };

              console.log(ks.serialize());
              // Now set ks as transaction_signer in the hooked web3 provider
              // and you can start using web3 using the keys/addresses in ks!
            });
          });*/
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
