$(document).ready(function () {
    console.log("document loaded");
    //getPagination('#flights');
    
});
let scheduledDepartureDate = '';
let scheduledDepartureTime = '';
let estimatedDepartureTime = '';
let scheduledLandingDate = '';
let scheduledLandingTime = '';
let estimatedLandingTime = '';
let statusColor = '';
let btnState = 0;
let typeName1 = '';
let arrivalsDepartures = '';
let rows = 0;
let selectedRows = 5;
function Clear() {
    document.getElementById('multiSelectFieldFlightIds').selectedIndex = 0;
    document.getElementById('multiSelectFieldCompanies').selectedIndex = 0;
    document.getElementById('multiSelectFieldCountries').selectedIndex = 0;
    state = 3;
    btnState = 3;
    arrivalsDepartures = "";
    getFlightsJQ(); 
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




function DeparturesClick() {
    if ($('#BtnDepatures').hasClass('flightBtnShadow')) {
        $('#BtnDepatures').removeClass('flightBtnShadow')
    }
    else {
        $('#BtnDepatures').addClass('flightBtnShadow')
    }
    GetBtnState();
    if (btnState == 1) {
        arrivalsDepartures = 'Departures';
    }
    else if (btnState == 2) {
        arrivalsDepartures = 'Arrivals';
    }
    else if (btnState == 3) {
        //arrivalsDepartures = "";
        FillDropBox("");
    }
    console.log("arrivalsDepartures from DeparturesClick: " + arrivalsDepartures);
    FillDropBox(arrivalsDepartures);

}
function ArrivalsClick() {
    if ($('#BtnArrivals').hasClass('flightBtnShadow')) {
        $('#BtnArrivals').removeClass('flightBtnShadow')
    }
    else {
        $('#BtnArrivals').addClass('flightBtnShadow')
    }
    GetBtnState();
    if (btnState == 1) {
        arrivalsDepartures = 'Departures';
    }
    else if (btnState == 2) {
        arrivalsDepartures = 'Arrivals';
    }
    else if (btnState == 3) {
        //arrivalsDepartures = "";
        FillDropBox("");
    }
    console.log("arrivalsDepartures from ArrivalsClick: " + arrivalsDepartures);
    FillDropBox(arrivalsDepartures);

}

//function ArrivalsDeparturesClick() {
//    if (btnState == 3) {
//        FillDropBox("");
//    }
//}





function GetBtnState() {
    if (!$('#BtnDepatures').hasClass('flightBtnShadow') && $('#BtnArrivals').hasClass('flightBtnShadow')) {
        btnState = 1;
    }
    else if (!$('#BtnArrivals').hasClass('flightBtnShadow') && $('#BtnDepatures').hasClass('flightBtnShadow')) {
        btnState = 2;
    }
    else if (!$('#BtnArrivals').hasClass('flightBtnShadow') && !$('#BtnDepatures').hasClass('flightBtnShadow')) {
        btnState = 3;
    }
    else {
        btnState = 3;
    }
    return btnState;
}

function FillDropBox(typeName) {
//Get airline companies to drop down box
$.ajax({

    url: `http://localhost:57588/api/AnonymousFacade/allairlinecompanies/search?typeName=${typeName}`

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

    url: `http://localhost:57588/api/AnonymousFacade/allCountriesByScheduledTime/search?typeName=${typeName}`

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

    url: `http://localhost:57588/api/AnonymousFacade/allFlightIdsByScheduledTime/search?typeName=${typeName}`

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
}
btnState = 3;
FillDropBox("");


 //post search parameters of flight
function getFlightsJQ() {
   
  
    let url_web_api = `http://localhost:57588/api/AnonymousFacade/searchbydata`

    let flightId = $('#multiSelectFieldFlightIds option:selected').text();
    let originCountry = $('#multiSelectFieldCountries option:selected').text();
    let airlineCompany = $('#multiSelectFieldCompanies option:selected').text();
    //let arrivalsDepartures = $('#multiSelectField option:selected').text();
    let state = GetBtnState();
    if (state == 1) {
        arrivalsDepartures = 'Departures';
    }
    else if (state == 2) {
        arrivalsDepartures = 'Arrivals';
    }
    else if(state == 3)
    {
        arrivalsDepartures = "";
    }
    

    let search = {
        flightId,
        originCountry,
        airlineCompany,
        arrivalsDepartures
    }
    // JSON.stringify(item)
    var ajaxPostDataConfig = {
        type: "POST", // what is the method? post, get, put , delete

        url: url_web_api,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(search) // request http body
    }
  
    //Get Data with search option
 
      
    $.ajax(ajaxPostDataConfig).then((data) => {
        btnState = state;
         
        //if (arrivalsDepartures == '') {
        if (btnState == 3 || btnState == 0) {
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
            else if (btnState == 2) //(arrivalsDepartures == 'Arrivals')
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
                        let dateTimeNow = new Date();

                        const estimatedDateTime = new Date(flight.REAL_LANDING_TIME);

                        let dateTimeFinal = new Date((new Date(flight.REAL_LANDING_TIME)).setHours((new Date(flight.REAL_LANDING_TIME)).getHours() - 2));

                        let dateTimeLanding = new Date((new Date(flight.REAL_LANDING_TIME)).setMinutes((new Date(flight.REAL_LANDING_TIME)).getMinutes() - 15));

                        if (flight.REAL_LANDING_TIME == flight.LANDING_TIME) {

                            if (dateTimeNow >= dateTimeLanding && dateTimeNow < estimatedDateTime) {
                                LandingStatus = "LANDING";
                                statusColor = 'squareLightorange';
                            }
                            else if (dateTimeNow >= estimatedDateTime) {
                                LandingStatus = "LANDED";
                                statusColor = 'squareLightgreen';
                            }
                            else if (dateTimeNow >= dateTimeFinal && dateTimeNow < dateTimeLanding) {
                                LandingStatus = "FINAL";
                                statusColor = 'squareLightgreen';
                            }
                            else if (dateTimeNow < dateTimeFinal) {
                                LandingStatus = "NOT FINAL";
                                statusColor = 'squareLightgreen';
                            }
                        }
                        else {
                            LandingStatus = "DELAYED";
                            statusColor = 'squareLightcoral';
                        }

                        scheduledLandingDate = dateFormat(flight.LANDING_TIME, 'dd/mm')

                        scheduledLandingTime = dateFormat(flight.LANDING_TIME, 'HH:MM:ss')

                        estimatedLandingTime = dateFormat(flight.REAL_LANDING_TIME, 'HH:MM:ss')
                    }
                    $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${scheduledLandingDate}&nbsp&nbsp&nbsp${scheduledLandingTime}</td><td>${estimatedLandingTime}</td><td class="${statusColor}">${LandingStatus}</td></tr>`)
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
                        if (flight.REAL_DEPARTURE_TIME == flight.DEPARTURE_TIME) {
                            departureStatus = 'ON TIME';
                        }
                        else {
                            departureStatus = 'DELAYED';
                        }

                        statusColor = GetDepartureStatusColor(departureStatus);
                        scheduledDepartureDate = dateFormat(flight.DEPARTURE_TIME, 'dd/mm')

                        scheduledDepartureTime = dateFormat(flight.DEPARTURE_TIME, 'HH:MM:ss')

                        estimatedDepartureTime = dateFormat(flight.REAL_DEPARTURE_TIME, 'HH:MM:ss')
                    }
                    $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${scheduledDepartureDate}&nbsp&nbsp&nbsp&nbsp${scheduledDepartureTime}</td><td>${estimatedDepartureTime}</td><td id="status" class="${statusColor}">${departureStatus}</td></tr>`)
                })
        }
        rows = GetNumOfRows();
        getPagination('#flights');
        }).fail(

            // what to do on error
            function (err) {
                //console.error(err)
            }
        )
    
}

function GetNumOfRows() {
    let x = document.getElementById("flights").rows.length - 1;
    return x;
}