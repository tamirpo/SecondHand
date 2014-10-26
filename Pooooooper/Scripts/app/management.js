
var areasVsSubAreas = [];
var areasVsLocations = [];
var areaIdVsName = [];
var subAreaIdVsName = [];
var locationIdVsName = [];

$(document).ready(function () {
    $('a[id$="Report"]').click(function () {
        showRelevantGraphForm(this.id);
    });
});

function showRelevantGraphForm(reportName) {

    var forms = $('div[id$="Form"]');
    for (var i = 0; i < forms.length; i++) {
        if (reportName + 'Form' == forms[i].id){
            forms[i].style.display = "";
        }
        else{
            forms[i].style.display = "none";
        }  
    }
}

function generateApartmentSearchPostsReport() {


    var startDate = document.getElementById('apartmentSearchPostsReport_startDate').value;
    var endDate = document.getElementById('apartmentSearchPostsReport_endDate').value;

    $.ajax({
        type: "POST",
        url: "Management/GenerateApartmentSearchPostsReport",
        data: JSON.stringify({ startDate:startDate, endDate:endDate}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            
            if (data != null) {

                var reportResultDiv = document.getElementById('apartmentSearchPostsForm');
                reportResultDiv.innerHTML = "";

                var table = document.createElement('table');
                table.border = "1";
                table.style.maxWidth = "800px";

                var trTitle = document.createElement('tr');

                var tdCity = document.createElement('td');
                tdCity.innerHTML = "עיר";
                tdCity.style.fontWeight = "bold";
                var tdDate = document.createElement('td');
                tdDate.innerHTML = "תאריך";
                tdDate.style.fontWeight = "bold";
                var tdSender = document.createElement('td');
                tdSender.innerHTML = "משתמש פייסבוק";
                tdSender.style.fontWeight = "bold";
                var tdPostText = document.createElement('td');
                tdPostText.innerHTML = "טקסט";
                tdPostText.style.fontWeight = "bold";
                trTitle.appendChild(tdCity);
                trTitle.appendChild(tdDate);
                trTitle.appendChild(tdSender);
                trTitle.appendChild(tdPostText);
                table.appendChild(trTitle);

                for (var i = 0; i < data.length; i++) {

                    var tr = document.createElement('tr');

                    // Extract City
                    var tdCity = document.createElement('td');
                    tdCity.innerHTML = data[i].City;
                    tdCity.style.fontSize = "12px";
                    tr.appendChild(tdCity);

                    // Extract date
                    var tdDate = document.createElement('td');
                    var date = new Date(parseInt(data[i].DateCreated.substr(6)));
                    var curr_date = date.getDate();
                    var curr_month = date.getMonth();
                    var curr_year = date.getFullYear();
                    var dateString = curr_date + "-" + curr_month + "-" + curr_year;
                    tdDate.innerHTML = dateString;
                    tdDate.style.fontSize = "12px";
                    tr.appendChild(tdDate);

                    var tdSender = document.createElement('td');
                    tdSender.innerHTML = data[i].Sender;
                    tdSender.style.fontSize = "12px";
//                    tdSender.style.fontWeight = "bold";
                    tr.appendChild(tdSender);

                    var tdPostText = document.createElement('td');
                    tdPostText.innerHTML = data[i].PostText;
                    tdPostText.style.fontSize = "12px";
                    tdPostText.style.maxWidth = "500px";
                    tr.appendChild(tdPostText);

                    table.appendChild(tr);
                }

                reportResultDiv.appendChild(table);
            }

        },
        error: function (ex) {
            alert(ex);
        }
    });
}

