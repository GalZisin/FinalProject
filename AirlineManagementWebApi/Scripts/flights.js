$(document).ready(function () {
    console.log("document loaded");
    
    getPagination('#flights');
    
});


let scheduledDepartureDate = '';
let scheduledDepartureTime = '';
let estimatedDepartureTime = '';
let scheduledLandingDate = '';
let scheduledLandingTime = '';
let estimatedLandingTime = '';
let statusColor = '';
let btnState = 0;

function Clear() {
    document.getElementById('multiSelectFieldFlightIds').selectedIndex = 0;
    document.getElementById('multiSelectFieldCompanies').selectedIndex = 0;
    document.getElementById('multiSelectFieldCountries').selectedIndex = 0;
}
function GetDepartureStatusColor(status) {
    if (status == 'ON TIME') {
        statusColor = 'squareLightgreen';
    }
    else {
        statusColor = 'squareLightcoral';
    }
    return statusColor;
}
//function DeparturesClick() {
//    if ($('#BtnDepatures').hasClass('flightBtnShadow')) {
//        $('#BtnDepatures').removeClass('flightBtnShadow')
//    }
//    else {
//        $('#BtnDepatures').addClass('flightBtnShadow')
//    }
//}
//function ArrivalsClick() {
//    if ($('#BtnArrivals').hasClass('flightBtnShadow')) {
//        $('#BtnArrivals').removeClass('flightBtnShadow')
//    }
//    else {
//        $('#BtnArrivals').addClass('flightBtnShadow')
//    }
//}
//function GetBtnState() {
//    if (!$('#BtnDepatures').hasClass('flightBtnShadow') && $('#BtnArrivals').hasClass('flightBtnShadow')) {
//        btnState = 1;
//    }
//    else if (!$('#BtnArrivals').hasClass('flightBtnShadow') && $('#BtnDepatures').hasClass('flightBtnShadow')) {
//        btnState = 2;
//    }
//    else if (!$('#BtnArrivals').hasClass('flightBtnShadow') && !$('#BtnDepatures').hasClass('flightBtnShadow')) {
//        btnState = 3;
//    }
//    else {
//        btnState = 0;
//    }
//}
//function getFlightsStateJQ() {

//    GetBtnState();
//    if (btnState == 1) {
//        let URL = `http://localhost:57588/api/AnonymousFacade/alldepartureflights`
//        $.ajax({

//            url: URL
//        }).then((data) => {

//            let statusColor = '';
//            let $flight_table = $("#flights").empty();
//            $flight_table.append(`  <tr>
//                                    <th></th>
//                                    <th>Flight</th>
//                                    <th>Departing from</th>
//                                    <th>Destination</th>
//                                    <th>Scheduled time</th>
//                                    <th>Estimated time</th>
//                                    <th>Status</th></tr>`);

//            $.each(data, function (i, flight) {
//                if (data.length > 0) {
//                    statusColor = GetStatusColor(flight.Status);
//                    scheduledDepartureDate = dateFormat(flight.DEPARTURE_TIME, 'dd/mm')

//                    scheduledDepartureTime = dateFormat(flight.DEPARTURE_TIME, 'HH:MM:ss')

//                    estimatedDepartureTime = dateFormat(flight.REAL_DEPARTURE_TIME, 'HH:MM:ss')
//                }
//                //$flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td>${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${flight.DEPARTURE_TIME}</td><td class="square">${status}</td></tr>`)
//                $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${scheduledDepartureDate}&nbsp&nbsp&nbsp&nbsp${scheduledDepartureTime}</td><td>${estimatedDepartureTime}</td><td id="status" class="${statusColor}">${flight.Status}</td></tr>`)
//            })

//        }).fail(

//            // what to do on error
//            function (err) {
//                //console.error(err)
//            }
//        )
//    }
//    else if (btnState == 2) {
//        $.ajax({

