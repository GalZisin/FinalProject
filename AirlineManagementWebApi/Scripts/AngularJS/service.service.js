
//Service - constractor
angular.module('dataApp').service('apiService', ['$http', function ($http) {

    // expose a getFlights function from your service
    // that takes a flight object
    this.getFlights = function (url) {
        console.log("url: " + url);
        // return a Promise object so that the caller can handle success/failure
        return $http.get(url).then(function (response) {
            return response.data;
        },
            //error
            (err) => {
                alert('error')
                console.log(err)
            }
        );
    }
}]);

//Service - factory

// angular.module("dataApp").factory("apiService", function($http,globalConstService) {
//     return {
//       getFlights: function() {
//             return $http.get(globalConstService.url).then(function(response) {
//                 return response.data;
//             },
//             //error
//           (err) => {
//             alert('error')
//             console.log(err)
//           }
//              );
//         }
//     }
// })



// module.service("apiService", function (globalConstService, dataService, $http) {

//     this.getFlights = () => {
//       $http.get(globalConstService.url)
//         .then(

//           // success
//           (resp) => {

//             dataService.flights = resp.data
//             console.log(resp.data)
//           },
//           // error
//           (err) => {
//             alert('error')
//             console.log(err)
//           }
//         )
//     }

//   })