function generateTotalPostsReport() {


    var startDate = document.getElementById('totalPostsReport_startDate').value;
    var endDate = document.getElementById('totalPostsReport_endDate').value;
    var cityId = document.getElementById('totalPostsCitySelect').options[document.getElementById('totalPostsCitySelect').selectedIndex].value;

    $.ajax({
        type: "POST",
        url: "Management/GenerateTotalPostsReport",
        data: JSON.stringify({ startDate: startDate, endDate: endDate , cityId:cityId}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data != null) {

                var reportResultDiv = document.getElementById('report1Form');
                reportResultDiv.innerHTML = "";

                var table = document.createElement('table');
                table.border = "1";
                table.style.maxWidth = "800px";

                var trTitle = document.createElement('tr');

                var tdDate = document.createElement('td');
                tdDate.innerHTML = "תאריך";
                tdDate.style.fontWeight = "bold";
                var tdSender = document.createElement('td');
                tdSender.innerHTML = "משתמש פייסבוק";
                tdSender.style.fontWeight = "bold";
                var tdPostText = document.createElement('td');
                tdPostText.innerHTML = "טקסט";
                tdPostText.style.fontWeight = "bold";
                trTitle.appendChild(tdDate);
                trTitle.appendChild(tdSender);
                trTitle.appendChild(tdPostText);
                table.appendChild(trTitle);

                for (var i = 0; i < data.length; i++) {

                    var tr = document.createElement('tr');
                    var tdDate = document.createElement('td');

                    var date = new Date(parseInt(data[i].DateCreated.substr(6)));

                    var curr_date = date.getDate();
                    var curr_month = date.getMonth();
                    var curr_year = date.getFullYear();
                    var dateString = curr_date + "-" + curr_month + "-" + curr_year;
                    tdDate.innerHTML = dateString;
                    tdDate.style.fontSize = "12px";
                    tr.appendChild(tdDate);

                    var tdSender = document.createElement('td');
                    tdSender.innerHTML = data[i].Sender;
                    tdSender.style.fontSize = "12px";
                    //                    tdSender.style.fontWeight = "bold";
                    tr.appendChild(tdSender);

                    var tdPostText = document.createElement('td');
                    tdPostText.innerHTML = data[i].PostText;
                    tdPostText.style.fontSize = "12px";
                    tdPostText.style.maxWidth = "500px";
                    tr.appendChild(tdPostText);

                    table.appendChild(tr);
                }

                reportResultDiv.appendChild(table);
            }

        },
        error: function (ex) {
            alert(ex);
        }
    });


}

