'use strict'

angular.module('myApp.colorpicker', [])
  .component('colorpicker', {
    bindings: {
      red: '@',
      green: '@',
      blue: '@',
      alpha: '='
    },
    templateUrl: 'components/colorpicker/colorpicker.component.html',
    controller: function () {
      // `this` ist im template `$ctrl`

      // Default Values
      this.red = 0
      this.green = 255
      this.blue = 0
      this.alpha = Math.random()
    }
  })
