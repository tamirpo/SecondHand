
var currentWordSpan;
var categorizingType = "word";
var chosenWordsDivs = new Array();
var chosenRootExpression;
var addedExpressions = [];
var expressionsVsRootExpressions = [];
var rootExpressionIdsVsCategoryIds = [];
var houseId;
var comment;
var streetsComment;
var allCategories = [];
var allRootExpressions = [];
var addedExpressions;
var rootExpressionsStringVsCategoryString;
var expressionsVsTypes;
var cityId;
var cityIdJson;

$(document).ready(function () {

    var expressionsVsTypesString = document.getElementById("postExpressionsVsTypes").value;
    if (expressionsVsTypesString == "") {
        expressionsVsTypesString = '{}';
    }
    expressionsVsTypes = JSON.parse(expressionsVsTypesString);

    fillSummary();

    if (document.getElementById("has_more_than_one_sub_area").value == 1) {
        document.getElementById("address_form").style.visibility = "visible";
    }

    cityId = document.getElementById("selectedCity").options[document.getElementById("selectedCity").selectedIndex].value;
    cityIdJson = JSON.stringify({ cityId: cityId });

    getNeighborhoods();
    getLocations();

});

function getLocations() {
    cityId = document.getElementById("selectedCity").options[document.getElementById("selectedCity").selectedIndex].value;
    cityIdJson = JSON.stringify({ cityId: cityId });

    $.ajax({
        type: "POST",
        url: "House/GetLocations",
        data: cityIdJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var select = document.getElementById("selectedLocations");
            select.innerHTML = "";

            var locationsIdVsName = JSON.parse(data);
            for (currentLocationId in locationsIdVsName) {
                var option = document.createElement("option");
                option.text = locationsIdVsName[currentLocationId];
                option.value = currentLocationId;
                select.add(option, false);
            }

            var option = document.createElement("option");
            option.text = "כלום";
            option.value = "-1";
            select.add(option, false);
        },
        error: function (ex) {
            alert(ex);
        }
    });
}

function getNeighborhoods() {
    cityId = document.getElementById("selectedCity").options[document.getElementById("selectedCity").selectedIndex].value;
    cityIdJson = JSON.stringify({ cityId: cityId });

    $.ajax({
        type: "POST",
        url: "House/GetNeighborhoods",
        data: cityIdJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var select = document.getElementById("selectedNeighborhoods");
            select.innerHTML = "";

            var neighborhoodsIdVsName = JSON.parse(data);
            for (currentNeighborhoodId in neighborhoodsIdVsName) {
                var option = document.createElement("option");
                option.text = neighborhoodsIdVsName[currentNeighborhoodId];
                option.value = currentNeighborhoodId;
                select.add(option, false);
            }

            var option = document.createElement("option");
            option.text = "כלום";
            option.value = "-1";
            select.add(option, false);
        },
        error: function (ex) {
            alert(ex);
        }
    });

}

function updateHouseAddress() {
    var houseId = document.getElementById("houseId").value;

    var selectNeighborhoods = document.getElementById("selectedNeighborhoods");
    selectedNeighborhoods = new Array();
    for (var i = 0; i < selectNeighborhoods.options.length; i++)
        if (selectNeighborhoods.options[i].selected)
            selectedNeighborhoods.push(selectNeighborhoods.options[i].value);

    var selectLocations = document.getElementById("selectedLocations");
    selectedLocations = new Array();
    for (var i = 0; i < selectLocations.options.length; i++)
        if (selectLocations.options[i].selected)
            selectedLocations.push(selectLocations.options[i].value);

    var selectedLocationsJson = JSON.stringify(selectedLocations);
    var selectedNeighborhoodsJson = JSON.stringify(selectedNeighborhoods);
    var params = JSON.stringify({ houseId: houseId, selectedNeighborhoods : selectedNeighborhoodsJson, selectedLocations: selectedLocationsJson });


    $.ajax({
        type: "POST",
        url: "House/UpdateHouseAddress",
        data: params,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // CLEAR ALL AND WRITE SUCCESS
            var tr = document.getElementById("body");
            tr.innerHTML = "";
            tr.innerHTML = "<td> Success </td>";

            var addressForm = document.getElementById("address_form");
            addressForm.innerHTML = "";
        },
        error: function (ex) {
            alert(ex);
        }
    });
}

function fillSummary() {


    for (var rootExpression in expressionsVsTypes) {
        var type = expressionsVsTypes[rootExpression];
        var categorySpan;
        categorySpan;
        switch (type) {
            case "עיר":
                categorySpan = document.getElementById("citySpan");
                break;
            case "סוג חיפוש":
                categorySpan = document.getElementById("purposeSpan");
                break;
            case "מספר חדרים":
                categorySpan = document.getElementById("roomsNumberSpan");
                break;
            case "תת איזור":
                categorySpan = document.getElementById("subAreaSpan");
                break;
            case "מחיר":
                categorySpan = document.getElementById("priceSpan");
                break;
            case "חניה":
                categorySpan = document.getElementById("parkingSpan");
                break;
            case "משופצת":
                categorySpan = document.getElementById("renovatedSpan");
                break;
            case "קומה":
                categorySpan = document.getElementById("floorSpan");
                break;
            case "מספר שותפים סהכ":
                categorySpan = document.getElementById("totalRoommatesSpan");
                break;
            case "רחוב":
                categorySpan = document.getElementById("streetSpan");
                break;
            case "צד":
                categorySpan = document.getElementById("sideSpan");
                break;
            case "סוג בית":
                categorySpan = document.getElementById("typeSpan");
                break;
            case "מחיר":
                categorySpan = document.getElementById("priceSpan");
                break;
            case "גודל":
                categorySpan = document.getElementById("sizeSpan");
                break;
            case "לוקיישן":
                categorySpan = document.getElementById("locationSpan");
                break;
            case "בעלי חיים":
                categorySpan = document.getElementById("petsSpan");
                break;
            case "מרפסת":
                categorySpan = document.getElementById("balconySpan");
                break;
            case "מעלית":
                categorySpan = document.getElementById("elevatorSpan");
                break;
            case "מרוהטת":
                categorySpan = document.getElementById("furnituredSpan");
                break;
            case "עישון":
                categorySpan = document.getElementById("smokeSpan");
                break;
            case "סאבלט":
                categorySpan = document.getElementById("subletSpan");
                break;
            case "מתיווך":
                categorySpan = document.getElementById("fromAgencySpan");
                break;
            default:

        }

        if (categorySpan.innerHTML != "x") {
            categorySpan.innerHTML += ", " + rootExpression;
        }
        else {
            categorySpan.innerHTML = rootExpression;
            categorySpan.style.color = "#00CC00";
        }
    }
}

