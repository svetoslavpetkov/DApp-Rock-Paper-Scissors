import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import NewWallet from '@/components/NewWallet'
import NewGame from '@/components/NewGame'


Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/new-game',
      name: 'new-game',
      component: NewGame
    }
  ]
})
