$(document).ready(function () {
    console.log("document loaded successfully");
  
    fetchdata();
    setInterval(CheckTime, 30000);
});

let scheduledDepartureDate = '';
let scheduledDepartureTime = '';
let estimatedDepartureTime = '';
let scheduledLandingDate = '';
let scheduledLandingTime = '';
let estimatedLandingTime = '';
let statusColor = '';
let btnState = 0;
let rows = 0;
let selectedRows = 5;
let mouseMoveTime = 0;
function Clear() {
    document.getElementById('multiSelectFieldFlightIds').selectedIndex = 0;
    document.getElementById('multiSelectFieldCompanies').selectedIndex = 0;
    document.getElementById('multiSelectFieldCountries').selectedIndex = 0;
    fetchdata();
    //LoadDropboxes();
    setTimeout(LoadDropboxes, 500);
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

LoadDropboxes(); //Load drop boxes first time

function LoadDropboxes() {
    $.ajax({
        type: 'GET',
        url: `http://localhost:57588/api/AnonymousFacade/allairlinecompanies/search?typeName=Departures`

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

        url: `http://localhost:57588/api/AnonymousFacade/allCountriesByScheduledTime/search?typeName=Departures`

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

        url: `http://localhost:57588/api/AnonymousFacade/allFlightIdsByScheduledTime/search?typeName=Departures`

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
//Get Data with search option
function fetchdata() {

    let flightId = $('#multiSelectFieldFlightIds option:selected').text();
    let originCountry = $('#multiSelectFieldCountries option:selected').text();
    let airlineCompany = $('#multiSelectFieldCompanies option:selected').text();

    $.ajax({
        type: 'GET',
     
        url: `http://localhost:57588/api/AnonymousFacade/SearchFlightByConditions/search?typeName=Departures&flightId=${flightId}&country=${originCountry}&company=${airlineCompany}`

    }).then((data) => {
        let number = 1;
        const event = new Date();
        let hour = event.toLocaleTimeString('it-IT')

        localStorage.setItem('FlightData', JSON.stringify(data));
        localStorage.setItem('Time', hour);
  
        let $flight_table = $("#flights").empty();
        $flight_table.append(`        <tr>
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
            $flight_table.append(`<tr><td><img class="CompanyImage Imageth" src="../../Content/companyImages/${flight.AIRLINE_NAME}.jpg" alt="Logo" width="250" height="100"></td><td> ${flight.AIRLINE_NAME} ${flight.ID} </td><td>${flight.O_COUNTRY_NAME}</td><td>${flight.D_COUNTRY_NAME}</td><td>${scheduledDepartureDate}&nbsp&nbsp&nbsp&nbsp${scheduledDepartureTime}</td><td>${estimatedDepartureTime}</td><td id="c${number}" class="${statusColor}">${departureStatus}</td></tr>`)
            number++;
        })
        myFunction(data);
        rows = GetNumOfRows();
        getPagination('#flights');
       
    }).fail(

        // what to do on error
        function (err) {
            GetOnError();

        }
    )
}
//Get search parameters of flight
function getFlightsJQ(bState, typeName) {

    let flightId = $('#multiSelectFieldFlightIds option:selected').text();
    let originCountry = $('#multiSelectFieldCountries option:selected').text();
    let airlineCompany = $('#multiSelectFieldCompanies option:selected').text();

    btnState = bState;

    $.ajax({

        url: `http://localhost:57588/api/AnonymousFacade/SearchFlightByConditions/search?typeName=${typeName}&flightId=${flightId}&country=${originCountry}&company=${airlineCompany}`

    }).then((data) => {
        const event = new Date();
        let hour = event.toLocaleTimeString('it-IT')

        localStorage.setItem('FlightData', JSON.stringify(data));
        localStorage.setItem('Time', hour);
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
            GetOnError();
        }
    )
}


function GetOnError() {
    let statusColor = '';
    let aadata = localStorage.getItem('FlightData');
    let time = localStorage.getItem('Time');
    //var ee = " fail to connect, please check your connection settings" + err;
    let adata = JSON.parse(aadata)
    swal(
        'Fail to connect!',
        'please check your connection settings',
        'error'
    )
    $('#time').html('Server not available! The information is presented from ' + JSON.stringify(time)).addClass("server_error_style")
    let $flight_table = $("#flights")
    $flight_table.append(`
                                    <tr>
                                    <th></th>
                                    <th>Flight</th>
                                    <th>Departing from</th>
                                    <th>Destination</th>
                                    <th>Scheduled time</th>
                                    <th>Estimated time</th>
                                    <th>Status</th></tr>`);


    $.each(adata, function (i, flight) {
        if (adata.length > 0) {

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

function myFunction(data) {

    let num = 1;

    $.each(data, function (i, flight) {
        console.log("REAL_DEPARTURE_TIME: " + flight.REAL_DEPARTURE_TIME);
        console.log("DEPARTURE_TIME: " + flight.DEPARTURE_TIME);
        if (flight.REAL_DEPARTURE_TIME == flight.DEPARTURE_TIME) {
            departureStatus = 'ON TIME';
          
        }
        else {
            departureStatus = 'DELAYED';
        }
        statusColor = GetDepartureStatusColor(departureStatus);
        let className = $('#c' + num).attr('class');

        if (className != statusColor) {
            $('#c' + num).removeClass(className)
            $('#c' + num).addClass(statusColor);
        }
        $('#c' + num).html(departureStatus);
        num++;
    })
}  

function GetNumOfRows() {
    let x = document.getElementById("flights").rows.length - 1;
    return x;
}

function CheckTime() {
    var d = new Date();
    if (d.getMinutes() - mouseMoveTime.getMinutes() > 1) {
        console.log("Run fetchdata()");
        fetchdata();
    }
}

$(document).mousemove(function (event) {
    mouseMoveTime = new Date();
});