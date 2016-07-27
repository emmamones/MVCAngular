'use strict'

function bookEditController (booksApi) {
  this.$routerOnActivate = function (next) {
    booksApi.loadBook(next.params.isbn)
      .then(book => this.book = book)
  }

  this.handleSubmit = function (book) {
    booksApi.saveBook(book)
      .then(function () {
        this.$router.navigate(['BookShow', {isbn: book.isbn}])
      }.bind(this))
  }
}

angular.module('myApp.bookEdit', [
  'myApp.booksApi'
])
  .component('bookEdit', {
    bindings: {
      $router: '<'
    },
    templateUrl: 'components/book-edit/book-edit.component.html',
    controller: bookEditController
  })
