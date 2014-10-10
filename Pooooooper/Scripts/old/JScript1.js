
var currentWordSpan;
var categorizingType = "word";
var chosenWordsDivs = new Array();
var chosenRootExpression;
var addedExpressions = [];
var expressionsVsRootExpressions = [];
var rootExpressionIdsVsCategoryIds = [];
var categoriesVsAddresses = [];
var houseId;
var comment;
var streetsComment;
var allCategories = [];
var allRootExpressions = [];
var addedExpressions;
var rootExpressionsStringVsCategoryString;
var expressionsVsTypes;
var house;

$(document).ready(function () {

    var postExpressionsVsRootExpressionsString = document.getElementById("postExpressionsVsRootExpressions").value;
    if (postExpressionsVsRootExpressionsString == "") {
        postExpressionsVsRootExpressionsString = '{}';
    }

    var rootExpressionIdsVsCategoryIdsString = document.getElementById("rootExpressionIdsVsCategoryIds").value;
    if (rootExpressionIdsVsCategoryIdsString == "") {
        rootExpressionIdsVsCategoryIdsString = '{}';
    }

    var categoriesVsAddressesString = document.getElementById("categoriesVsAddresses").value;
    if (categoriesVsAddressesString == "") {
        categoriesVsAddressesString = '{}';
    }
    

    var expressionsVsTypesString = document.getElementById("postExpressionsVsTypes").value;
    if (expressionsVsTypesString == "") {
        expressionsVsTypesString = '{}';
    }

    var houseString = document.getElementById("house").value;
    if (expressionsVsTypesString == "") {
        expressionsVsTypesString = '{}';
    }
    

    house = JSON.parse(houseString);
    expressionsVsRootExpressions = JSON.parse(postExpressionsVsRootExpressionsString);
    rootExpressionIdsVsCategoryIds = JSON.parse(rootExpressionIdsVsCategoryIdsString);
    categoriesVsAddresses = JSON.parse(categoriesVsAddressesString);
    expressionsVsTypes = JSON.parse(expressionsVsTypesString);
    addedExpressions = JSON.parse('{}');

    rootExpressionsStringVsCategoryString = JSON.parse('{}');


    
    fillCategories();
    

    houseId = document.getElementById("houseId").value;
    comment = document.getElementById("comment").value;

});

function showLoading() {
    $("#loading").show();
}

function hideLoading() {
    $("#loading").hide();
}

function fillSummary() {

    document.getElementById("commentSpan").innerHTML = comment;
    //document.getElementById("streetsCommentSpan").innerHTML = streetsComment;

    var subAreas = '';
    var locations = '';
    for (var addressesKey in house.Addresses) {
        var currentAddress = house.Addresses[addressesKey];
        if (currentAddress.SubAreas.length > 0) {
            subAreas += '-- ';
            for (var subAreaKey in currentAddress.SubAreas) {
                var currentSubArea = currentAddress.SubAreas[subAreaKey];
                subAreas += ', ' + currentSubArea;
            }

            subAreas += '<br>';
        }
        else if (currentAddress.Locations.length > 0) {
            locations += '-- ';
            for (var locationKey in currentAddress.Locations) {
                var currentLocation = currentAddress.Locations[locationKey];
                locations += ', ' + currentLocation;
            }

            locations += '<br>';
        }

    }
    document.getElementById("subAreaSpan").innerHTML = subAreas;
    document.getElementById("locationSpan").innerHTML = locations;
    document.getElementById("locationSpan").style.color = "#00CC00";
    document.getElementById("subAreaSpan").style.color = "#00CC00";

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
            case "מס טלפון":
                categorySpan = document.getElementById("phoneNumberSpan");
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

function fillRootExpressions() {
    $.ajax({
        type: "POST",
        url: "OldExpressionStudy/GetAllRootExpressions",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            allRootExpressions = JSON.parse(data);
            fillCategoriesTreeAndSummary();
        },
        error: function (ex) {
            alert(ex);
        }
    });
}

function fillCategories() {
    $.ajax({
        type: "POST",
        url: "OldExpressionStudy/GetAllCategories",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            allCategories = JSON.parse(data);
            fillRootExpressions();
        },
        error: function(ex) {
            alert(ex);
        }
    });
}

