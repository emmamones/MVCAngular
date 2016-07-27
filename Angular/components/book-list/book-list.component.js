'use strict'

angular.module('myApp.bookList', [
  'myApp.booksApi'
])
  .component('bookList', {
    templateUrl: 'components/book-list/book-list.component.html',
    controller: function (booksApi) {
      booksApi.loadBooks()
        .then(books => this.books = books) // ES2015!!!!!
        // Im IE11:
        // .then(function (books) {
        //  this.books = books
        // }.bind(this))
    }
  })
