'use strict'

angular.module('myApp.bookShow', [
  'myApp.booksApi'
])
  .component('bookShow', {
    bindings: {
      $router: '<'
    },
    templateUrl: 'components/book-show/book-show.component.html',
    controller: function (booksApi) {

      this.$routerOnActivate = function (next /* [, prev] */) {
        booksApi.loadBook(next.params.isbn)
          // Orginaler Weg
          // .then(function (book) {
          //   this.book = book
          // }.bind(this))
          // ES2015 (aktueller Chrome, Edge oder FF)
          .then(book => this.book = book)
      }

    }
  })
