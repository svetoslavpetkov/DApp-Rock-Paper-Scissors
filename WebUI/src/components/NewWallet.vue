<template>
    <article>
        <h1>Create new wallet</h1>
        <div class="container">
            <div class="row">
                <div class="col">
                    Enter wallet name
                </div>
                <div class="col">
                    <input type="text" placeholder="enter wallet name" v-model="name" />
                </div> 
                <div class="col">
                    {{name}}
                </div>    
            </div>
            <div class="row">
                <div class="col">
                    Enter wallet random numbers
                </div>
                <div class="col">
                    <input type="text" placeholder="enter wallet name" v-model="seed" />
                </div> 
                <div class="col">
                    {{seed}}
                </div>    
            </div>
            <div class="row">
                <div class="col">
                    Enter wallet password
                </div>
                <div class="col">
                    <input type="password" placeholder="enter wallet name" v-model="password" />
                </div> 
                <div class="col">
                    {{password}}
                </div>    
            </div>
            <button type="button" v-if="!creationInProgress" v-on:click="createWallet" class="btn btn-primary btn-lg">Create</button>

            <button type="button" v-if="!creationInProgress" v-on:click="load" class="btn btn-primary btn-lg">Load</button>
            
            <div class="progress" v-else>
                <div class="progress-bar bg-success" role="progressbar"  :style="progressStyle" :aria-valuenow="progress" aria-valuemin="0" aria-valuemax="100"></div>
            </div>
            <h2>
                {{mnemonic}}
            </h2>
        </div>
    </article>    
</template>
<script>
import Wallet from '../../static/js/wallet'
import apiService from '../services/apiService'

export default {
    name: 'NewWallet',
    data (){
        return {
            name : '',
            seed : '',
            password : '',
            creationInProgress : false,
            progress: 25,
            progressStyle : 'width: 25%',
            mnemonic: ''
        }
    },
    methods: {
        createWallet : function(){
            this.creationInProgress = true;
            var data = this;
            Wallet.create(this.name, this.password, this.seed,function(mnemonic){
                data.mnemonic = mnemonic;
            });
        },
        load : function(){
            var data = this;
            Wallet.load(data.name,data.password, console.log);
        }
    }
};
</script>
