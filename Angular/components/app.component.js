'use strict'

angular.module('myApp', [
  // External
  'ngComponentRouter',

  // Internal
  'myApp.colorpicker',
  'myApp.book'
])
  .value('$routerRootComponent', 'myApp')
  .component('myApp', {
    templateUrl: 'components/app.component.html',
    $routeConfig: [
      {path: '/books/...', component: 'book', useAsDefault: true}
    ]
  })
