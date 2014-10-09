
$(document).ready(function () {


    $('a[id^="report"]').click(function () {
        
        showRelevantGraphForm(this.id);


    });



});



function showRelevantGraphForm(reportName) {

    var forms = $('div[id$="Form"]');
    for (var i = 0; i < forms.length; i++) {
        if (reportName + 'Form' == forms[i].id){
            forms[i].style.display = "inline-flex";
        }
        else{
            forms[i].style.display = "none";
        }
        
    }
}

function generateBusinessReport() {


    var startDate = document.getElementById('businessReport_startDate').value;
    var endDate = document.getElementById('businessReport_endDate').value;

    $.ajax({
        type: "POST",
        url: "Management/GenerateBusinessReport",
        data: JSON.stringify({ startDate:startDate, endDate:endDate}),
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

function showReport2() {


}

function showReport3() {


}

function showReport4() {


}