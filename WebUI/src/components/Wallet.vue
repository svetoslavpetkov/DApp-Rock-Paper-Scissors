<template>
  <div>
    <h1>{{ msg }}</h1>
    <h2>Essential Links</h2>
    

    <button v-on:click="createVault" >Create wallet</button>
  </div>
</template>
<script>
import EthLightwallet from 'eth-lightwallet'
import Web3 from 'web3'

export default {
  name: 'Wallet',
  data () {
        return {
          //try to load wallet
          userName: 'Svetoslav Petkov',
          address : '0xasfsadfadsfasd'
        }
      },
      methods : {
        createVault : function(){
          var password = prompt('Enter password for encryption', 'password');

          document.lightwallet = EthLightwallet;
          
          document.lightwallet.keystore.createVault({
            password: password,
            // seedPhrase: seedPhrase, // Optionally provide a 12-word seed phrase
            // salt: fixture.salt,     // Optionally provide a salt.
                                      // A unique salt will be generated otherwise.
            // hdPathString: hdPath    // Optional custom HD Path String
          }, function (err, ks) {
            
            console.log(err);

            // Some methods will require providing the `pwDerivedKey`,
            // Allowing you to only decrypt private keys on an as-needed basis.
            // You can generate that value with this convenient method:
            ks.keyFromPassword(password, function (err, pwDerivedKey) {
              if (err) throw err;

              // generate five new address/private key pairs
              // the corresponding private keys are also encrypted
              ks.generateNewAddress(pwDerivedKey, 5);
              var addr = ks.getAddresses();

              ks.passwordProvider = function (callback) {
                var pw = prompt("Please enter password", "Password");
                callback(null, pw);
              };

              // Now set ks as transaction_signer in the hooked web3 provider
              // and you can start using web3 using the keys/addresses in ks!
            });
          });
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