//            url: `http://localhost:57588/api/AnonymousFacade/AllArrivalFlights/2`
//        }).then((data) => {
//            let $flight_table = $("#flights").empty();
//            $flight_table.append(`  <tr>
//                                    <th></th>
//                                    <th>Flight</th>
//                                    <th>Departing from</th>
//                                    <th>Destination</th>
//                                    <th>Landing time</th>
//                                    <th>Status</th></tr>`);

//            $.each(data, function (i, flight) {
//                if (data.length > 0) {
//                    statusColor = GetStatusColor(flight.Status);
//                }
//                $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${flight.LANDING_TIME}</td><td class="${statusColor}">${flight.Status}</td></tr>`)
//            })

//        }).fail(

//            // what to do on error
//            function (err) {
//                //console.error(err)
//            }
//        )
//    }
//    else if (btnState == 3) {
//        $.ajax({

//            url: `http://localhost:57588/api/AnonymousFacade/AllFlights/3`
//        }).then((data) => {
//            let $flight_table = $("#flights").empty();
//            $flight_table.append(`  <tr>
//                                    <th></th>
//                                    <th>Flight</th>
//                                    <th>Departing from</th>
//                                    <th>Destination</th>
//                                    <th>Departure time</th>
//                                    <th>Landing time</th>
//                                    <th>Status</th></tr>`);

//            $.each(data, function (i, flight) {
//                if (data.length > 0) {
//                    statusColor = GetStatusColor(flight.Status);
//                }
//                $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${flight.DEPARTURE_TIME}</td><td>${flight.LANDING_TIME}</td><td class="${statusColor}">${flight.Status}</td></tr>`)
//            })
//        }).fail(

//            // what to do on error
//            function (err) {
//                //console.error(err)
//            }
//        )
//    }
//}
//Get airline companies to drop down box
$.ajax({

    url: `http://localhost:57588/api/AnonymousFacade/allairlinecompanies`

}).then((data) => {

    let len = data.length;
    let id = 1;
    $("#multiSelectFieldCompanies").empty();
    $("#multiSelectFieldCompanies").append("<option selected></option>");
    for (var i = 0; i < len; i++) {

        let name = data[i].AIRLINE_NAME;

        $("#multiSelectFieldCompanies").append("<option value='" + id + "'>" + name + "</option>");
        id++;
    }
}).fail(

    // what to do on error
    function (err) {
        //console.error(err)
    }
)
//Get origin countries to drop down box
$.ajax({

    url: `http://localhost:57588/api/AnonymousFacade/allCountries`

}).then((data) => {

    let len = data.length;
    let id = 1;
    $("#multiSelectFieldCountries").empty();
    $("#multiSelectFieldCountries").append("<option selected></option>");
    for (var i = 0; i < len; i++) {

        let name = data[i].COUNTRY_NAME;

        $("#multiSelectFieldCountries").append("<option value='" + id + "'>" + name + "</option>");
        id++;
    }
}).fail(

    // what to do on error
    function (err) {
        //console.error(err)
    }
)
//Get flight Ids to drop down box
$.ajax({

    url: `http://localhost:57588/api/AnonymousFacade/allFlightIds`

}).then((data) => {

    let len = data.length;
    let id = 1;

    $("#multiSelectFieldFlightIds").empty();
    $("#multiSelectFieldFlightIds").append("<option selected></option>");
    for (var i = 0; i < len; i++) {

        let ID = data[i];

        $("#multiSelectFieldFlightIds").append("<option value='" + id + "'>" + ID + "</option>");
        id++;
    }

}).fail(

    // what to do on error
    function (err) {
        //console.error(err)
    }
)