function generateApartmentsDetailsReport() {

    var startDate = document.getElementById('apartmentsDetailsReport_startDate').value;
    var endDate = document.getElementById('apartmentsDetailsReport_endDate').value;
    var cityId = document.getElementById('apartmentsDetailsCitySelect').options[document.getElementById('apartmentsDetailsCitySelect').selectedIndex].value;

    var selectedAreaId = document.getElementById('apartmentsDetailsAreaSelect').options[document.getElementById('apartmentsDetailsAreaSelect').selectedIndex].value;
    var selectedLocationId = document.getElementById('apartmentsDetailsLocationSelect').options[document.getElementById('apartmentsDetailsLocationSelect').selectedIndex].value;
    var selectedSubAreaId = document.getElementById('apartmentsDetailsSubAreaSelect').options[document.getElementById('apartmentsDetailsSubAreaSelect').selectedIndex].value;
    var selectedPurpose = document.getElementById('apartmentsDetailsPurposeSelect').options[document.getElementById('apartmentsDetailsPurposeSelect').selectedIndex].value;

    var fromPrice = document.getElementById('fromPrice').value;
    var toPrice = document.getElementById('toPrice').value;
    var fromRoomsNumber = document.getElementById('fromRoomsNumber').value;
    var toRoomsNumber = document.getElementById('toRoomsNumber').value;
    var fromRoomatesNumber = document.getElementById('fromRoomatesNumber').value;
    var toRoomatesNumber = document.getElementById('toRoommatesNumber').value;
    var fromSize = document.getElementById('fromSize').value;
    var toSize = document.getElementById('toSize').value;

    var fromAgency = 0;
    if (document.getElementById('noFromAgencyRadio').checked) {
        fromAgency = 47;
    }
    else if (document.getElementById('yesFromAgencyRadio').checked) {
        fromAgency = 46;
    }

    var sublet = 0;
    if (document.getElementById('yesSubletRadio').checked) {
        sublet = 40;
    }
    else if (document.getElementById('noSubletRadio').checked) {
        sublet = 41;
    }

    var furnitured = 0;
    if (document.getElementById('furnituredCheckbox').checked) {
        furnitured = 37;
    }
    var renovated = 0;
    if (document.getElementById('renovatedCheckbox').checked) {
        renovated = 6;
    }
    var elevator = 0;
    if (document.getElementById('elevatorCheckbox').checked) {
        elevator = 29;
    }

    var balcony = 0;
    if (document.getElementById('balconyCheckbox').checked) {
        balcony = 30;
    }
    var smoke = 0;
    if (document.getElementById('smokeCheckbox').checked) {
        smoke = 35;
    }
    var pets = 0;
    if (document.getElementById('petsCheckbox').checked) {
        pets = 33;
    }
    var parking = 0;
    if (document.getElementById('parkingCheckbox').checked) {
        parking = 5;
    }
   

    $.ajax({
        type: "POST",
        url: "Management/GenerateApartmentsDetailsReport",
        data: JSON.stringify({
            startDate: startDate, endDate: endDate, cityId: cityId,
            areaId: selectedAreaId, subAreaId: selectedSubAreaId, locationId: selectedLocationId,
            fromPrice: fromPrice, toPrice: toPrice, fromSize: fromSize,
            toSize: toSize, fromRoomsNumber: fromRoomsNumber, toRoomsNumber: toRoomsNumber,
            fromRoomatesNumber: fromRoomatesNumber, toRoomatesNumber: toRoomatesNumber, furnitured: furnitured,
            renovated: renovated, elevator: elevator, sublet: sublet,
            balcony: balcony, smoke: smoke, pets: pets,
            parking: parking, purpose: selectedPurpose, fromAgency:fromAgency
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data != null) {

                var reportResultDiv = document.getElementById('apartmentsDetailsReportForm');
                reportResultDiv.innerHTML = "";

                var table = document.createElement('table');
                table.border = "1";
                table.style.maxWidth = "800px";

                var trTitle = document.createElement('tr');

                var tdDate = document.createElement('td');
                tdDate.innerHTML = "תאריך";
                tdDate.style.fontWeight = "bold";
                tdDate.style.fontSize = "12px";
                tdDate.style.minWidth = "65px";
                trTitle.appendChild(tdDate);

                var tdId = document.createElement('td');
                tdId.innerHTML = "זיהוי";
                tdId.style.fontWeight = "bold";
                tdId.style.fontSize = "12px";
                tdId.style.minWidth = "65px";
                trTitle.appendChild(tdId);

                var tdCity = document.createElement('td');
                tdCity.innerHTML = "עיר";
                tdCity.style.fontWeight = "bold";
                tdCity.style.fontSize = "12px";
                tdCity.style.minWidth = "65px";
                trTitle.appendChild(tdCity);



                /*var tdArea = document.createElement('td');
                tdArea.innerHTML = "איזור";
                tdArea.style.fontWeight = "bold";
                tdArea.style.fontSize = "12px";
                trTitle.appendChild(tdArea);


                var tdSubArea = document.createElement('td');
                tdSubArea.innerHTML = "תת איזור";
                tdSubArea.style.fontWeight = "bold";
                tdSubArea.style.fontSize = "12px";
                trTitle.appendChild(tdSubArea);

                var tdLocation = document.createElement('td');
                tdLocation.innerHTML = "לוקיישן";
                tdLocation.style.fontWeight = "bold";
                tdLocation.style.fontSize = "12px";
                trTitle.appendChild(tdLocation);
                */
                var tdPurpose = document.createElement('td');
                tdPurpose.innerHTML = "סוג חיפוש";
                tdPurpose.style.fontWeight = "bold";
                tdPurpose.style.fontSize = "12px";
                tdPurpose.style.minWidth = "65px";
                trTitle.appendChild(tdPurpose);

                var tdPrice = document.createElement('td');
                tdPrice.innerHTML = "מחיר";
                tdPrice.style.fontWeight = "bold";
                tdPrice.style.fontSize = "12px";
                tdPrice.style.minWidth = "65px";
                trTitle.appendChild(tdPrice);

                var tdRoomsNumber = document.createElement('td');
                tdRoomsNumber.innerHTML = "מס חדרים";
                tdRoomsNumber.style.fontWeight = "bold";
                tdRoomsNumber.style.fontSize = "12px";
                tdRoomsNumber.style.minWidth = "65px";
                trTitle.appendChild(tdRoomsNumber);

                var tdRoommates = document.createElement('td');
                tdRoommates.innerHTML = "שותפים";
                tdRoommates.style.fontWeight = "bold";
                tdRoommates.style.fontSize = "12px";
                tdRoommates.style.minWidth = "65px";
                trTitle.appendChild(tdRoommates);

                var tdSize = document.createElement('td');
                tdSize.innerHTML = "גודל";
                tdSize.style.fontWeight = "bold";
                tdSize.style.fontSize = "12px";
                tdSize.style.minWidth = "65px";
                trTitle.appendChild(tdSize);

                var tdSublet = document.createElement('td');
                tdSublet.innerHTML = "סאבלט";
                tdSublet.style.fontWeight = "bold";
                tdSublet.style.fontSize = "12px";
                tdSublet.style.minWidth = "65px";
                trTitle.appendChild(tdSublet);

                var tdFromAgency = document.createElement('td');
                tdFromAgency.innerHTML = "מתיווך";
                tdFromAgency.style.fontWeight = "bold";
                tdFromAgency.style.fontSize = "12px";
                tdFromAgency.style.minWidth = "65px";
                trTitle.appendChild(tdFromAgency);

                var tdFurnitured = document.createElement('td');
                tdFurnitured.innerHTML = "מרוהטת";
                tdFurnitured.style.fontWeight = "bold";
                tdFurnitured.style.fontSize = "12px";
                tdFurnitured.style.minWidth = "65px";
                trTitle.appendChild(tdFurnitured);

                var tdRenovated = document.createElement('td');
                tdRenovated.innerHTML = "משופצת";
                tdRenovated.style.fontWeight = "bold";
                tdRenovated.style.fontSize = "12px";
                tdRenovated.style.minWidth = "65px";
                trTitle.appendChild(tdRenovated);

                var tdElevator = document.createElement('td');
                tdElevator.innerHTML = "מעלית";
                tdElevator.style.fontSize = "12px";
                tdElevator.style.fontWeight = "bold";
                tdElevator.style.minWidth = "65px";
                trTitle.appendChild(tdElevator);

                var tdBalcony = document.createElement('td');
                tdBalcony.innerHTML = "מרפסת";
                tdBalcony.style.fontWeight = "bold";
                tdBalcony.style.fontSize = "12px";
                tdBalcony.style.minWidth = "65px";
                trTitle.appendChild(tdBalcony);

                var tdSmoke = document.createElement('td');
                tdSmoke.innerHTML = "ללא עישון";
                tdSmoke.style.fontWeight = "bold";
                tdSmoke.style.minWidth = "65px";
                tdSmoke.style.fontSize = "12px";
                trTitle.appendChild(tdSmoke);

                var tdPets = document.createElement('td');
                tdPets.innerHTML = "ללא בעח";
                tdPets.style.fontWeight = "bold";
                tdPets.style.fontSize = "12px";
                tdPets.style.minWidth = "65px";
                trTitle.appendChild(tdPets);

                var tdParking = document.createElement('td');
                tdParking.innerHTML = "חניה";
                tdParking.style.fontWeight = "bold";
                tdParking.style.fontSize = "12px";
                tdParking.style.minWidth = "65px";
                trTitle.appendChild(tdParking);
                
                table.appendChild(trTitle);


                var sumPrice = 0;
                var countPrice = 0;
                for (var i = 0; i < data.length; i++) {

                    var tr = document.createElement('tr');
                    var tdDate = document.createElement('td');

                    var date = new Date(parseInt(data[i].DateCreated.substr(6)));

                    var curr_date = date.getDate();
                    var curr_month = date.getMonth();
                    var curr_year = date.getFullYear();
                    var dateString = curr_date + "-" + curr_month + "-" + curr_year;
                    tdDate.innerHTML = dateString;
                    tdDate.style.fontSize = "12px";
                    tr.appendChild(tdDate);

                    var tdId = document.createElement('td');
                    tdId.innerHTML = data[i].id;
                    tdId.style.fontSize = "12px";
                    tr.appendChild(tdId);

                    var tdCity = document.createElement('td');
                    tdCity.innerHTML = data[i].City;
                    tdCity.style.fontSize = "12px";
                    tr.appendChild(tdCity);

                    /*var tdArea = document.createElement('td');
                    tdArea.innerHTML = data[i].Area;
                    tdArea.style.fontSize = "12px";
                    tr.appendChild(tdArea);

                    var tdSubArea = document.createElement('td');
                    tdSubArea.innerHTML = data[i].SubArea;
                    tdSubArea.style.fontSize = "12px";
                    tr.appendChild(tdSubArea);

                    var tdLocation = document.createElement('td');
                    tdLocation.innerHTML = data[i].Location;
                    tdLocation.style.fontSize = "12px";
                    tr.appendChild(tdLocation);
                    */
                    var tdPurpose = document.createElement('td');
                    tdPurpose.innerHTML = data[i].Purpose;
                    tdPurpose.style.fontSize = "12px";
                    tr.appendChild(tdPurpose);

                    var tdPrice = document.createElement('td');
                    tdPrice.innerHTML = data[i].Price;
                    tdPrice.style.fontSize = "12px";
                    tr.appendChild(tdPrice);

                    if (data[i].Price > 0) {
                        sumPrice += data[i].Price;
                        countPrice++;
                    }


                    var tdRoomsNumber = document.createElement('td');
                    tdRoomsNumber.innerHTML = data[i].RoomsNumber;
                    tdRoomsNumber.style.fontSize = "12px";
                    tr.appendChild(tdRoomsNumber);

                    var tdRoommates = document.createElement('td');
                    tdRoommates.innerHTML = data[i].RoommatesNumber;
                    tdRoommates.style.fontSize = "12px";
                    tr.appendChild(tdRoommates);

                    var tdSize = document.createElement('td');
                    tdSize.innerHTML = data[i].Size;
                    tdSize.style.fontSize = "12px";
                    tr.appendChild(tdSize);

                    var tdSublet = document.createElement('td');
                    tdSublet.innerHTML = data[i].Sublet;
                    tdSublet.style.fontSize = "12px";
                    tr.appendChild(tdSublet);

                    var tdFromAgency = document.createElement('td');
                    tdFromAgency.innerHTML = data[i].FromAgency;
                    tdFromAgency.style.fontSize = "12px";
                    tr.appendChild(tdFromAgency);

                    var tdFurnitured = document.createElement('td');
                    tdFurnitured.innerHTML = data[i].Furnitured;
                    tdFurnitured.style.fontSize = "12px";
                    tr.appendChild(tdFurnitured);

                    var tdRenovated = document.createElement('td');
                    tdRenovated.innerHTML = data[i].Renovated;
                    tdRenovated.style.fontSize = "12px";
                    tr.appendChild(tdRenovated);

                    var tdElevator = document.createElement('td');
                    tdElevator.innerHTML = data[i].Elevator;
                    tdElevator.style.fontSize = "12px";
                    tr.appendChild(tdElevator);

                    var tdBalcony = document.createElement('td');
                    tdBalcony.innerHTML = data[i].Balcony;
                    tdBalcony.style.fontSize = "12px";
                    tr.appendChild(tdBalcony);

                    var tdSmoke = document.createElement('td');
                    tdSmoke.innerHTML = data[i].Smoke;
                    tdSmoke.style.fontSize = "12px";
                    tr.appendChild(tdSmoke);

                    var tdPets = document.createElement('td');
                    tdPets.innerHTML = data[i].Pets;
                    tdPets.style.fontSize = "12px";
                    tr.appendChild(tdPets);

                    var tdParking = document.createElement('td');
                    tdParking.innerHTML = data[i].Parking;
                    tdParking.style.fontSize = "12px";
                    tr.appendChild(tdParking);

                    table.appendChild(tr);
                }

                var trPriceAvg = document.createElement('tr');

                var tdDate = document.createElement('td');
                tdDate.innerHTML = "ממוצע: " + Math.round(sumPrice/countPrice);
                tdDate.style.fontWeight = "bold";
                tdDate.style.fontSize = "12px";
                tdDate.colSpan = 17;
                trPriceAvg.appendChild(tdDate);

                table.appendChild(trPriceAvg);

                reportResultDiv.appendChild(table);
            }

        },
        error: function (ex) {
            alert(ex);
        }
    });


}


