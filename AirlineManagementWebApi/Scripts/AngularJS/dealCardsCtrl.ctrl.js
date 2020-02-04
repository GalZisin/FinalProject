let dataApp = module.controller("dealCardsCtrl", DealCardsCtrl);


// // DI dependency injection - IOC
// function ParentCtrl($scope, $http, dataService, globalConstService, apiService) {

//     $scope.dataService = dataService
//     apiService.getFlights()
// }

// DI dependency injection - IOC
function DealCardsCtrl($scope, $http, dataService, globalConstService, apiService, $uibModal) {

  // $scope.dataService1 = dataService
  // apiService.getFlights()


  // apiService.getFlights(globalConstService.url).then(function(data){
  //     $scope.Flights = data;

  //pagination variables.
  $scope.flights = [];
  $scope.filteredFlights = [];
  $scope.currentPage = 1;
  $scope.numPerPage = 8;
  $scope.maxSize = 5;
  $scope.myImage = '/Photos/img3.jpg';


  apiService.getFlights(globalConstService.url).then(function (data) {


    $scope.flights = data;
    console.log($scope.flights);

    $scope.BigTotalItems = $scope.flights.length + 4;

    $scope.$watch('currentPage + numPerPage', updateFilteredItems);

    function updateFilteredItems() {
      let begin = (($scope.currentPage - 1) * $scope.numPerPage),
        end = begin + $scope.numPerPage;

      $scope.filteredFlights = $scope.flights.slice(begin, end);
      console.log($scope.filteredFlights);
    }

  })

}

//////Default Image/////////////////////////////////////////////////

dataApp.directive('fallbackSrc', function () {
  var fallbackSrc = {
    link: function postLink(scope, iElement, iAttrs) {
      iElement.bind('error', function () {
        angular.element(this).attr("src", iAttrs.fallbackSrc);
      });
    }
  }
  return fallbackSrc;
});
// $('myImage').on('error', function() {
//   this.src = ''
// });













//////Modal-template/////////////////////////////////////////////////

  // $scope.open = function() {
  //   var modalInstance =  $uibModal.open({
  //     templateUrl: "modalContent.html",
  //     controller: "ModalContentCtrl",
  //     size: '',
  //   });

  // modalInstance.result.then(function(response){
  //     $scope.result = `${response} button hitted`;
  // });

  // };



  // dataApp.controller('ModalCtrl', function($scope, $uibModal) {

//   $scope.open = function() {
//     var modalInstance =  $uibModal.open({
//       templateUrl: "modalContent.html",
//       controller: "ModalContentCtrl",
//       size: '',
//     });

    // modalInstance.result.then(function(response){
    //     $scope.result = `${response} button hitted`;
    // });

//   };
// })

// dataApp.controller('ModalContentCtrl', function($scope, $uibModalInstance) {

//   $scope.ok = function(){
//     $uibModalInstance.close("Ok");
//   }

//   $scope.cancel = function(){
//     $uibModalInstance.dismiss();
//   } 

// });