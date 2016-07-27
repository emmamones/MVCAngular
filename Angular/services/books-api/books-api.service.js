'use strict'

angular.module('myApp.booksApi', [])
  .value('booksApiBaseUrl', 'http://bookmonkey-api.angularjs.de/books')
  .factory('booksApi', function ($http, booksApiBaseUrl) {

    return {
      loadBook: function (isbn) {
        return $http.get(booksApiBaseUrl + '/' + isbn)
        // return $http.get(`${booksApiBaseUrl}/${isbn}`)
          .then(function (response) {
            return response.data
          })
      },
      loadBooks: function () {
        return $http.get(booksApiBaseUrl)
          // Same as above, but in ES2015 (Chrome, FF or Edge)
          .then(response => response.data)
          // Or, in ES5 or below
          // .then(function (response) {
          //   return response.data
          // })
      },
      saveBook: function (book) {
        return $http.put(booksApiBaseUrl + '/' + book.isbn, book)
          .then(response => response.data)
      }
    }

  })