function onApartmentsDetailsAreaSelect() {

    // Fill subareas and locations of selected area
    var areasSelect = document.getElementById('apartmentsDetailsAreaSelect');
    var subAreasSelect = document.getElementById('apartmentsDetailsSubAreaSelect');
    var locationsSelect = document.getElementById('apartmentsDetailsLocationSelect');

    subAreasSelect.innerHTML = "";
    locationsSelect.innerHTML = "";

    var defaultOption = document.createElement('option');
    defaultOption.value = 0;
    defaultOption.innerHTML = "בחר";
    locationsSelect.appendChild(defaultOption);

    var defaultOption1 = document.createElement('option');
    defaultOption1.value = 0;
    defaultOption1.innerHTML = "בחר";
    subAreasSelect.appendChild(defaultOption1);

    var areaId = areasSelect.options[areasSelect.selectedIndex].value;
    
    for (var i = 0; i < areasVsSubAreas[areaId].length; i++) {
        var option = document.createElement('option');
        option.value = areasVsSubAreas[areaId][i];
        option.innerHTML = subAreaIdVsName[areasVsSubAreas[areaId][i]];

        subAreasSelect.appendChild(option);
    }

    for (var i = 0; i < areasVsLocations[areaId].length; i++) {
        var option = document.createElement('option');
        option.value = areasVsLocations[areaId][i];
        option.innerHTML = locationIdVsName[areasVsLocations[areaId][i]];

        locationsSelect.appendChild(option);
    }
}