//post search parameters of flight
function getFlightsJQ(bState, typeName) {

    let flightId = $('#multiSelectFieldFlightIds option:selected').text();
    let originCountry = $('#multiSelectFieldCountries option:selected').text();
    let airlineCompany = $('#multiSelectFieldCompanies option:selected').text();
   
    btnState = bState;

    $.ajax({
       
        url: `http://localhost:57588/api/AnonymousFacade/SearchFlightByConditions/search?typeName=${typeName}&flightId=${flightId}&country=${originCountry}&company=${airlineCompany}`
      
    }).then((data) => {
        //if (arrivalsDepartures == 'Show all') {
        if (btnState == 3) {
            let $flight_table = $("#flights").empty();
            $flight_table.append(`  <tr>
                                    <th></th>
                                    <th>Flight</th>
                                    <th>Departing from</th>
                                    <th>Destination</th>
                                    <th>Departure time</th>
                                    <th>Landing time</th>
                                    </tr>`);

            $.each(data, function (i, flight) {
                if (data.length > 0) {
            
                    scheduledDepartureDate = dateFormat(flight.DEPARTURE_TIME, 'dd/mm')

                    scheduledDepartureTime = dateFormat(flight.DEPARTURE_TIME, 'HH:MM:ss')

                    scheduledLandingDate = dateFormat(flight.LANDING_TIME, 'dd/mm')

                    scheduledLandingTime = dateFormat(flight.LANDING_TIME, 'HH:MM:ss')
                }
                $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${scheduledDepartureDate}&nbsp&nbsp&nbsp${scheduledDepartureTime}<td>${scheduledLandingDate}&nbsp&nbsp&nbsp${scheduledLandingTime}</td></tr>`)
            })
        }
        if (btnState == 2) //(arrivalsDepartures == 'Arrivals')
        {
            let $flight_table = $("#flights").empty();
            $flight_table.append(`  <tr>
                                    <th></th>
                                    <th>Flight</th>
                                    <th>Departing from</th>
                                    <th>Destination</th>
                                    <th>Scheduled time</th>
                                    <th>Estimated time</th>
                                    <th>Status</th></tr>`);

            $.each(data, function (i, flight) {
                if (data.length > 0) {
                    if (flight.LandingStatus == 'LANDING') {
                        statusColor = 'squareLightorange';
                    }
                    else if (flight.LandingStatus == 'LANDED') {
                        statusColor = 'squareLightgreen';
                    }
                    else if (flight.LandingStatus == 'NOT FINAL') {
                        statusColor = 'squareLightgreen';
                    }
                    else if (flight.LandingStatus == 'FINAL') {
                        statusColor = 'squareLightgreen';
                    }
                    //statusColor = GetStatusColor(flight.Status);
                    scheduledLandingDate = dateFormat(flight.LANDING_TIME, 'dd/mm')

                    scheduledLandingTime = dateFormat(flight.LANDING_TIME, 'HH:MM:ss')
                }
                $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${scheduledLandingDate}&nbsp&nbsp&nbsp${scheduledLandingTime}</td><td>${estimatedLandingTime}</td><td class="${statusColor}">${flight.LandingStatus}</td></tr>`)
            })
        }
        else if (btnState == 1) //(arrivalsDepartures == 'Departures')
        {
            let $flight_table = $("#flights").empty();
            $flight_table.append(`  <tr>
                                    <th></th>
                                    <th>Flight</th>
                                    <th>Departing from</th>
                                    <th>Destination</th>
                                    <th>Scheduled time</th>
                                    <th>Estimated time</th>
                                    <th>Status</th></tr>`);

            $.each(data, function (i, flight) {
                if (data.length > 0) {
                    statusColor = GetDepartureStatusColor(flight.Status);
                    scheduledDepartureDate = dateFormat(flight.DEPARTURE_TIME, 'dd/mm')

                    scheduledDepartureTime = dateFormat(flight.DEPARTURE_TIME, 'HH:MM:ss')
                 
                    estimatedDepartureTime = dateFormat(flight.REAL_DEPARTURE_TIME, 'HH:MM:ss')
                }
                $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${scheduledDepartureDate}&nbsp&nbsp&nbsp&nbsp${scheduledDepartureTime}</td><td>${estimatedDepartureTime}</td><td id="status" class="${statusColor}">${flight.Status}</td></tr>`)
            })
        }
    }).fail(

        // what to do on error
        function (err) {
            //console.error(err)
        }
    )
}