'use strict'

describe('Service: booksApi', function () {

  beforeEach(module('myApp.booksApi'))

  it('should work', function () {
    expect(true).toBeTruthy()
  })

  it('should load the booksApi service', inject(function (booksApi) {
    expect(booksApi).toBeTruthy()
    expect(typeof booksApi).toBe('object')
  }))

  describe('with a public API', function () {
    it('should have a loadBook method', inject(function (booksApi) {
      expect(typeof booksApi.loadBook).toBe('function')
    }))
    // ...
  })

  describe('with a `loadBook(isbn)` method', function () {

    var testBook

    beforeEach(inject(function ($httpBackend, booksApiBaseUrl) {
      testBook = {
          isbn: '123-456-789',
          title: 'Test-Book'
        }
      $httpBackend.whenGET(booksApiBaseUrl + '/123-456-789')
        .respond(testBook)
    }))

    afterEach(inject(function ($httpBackend) {
      $httpBackend.verifyNoOutstandingExpectation()
      $httpBackend.verifyNoOutstandingRequest()
    }))

    it('should return a promise', inject(function (booksApi, $httpBackend) {
      var promise = booksApi.loadBook('123-456-789')
      $httpBackend.flush()
      expect(typeof promise.then).toBe('function')
    }))

    it('should send an HTTP GET to `baseUrl/books/:isbn`', inject(function (booksApi, booksApiBaseUrl, $httpBackend) {
      $httpBackend.expectGET(booksApiBaseUrl + '/123-456-789')
      booksApi.loadBook('123-456-789')
      $httpBackend.flush()
    }))

    it('should return a promise to return a book',
      inject(function (booksApi, booksApiBaseUrl, $httpBackend) {
        var book
        var promise = booksApi.loadBook('123-456-789')
        promise.then(function (_book_) {
          book = _book_
        })
        $httpBackend.flush()
        expect(book).toEqual(testBook)
      })
    )
  })

})