function onApartmentsDetailsCitySelect() {

    // AJAX call to get addresses
    var cityId = document.getElementById('apartmentsDetailsCitySelect').options[document.getElementById('apartmentsDetailsCitySelect').selectedIndex].value;

    $.ajax({
        type: "POST",
        url: "Management/GetAddressesIdVsNamesForCity",
        data: JSON.stringify({ cityId: cityId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data != null) {

                // data is list of dictionaries : areasVsSubAreas, areasVsLocations...
                areaIdVsName = data[0];
                subAreaIdVsName = data[1];
                locationIdVsName = data[2];

                var areasSelect = document.getElementById('apartmentsDetailsAreaSelect');

                areasSelect.innerHTML = "";
                var defaultOption = document.createElement('option');
                defaultOption.value = 0;
                defaultOption.innerHTML = "בחר";
                areasSelect.appendChild(defaultOption);

                for (var areaId in data[0]) {
                    var areaOption = document.createElement('option');
                    areaOption.value = areaId;
                    areaOption.innerHTML = data[0][areaId];

                    areasSelect.appendChild(areaOption);
                }

                $.ajax({
                    type: "POST",
                    url: "Management/GetAddressesForCity",
                    data: JSON.stringify({ cityId: cityId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        if (data != null) {

                            // data is list of dictionaries : areasVsSubAreas, areasVsLocations...
                            areasVsSubAreas = data[0];
                            areasVsLocations = data[1];
                        }

                    },
                    error: function (ex) {
                        alert(ex);
                    }
                });
            }

        },
        error: function (ex) {
            alert(ex);
        }
    });

}