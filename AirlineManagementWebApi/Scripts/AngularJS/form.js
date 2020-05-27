dataApp = module.controller("dealCardsCtrl", DealCardsCtrl);



//////Post form data/////////////////////////////////////////////////

dataApp.controller("RegisterCtrl", RegisterCtrlCtor);

function RegisterCtrlCtor($scope, $http) {
  $scope.formModel = {};
  ////Register/////
  $scope.onSubmit = function (valid) {
    if (valid) {
      console.log("Hey i'm submitted!");
      console.log($scope.formModel);
        $http.post('/api/AnonymousFacade/register', $scope.formModel      //'Access-Control-Allow-Origin: *'
      ).then(function (data) {
        console.log(":)")
        $('#myCustomerModal').modal('hide');
      },
        function (data) {
          console.log(":(")
        });
    }
    else {
      console.log("I'm not valid!");
    }
  };
}





//////Clear customer Form/////////////////////////////////////////////////
dataApp.controller('CustomerFormController', function ($scope) {
  $scope.reset = function () {
    $scope.formModel.firstName = "";
    $scope.formModel.lastName = "";
    $scope.formModel.username = "";
    $scope.formModel.email = "";
    $scope.formModel.phoneNumber = "";
    $scope.formModel.password = "";
    $scope.formModel.confirm_password = "";
  }
});

//////Clear company Form/////////////////////////////////////////////////
dataApp.controller('CompanyFormController', function ($scope) {
  $scope.reset = function () {
    $scope.formModel.companyName = "";
    $scope.formModel.username = "";
    $scope.formModel.email = "";
    $scope.formModel.phoneNumber = "";
    $scope.formModel.password = "";
    $scope.formModel.confirm_password = "";
  }
});

//////Create derective comperTo - password confirm/////////////////////////////////////////////////
let compareTo = function () {
  return {
    require: "ngModel",
    scope: {
      otherModelValue: "=compareTo"
    },
    link: function (scope, element, attributes, ngModel) {

      ngModel.$validators.compareTo = function (modelValue) {
        return modelValue == scope.otherModelValue;
      };

      scope.$watch("otherModelValue", function () {
        ngModel.$validate();
      });
    }
  };
};

dataApp.directive("compareTo", compareTo);


//////show-hide password/////////////////////////////////////////////////
$(document).ready(function () {
  $("#show_hide_password #k").on('click', function (event) {
    event.preventDefault();
    if ($('#show_hide_password input').attr("type") == "text") {
      $('#show_hide_password input').attr('type', 'password');
      $('#show_hide_password #pp').addClass("fa-eye-slash");
      $('#show_hide_password #pp').removeClass("fa-eye");
    } else if ($('#show_hide_password input').attr("type") == "password") {
      $('#show_hide_password input').attr('type', 'text');
      $('#show_hide_password #pp').removeClass("fa-eye-slash");
      $('#show_hide_password #pp').addClass("fa-eye");
    }
  });
});


//////Login/////////////////////////////////////////////////////////////
// $('.form').find('input, textarea').on('keyup blur focus', function (e) {

//   var $this = $(this),
//       label = $this.prev('label');

// 	  if (e.type === 'keyup') {
// 			if ($this.val() === '') {
//           label.removeClass('active highlight');
//         } else {
//           label.addClass('active highlight');
//         }
//     } else if (e.type === 'blur') {
//     	if( $this.val() === '' ) {
//     		label.removeClass('active highlight'); 
// 			} else {
// 		    label.removeClass('highlight');   
// 			}   
//     } else if (e.type === 'focus') {

//       if( $this.val() === '' ) {
//     		label.removeClass('highlight'); 
// 			} 
//       else if( $this.val() !== '' ) {
// 		    label.addClass('highlight');
// 			}
//     }

// });

// $('.tab a').on('click', function (e) {

//   e.preventDefault();

//   $(this).parent().addClass('active');
//   $(this).parent().siblings().removeClass('active');

//   target = $(this).attr('href');

//   $('.tab-content > div').not(target).hide();

//   $(target).fadeIn(600);

// });























  // function validate(){
  //   var  validationField = document.getElementById('validation-txt');
  //   var  password= document.getElementById('password-2');

  //   var content = password.value;
  //   var  errors = [];
  //   console.log(content);
  //   if (content.length < 8) {
  //     errors.push("Your password must be at least 8 characters"); 
  //   }
  //   if (content.search(/[a-z]/i) < 0) {
  //     errors.push("Your password must contain at least one letter.");

  //   }
  //   if (content.search(/[0-9]/i) < 0) {
  //     errors.push("Your password must contain at least one digit."); 

  //   }
  //   if (errors.length > 0) {
  //     validationField.innerHTML = errors.join('');

  //     return false;
  //   }
  //     validationField.innerHTML = errors.join('');
  //     return true;

  //   }