let dataApp = module.controller("dealCardsCtrl", DealCardsCtrl);
let myuser;
// DI dependency injection - IOC
function DealCardsCtrl($window, $rootScope, $scope, $http, dataService, globalConstService, apiService, $uibModal) {

    //let token = '';
    $scope.firstName = '';
    $scope.localCountryName = '';
    $scope.isLogedIn = false;
    $scope.isError = false;

    $scope.getLocalCountry = () => {
        $http({
            method: 'GET',
            url: 'https://ipapi.co/json/',
            data: 'parameters'
        }).then(function success(response) {
            //$scope.localCountryName = response.data.country_name;
            $scope.localCountryName = response.data.country_name;
            //console.log(" $scope.localCountryName: " + JSON.stringify($scope.localCountryName));
            console.log(" localCountryName: " + JSON.stringify($scope.localCountryName));
            $scope.ServiesGetFlights(response.data.country_name);
        }, function error(response) {
            console.log("failed to load local country: " + JSON.stringify(response))
        });
    }
    //CustomerFacade / GetCustomerFirstName
    $scope.GetFirstName = function () {
        let token = '';
        let url = '';
        let adminToken = localStorage.getItem('admin');
        let customerToken = localStorage.getItem('customer');
        let companyToken = localStorage.getItem('airlineCompany');

        console.log("adminToken: " + adminToken)
        console.log("customerToken: " + customerToken)
        console.log("companyToken: " + companyToken)


        if (adminToken !== null && adminToken !== '') {
            token = adminToken;
            url = '/api/AdministratorFacade/getAdministratorFirstName';
        }
        else if (customerToken !== null && customerToken !== '') {
            token = customerToken;
            url = '/api/CustomerFacade/getCustomerFirstName';
        }
        else if (companyToken !== null && companyToken !== '') {
            token = companyToken
            url = '/api/AirlineCompanyFacade/getAirlineCompanyName';
        }
        else {
            return;
        }
        var path = url;
        console.log("path: " + path)
        // $http.defaults.headers.common.Authorization = 'Bearer ' + token;
        return $http({
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                "Authorization": 'Bearer ' + token
            },
            url: path,
            // data: token
        }).then(function (data) {
            if (data != '') {
                console.log("my data: " + data.data);
                $scope.firstName = data.data
                $scope.isLogedIn = !$scope.isLogedIn;
            }
            else {
                $scope.isLogedIn = false;
            }
        }, function (error) {
            console.log(error);
            if (error.message === 'Request failed with status code 401') {
                localStorage.removeItem('admin');
                localStorage.removeItem('airlineCompany');
                localStorage.removeItem('customer');
                alert("Your token expires, please re - login!");
                $scope.isLogedIn = false;
                $location.path('/FlightDeals')
                $location.replace();
            }
        });
    }

    $scope.ServiesGetFlights = (localCountryName) => {


        let url = globalConstService.url + localCountryName;
        console.log("my url" + url)
        apiService.getFlights(url).then(function (data) {

            $scope.flights = data;
            console.log($scope.flights);

            $scope.BigTotalItems = $scope.flights.length + 5;

            $scope.$watch('currentPage + numPerPage', updateFilteredItems);

            function updateFilteredItems() {
                let begin = (($scope.currentPage - 1) * $scope.numPerPage),
                    end = begin + $scope.numPerPage;

                $scope.filteredFlights = $scope.flights.slice(begin, end);
                console.log($scope.filteredFlights);
            }

        })
    }

    $scope.init = function () {
        $scope.getLocalCountry();
        $scope.GetFirstName();
    }

    $scope.init();

    //pagination variables.
    $scope.flights = [];
    $scope.filteredFlights = [];
    $scope.currentPage = 1;
    $scope.numPerPage = 4;
    $scope.maxSize = 5;
    $scope.myImage = '/Photos/img3.jpg';






    $scope.onLogin2 = function () {
        console.log("$scope.isLogedIn: " + $scope.isLogedIn);
        if ($scope.isLogedIn === true) {
            localStorage.removeItem('admin');
            localStorage.removeItem('airlineCompany');
            localStorage.removeItem('customer');
            $scope.isLogedIn = !$scope.isLogedIn;
        }
        else {
            $scope.GetFirstName();
            //$scope.isLogedIn = !$scope.isLogedIn;

        }

        //$scope.init();

    }



    // dataApp.controller("LoginCtrl", LoginCtrlCtor);

    $scope.formModelLogin = {};
    // let userName = $scope.formModelLogin.username;
    ////Login/////
    $scope.onLogin = function (valid) {

        if (valid) {
            console.log("Hey i'm submitted!");
            console.log($scope.formModelLogin);
            let obj = {};
            $http.post('/api/Login/UserLogin', $scope.formModelLogin)     //'Access-Control-Allow-Origin: *'
                .then(function (res) {
                    console.log("res data: " + res);
                    obj = JSON.parse(JSON.stringify(res.data));
                    console.log("type: " + obj.type)
                    console.log("token: " + obj.token)
                    console.log("name: " + obj.name)
                    //store user details and jwt token in local storage to keep user logged in between page refreshes
                    if (obj.type === 'Administrator') {
                        localStorage.setItem('admin', obj.token);
                    }
                    else if (obj.type === 'AirlineCompany') {
                        localStorage.setItem('airlineCompany', obj.token);
                    }
                    else if (obj.type === 'Customer') {
                        localStorage.setItem('customer', obj.token);
                    }
                    $scope.isLogedIn = !$scope.isLogedIn;
                    $("#myButton").click();
                    // $scope.GetFirstName(false);
                    console.log(":)")
                    $('#myLoginModal').modal('hide');

                    $("#myLoginModal").on("hidden.bs.modal", function () {
                        $("form input[type='text'],input[type='password']").val('');
                        $scope.isError = false;
                    });

                }, function error(response) {
                    console.log(":(");
                    console.log("response" + JSON.stringify(response));
                    obj = JSON.parse(JSON.stringify(response));
                   
                    console.log("response.message: " + obj.statusText);
                    if (obj.statusText === "Unauthorized") {
                        console.log("$scope.isError = true")
                        $scope.isError = true;
                    }
                });

        }
        else {
            console.log("I'm not valid!");
        }

    };


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