function chooseWord(div){
    if (div.getAttribute("selected") == "true") {
        
        $("a[name='" + addedExpressions[div.value] + "'").click();
		chosenWordsDivs.splice(chosenWordsDivs.indexOf(div), 1);
		div.setAttribute("selected", "false");

        document.getElementById("currentExpression").value = "";
        
    }
    else{
        document.getElementById("currentExpression").value = div.innerHTML;
        if (chosenWordsDivs.length == 0) {
            if (div.innerHTML in addedExpressions) {
                $("a[name='" + addedExpressions[div.value] + "'").parent().parent().parent().children()[0].click()
                $("a[name='" + addedExpressions[div.value] + "'").click();
            }
            div.setAttribute("selected", "true");
            chosenWordsDivs.push(div);
        }
        else {
            var nextDiv = $('#' + chosenWordsDivs[chosenWordsDivs.length - 1].id).next()[0];
            if (nextDiv == div) {
                div.setAttribute("selected", "true");
                chosenWordsDivs.push(div);
                document.getElementById("currentExpression").value += " " + div.innerHTML;
            }
        }

        /*var spans = $("span").filter(function (idx) {
            return this.innerHTML.indexOf(expressionsVsRootExpressions[document.getElementById("currentExpression").value]) == 0;
        });

        spans.click();
    */
    
    }
};

function setCategory(barItem){
	if (barItem.getAttribute("selected") == "true"){
    	chosenCategoriesDivs.splice(chosenCategoriesDivs.indexOf(barItem), 1);
    }
    else{
    	if (chosenWordsDivs.length > 0){
		    chosenCategoriesDivs.push(barItem);
		    add();

			for (var i = 0; i < chosenWordsDivs.length; i++){
				chosenWordsDivs[i].setAttribute("selected", "false"); 
				chosenWordsDivs[i].style.backgroundColor =  document.defaultView.getComputedStyle(barItem,null).getPropertyValue("background-color");	    
			}
	
			chosenWordsDivs = [];
			chosenCategoriesDivs = [];
		}
	}
}

function addExpression() {
    var expression = chosenWordsDivs[0].innerHTML;
    chosenWordsDivs[0].style.backgroundColor = "#339966";
    for (var i = 1; i < chosenWordsDivs.length; i++) {
        expression = expression + " " + chosenWordsDivs[i].innerHTML;
    }

    chosenWordsDivs[0].innerHTML = expression;
    chosenWordsDivs[0].value = expression;
    for (var i = 1; i < chosenWordsDivs.length; i++) {
        chosenWordsDivs[i].remove();
    }

    addedExpressions[expression] = chosenRootExpression;
    //addedExpressions[expression] = chosenRootExpression;

    //$('span[selectedRoot=true]').click();
    $('.divi[selected=true]').click();
    chosenWordsDivs = new Array();
}

