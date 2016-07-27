'use strict'

angular.module('myApp.book', [
  'myApp.bookList',
  'myApp.bookShow',
  'myApp.bookEdit'
])
  .component('book', {
    template: `
    <h1>Boooooooks...</h1>
    <ng-outlet></ng-outlet>
    `,
    $routeConfig: [
      {path: '/', name: 'BookList', component: 'bookList'},
      {path: '/:isbn', name: 'BookShow', component: 'bookShow'},
      {path: '/:isbn/edit', name: 'BookEdit', component: 'bookEdit'}
    ]
  })