function fillCategoriesTreeAndSummary() {


        var primaryUL = document.getElementById("categoriesUL");
        for (var categoryId in allCategories) {



            //<li class="active"><a href="#">ניהול פרסומות</a></li><li><a href="#">ניהול פרסומות</a></li></ul>
            /*<li class="dropdown">
    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
      Dropdown <span class="caret"></span>
    </a>
    <ul class="dropdown-menu" role="menu">
      ...
    </ul>
  </li>*/
            var li = document.createElement('li');

            var a = document.createElement('a');



            a.innerHTML = allCategories[categoryId] + "<span class='caret'></span>";;
            a.href = "#";
            a.className = "dropdown-toggle";
            a.setAttribute("data-toggle", "dropdown")
            li.appendChild(a);

            var ul = document.createElement('ul');
            ul.className = "dropdown-menu";
            ul.setAttribute("role", "menu")
            ul.id = "category" + categoryId;
            ul.setAttribute("name", allCategories[categoryId]);
            li.appendChild(ul);

            li.className = "dropdown";

            primaryUL.appendChild(li);


        }
        

        /*for (var categoryId in categoriesVsAddresses) {

            var currentCategoryUl = document.getElementById("category" + categoryId);
            for (var currentAddress in categoriesVsAddresses[categoryId]) {
                
                var rootExpressionLi = document.createElement('li');
                rootExpressionLi.id = "root_" + categoriesVsAddresses[categoryId][currentAddress];
                rootExpressionLi.setAttribute('name', allRootExpressions[rootId]);

                /*var span = document.createElement('a');
                span.innerHTML = allRootExpressions[rootId];
                span.setAttribute('name', allRootExpressions[rootId]);
                
                rootExpressionLi.appendChild(span);
                currentCategoryUl.appendChild(rootExpressionLi);

                rootExpressionsStringVsCategoryString[allRootExpressions[rootId]] = currentCategoryUl.getAttribute("name");
            }
        }*/

        for (var rootId in rootExpressionIdsVsCategoryIds) {
            var currentCategoryUl = document.getElementById("category" + rootExpressionIdsVsCategoryIds[rootId]);
            var rootExpressionLi = document.createElement('li');
            rootExpressionLi.id = "root_" + rootId;
            rootExpressionLi.setAttribute('name', allRootExpressions[rootId]);
            var span = document.createElement('a');
            span.innerHTML = allRootExpressions[rootId];
            span.setAttribute('name',allRootExpressions[rootId]);
            //span.href = "#"
            rootExpressionLi.appendChild(span);
            currentCategoryUl.appendChild(rootExpressionLi);

            rootExpressionsStringVsCategoryString[allRootExpressions[rootId]] = currentCategoryUl.getAttribute("name");

        }
        /*for (var rootId in rootExpressionIdsVsCategoryIds){
            var currentCategoryLi = document.getElementById("category" + rootExpressionIdsVsCategoryIds[rootId]);
            var ul = document.createElement('ul');

            currentCategoryLi.appendChild(ul);

            var li = document.createElement('li');

            ul.appendChild(li);

            li.id = rootId;

            var span = document.createElement('span');
            span.innerHTML = allRootExpressions[rootId];
            span.style.cursor = "pointer";
            span.style.fontSize = "10pt";
            span.setAttribute("selectedRoot", "false");
            li.appendChild(span);

            rootExpressionsStringVsCategoryString[allRootExpressions[rootId]] = currentCategoryLi.getAttribute("name");
        }*/

        $('li[id^="root"] a').click(function () {
            if (chosenWordsDivs.length > 0) {
                if (this.getAttribute("selectedRoot") == "true") {
                    this.setAttribute("selectedRoot", "false");

                    this.parentElement.className = "";
                    chosenRootExpression = "";
                }
                else {
                    this.setAttribute("selectedRoot", "true");
                    this.parentElement.className ="active";
                    chosenRootExpression = this.innerHTML;
                }
            }
            else {
                this.setAttribute("selectedRoot", "true");
                this.parentElement.className = "active";
                chosenRootExpression = this.innerHTML;
            }
        });

        $('.dropdown-menu').on({
            "click": function (e) {
                e.stopPropagation();
            }
        });
        
        fillSummary();
    }



    function changeCategorizingType(type){

        var wordType = document.getElementById('wordCategorizing');
        var phraseType = document.getElementById('phraseCategorizing');
        if (type == wordType){
            phraseType.checked = false;		
            categorizingType = "word";
        }
        else if (type == phraseType){
            wordType.checked = false;
            categorizingType = "phrase";
        }
    }



    function add(){
        if (categorizingType == "word"){
            addWord();
        }
        else if (categorizingType == "phrase"){
            addPhrase();
        }
    }

    function addWord(){

        var word = chosenWordsDivs[0].innerHTML;
        var category = chosenCategoriesDivs[0].innerHTML;
	
        wordsResults.innerHTML += word + " - " + category + '<br>';
    }

    function addPhrase(){

        var firstWord = chosenWordsDivs[0].innerHTML;
        var secondWord = chosenWordsDivs[1].innerHTML;
        var category = chosenCategoriesDivs[0].innerHTML;
	
        phrasesResults.innerHTML += firstWord + " " + secondWord + " - " + category + '<br>';
    }

    function savePostExpressions() {

        var restJson = JSON.stringify({houseId:houseId, cityId:document.getElementById('citySelect')[document.getElementById('citySelect').selectedIndex].value});
        var expressionsJson = JSON.stringify(addedExpressions);
        var rootJson = JSON.stringify(rootExpressionIdsVsCategoryIds);
        showLoading();
        $.ajax({
            type: "POST",
            url: "OldExpressionStudy/SaveExpressionsAndGetNewPost",
            data: JSON.stringify({ expressionsVsRootExpressions: expressionsJson, rootExpressionIdsVsCategoryIds: rootJson, rest: restJson }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: successFunc,
            error: errorFunc
        });
    }

    function addBug() {
        var bugComment = document.getElementById('bugComment').value;
        var houseIdJson = JSON.stringify({ houseId: houseId, comment: bugComment });
        var expressionsJson = JSON.stringify(addedExpressions);
        var rootJson = JSON.stringify(rootExpressionIdsVsCategoryIds);
        showLoading();
        $.ajax({
            type: "POST",
            url: "OldExpressionStudy/AddBugAndGetNewPost",
            data: JSON.stringify({ expressionsVsRootExpressions: expressionsJson, rootExpressionIdsVsCategoryIds: rootJson, houseId: houseIdJson }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });
    }

    function successFunc(data, status) {
        clearPostElements();
        data = JSON.parse(data);
        if (data != null) {
            document.getElementById('houseId').value = data.id;
            houseId = document.getElementById('houseId').value;

            document.getElementById('comment').value = data.Comment;
            document.getElementById('commentSpan').innerHTML = data.Comment;
            comment = document.getElementById('comment').value;

            document.getElementById('streetsComment').value = data.StreetsComment;
            //document.getElementById('streetsCommentSpan').innerHTML = data.StreetsComment;
            streetsComment = document.getElementById('streetsComment').value;

            document.getElementById('streets_found').innerHTML = data.StreetsFound;
            document.getElementById('locations_found').innerHTML = data.LocationsFound;
            document.getElementById('subareas_found').innerHTML = data.SubAreasFound;
            document.getElementById('areas_found').innerHTML = data.AreasFound;
            document.getElementById('neighborhoods_found').innerHTML = data.NeighborhoodsFound;

            document.getElementById('postExpressionsVsTypes').value = data.ExpressionsVsTypesJson;
            expressionsVsTypes = JSON.parse(document.getElementById("postExpressionsVsTypes").value);

            house = data;

            addedExpressions = JSON.parse('{}');
            // Setting post text in label
            document.getElementById('originalMessage').innerHTML = data.Message;

            // Post Words
            var postWordsDiv = document.getElementById('postWords');
            for (var i = 0; i < data.MessageWords.length; i++) {

                var currentWordDiv = document.createElement('div');
                currentWordDiv.id = 'word' + i + 1;
                currentWordDiv.className = 'divi';
                currentWordDiv.onclick = function () { chooseWord(this); }
                currentWordDiv.setAttribute("selected", "false");

                currentWordDiv.style.marginLeft = "3px";
                currentWordDiv.innerHTML = data.MessageWords[i];

                postWordsDiv.appendChild(currentWordDiv);

                if (i % 15 == 0 && i != 0) {
                    var br = document.createElement("br");
                    postWordsDiv.appendChild(br);
                }
            }

            fillSummary();
        }
        else {

        }

        hideLoading();
    }

    function errorFunc(ms) {
        if (ms.status = 200) { // no more posts
            clearPostElements();

            var postWordsDiv = document.getElementById('postWords');
            var currentWordDiv = document.createElement('div');
            currentWordDiv.innerHTML = "אין יותר פוסטים";
            currentWordDiv.style.fontSize = "20pt";
            postWordsDiv.appendChild(currentWordDiv);
        }
    }

    function clearPostElements() {
        document.getElementById('bugComment').value = '';
        var postWordsDiv = document.getElementById('postWords');
        while (postWordsDiv.firstChild) {
            postWordsDiv.removeChild(postWordsDiv.firstChild);
        }

        document.getElementById('originalMessage').innerHTML = "";
        $('a[selectedRoot=true]').click();

        chosenWordsDivs = new Array();

        $.each($('#types span'), function (index, value) {
            if (index % 2 == 1) {
                value.innerHTML = "x";
                value.style.color = "Red";
            }
        });
    }
