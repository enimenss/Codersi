function DrawDropdownUsers() {
    $('<div class="form-group" id="dropdownUsersTable"></div>').appendTo('#addUsers');
    //$('<div class="form-group"> <label id="labelFilter"/>').text('Filter:').appendTo('#dropdownInvoice')
    $select = $('<select id="selectUserstable" class="form-control" />').appendTo('#dropdownUsersTable');
    $('<option/>').val('0').text('Select filter..').appendTo($select);
    $('<option/>').val('1').text('Name').appendTo($select);
    $('<option/>').val('2').text('Surname').appendTo($select);
    $('<option/>').val('3').text('Username').appendTo($select);
    $('<option/>').val('4').text('E-mail').appendTo($select);
}

function DrawDropdownCompetitions() {
    $('<div class="form-group" id="dropdownCompetitionsTable"></div>').appendTo('#addCompetitions');
    //$('<div class="form-group"> <label id="labelFilter"/>').text('Filter:').appendTo('#dropdownInvoice')
    $select = $('<select id="selectCompetitionsTable" class="form-control" />').appendTo('#dropdownCompetitionsTable');
    $('<option/>').val('0').text('Select filter..').appendTo($select);
    $('<option/>').val('1').text('Number').appendTo($select);
    $('<option/>').val('2').text('Name').appendTo($select);
    $('<option/>').val('3').text('Starting date').appendTo($select);
    $('<option/>').val('4').text('Ending date').appendTo($select);
    $('<option/>').val('5').text('Type').appendTo($select);
    $('<option/>').val('6').text('Citye').appendTo($select);
    $('<option/>').val('7').text('No of landmark').appendTo($select);
}

function DrawDropdownLandmarks() {
    $('<div class="form-group" id="dropdownLandmarksTable"></div>').appendTo('#addLandmarks');
    //$('<div class="form-group"> <label id="labelFilter"/>').text('Filter:').appendTo('#dropdownInvoice')
    $select = $('<select id="selectLandmarksTable" class="form-control" />').appendTo('#dropdownLandmarksTable');
    $('<option/>').val('0').text('Select filter..').appendTo($select);
    $('<option/>').val('1').text('Number').appendTo($select);
    $('<option/>').val('2').text('Name').appendTo($select);
    $('<option/>').val('3').text('City').appendTo($select);
}

function UserDetails(button) {
    var rowUser = $(button).parents('tr')[0];
    var data = $('#tableUsers').DataTable().row(rowUser).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("User details");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    modalBody.append("<p>Name: " + data.Named + "</p>");
    modalBody.append("<p>Surname: " + data.Surname + "</p>");
    modalBody.append("<p>Username: " + data.Username + "</p>");
    modalBody.append("<p>Password: " + data.Pass + "</p>");
    modalBody.append("<p>E-mail: " + data.Email + "</p>");
    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>Close</button>");
    popUpBody.modal("show");
}

function CompetitionDetails(button) {
    var rowCompetition = $(button).parents('tr')[0];
    var data = $('#tableCompetitions').DataTable().row(rowCompetition).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Competition details");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    modalBody.append("<p>Name: " + data.Named + "</p>");
    modalBody.append("<p>Starting date: " + data.StartingDate + "</p>");
    modalBody.append("<p>EndingDate: " + data.EndingDate + "</p>");
    modalBody.append("<p>Type: " + data.Type + "</p>");
    modalBody.append("<p>Number of landmarks: " + data.LandmarkCount + "</p>");
    modalBody.append("<p>City: " + data.City + "</p>");
    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>Close</button>");
    modalFooter.append("<button value=" + id + " onclick='DeleteCompetition(this)' class='btn btn-danger'>Delete competition</button>");
    modalFooter.append("<button value=" + id + " onclick='ChangeCompetition(this)' class='btn btn-danger'>Change competition</button>");
    popUpBody.modal("show");
}

function LandmarkDetails(button) {
    var rowLandmark = $(button).parents('tr')[0];
    var data = $('#tableLandmars').DataTable().row(rowLandmark).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Landmark details");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    modalBody.append("<p>Name: " + data.Named + "</p>");
    modalBody.append("<p>City: " + data.City + "</p>");
    modalBody.append("<p>Location: " + "" + "</p>");

    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>Close</button>");
    modalFooter.append("<button value=" + id + " onclick='DeleteLandmark(this)' class='btn btn-danger'>Delete landmark</button>");
    popUpBody.modal("show");
}

function CloseModal() {
    $('#AdminModalDetails').modal('hide');
}

function DeleteUser(button) {
    var id = button.value;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    modalBody.append("<p>Are you sure you want to delete this user?</p>");
    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>No</button>");
    popUpBody.modal("show");
    modalFooter.append("<button value=" + id + " onclick='DeleteUserYes(this)' class='btn btn-danger'>Yes</button>");
}

function DeleteUserYes(button) {
    var id = button.value;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "id": id },
        url: "/Admin/DeleteUser",
        chache: false,
        success: function (data) {
            var modalHeader = $('#headerModal');
            modalHeader.empty();
            var popUpBody = $('#AdminModalDetails');
            var modalBody = $("#editDetails");
            modalBody.empty();
            modalBody.append("<p>User deleted! </p>");
            var modalFooter = $("#modalEdit");
            modalFooter.empty();
            modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>OK</button>");
            popUpBody.modal("show");
            $('#tableUsers').DataTable().ajax.reload().draw();
        }
    });
}

function DeleteCompetition(button) {
    var id = button.value;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    modalBody.append("<p>Are you sure you want to delete this competition?</p>");
    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>No</button>");
    popUpBody.modal("show");
    modalFooter.append("<button value=" + id + " onclick='DeleteCompetitionYes(this)' class='btn btn-danger'>Yes</button>");
}

function DeleteCompetitionYes(button) {
    var id = button.value;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "id": id },
        url: "/Admin/DeleteCompetition",
        chache: false,
        success: function (data) {
            CloseModal();
            var modalHeader = $('#headerModal');
            modalHeader.empty();
            var popUpBody = $('#AdminModalDetails');
            var modalBody = $("#editDetails");
            modalBody.empty();
            modalBody.append("<p>Competition deleted! </p>");
            var modalFooter = $("#modalEdit");
            modalFooter.empty();
            modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>OK</button>");
            popUpBody.modal("show");
            $('#tableCompetitions').DataTable().ajax.reload().draw();
        }
    });
}

function DeleteLandmark(button) {
    var id = button.value;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    modalBody.append("<p>Are you sure you want to delete this landmark?</p>");
    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>No</button>");
    popUpBody.modal("show");
    modalFooter.append("<button value=" + id + " onclick='DeleteLandmarkYes(this)' class='btn btn-danger'>Yes</button>");
}

function DeleteLandmarkYes(button) {
    var id = button.value;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "id": id },
        url: "/Admin/DeleteLandmark",
        chache: false,
        success: function (data) {
            CloseModal();
            var modalHeader = $('#headerModal');
            modalHeader.empty();
            var popUpBody = $('#AdminModalDetails');
            var modalBody = $("#editDetails");
            modalBody.empty();
            modalBody.append("<p>Landmark deleted! </p>");
            var modalFooter = $("#modalEdit");
            modalFooter.empty();
            modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>OK</button>");
            popUpBody.modal("show");
            $('#tableLandmarks').DataTable().ajax.reload().draw();
        }
    });
}

function ChangeCompetition(button) {
    CloseModal();
    var rowCompetition = $(button).parents('tr')[0];
    var data = $('#tableCompetitions').DataTable().row(rowCompetition).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Change competition");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    $("<form action='' id='formCompetition'></form>").appendTo(modalBody);
    $("<div col-md-6' style='float:left' id='left'></div>").appendTo($('#formCompetition'));
    $("<div col-md-6' style='float:right' id='right'></div>").appendTo($('#formCompetition'));

    $("<div class='form-group' id='DivNumber'></div>").appendTo($('#left'));
    $('#DivNumber').append("<label class='col-md-5'>Number: </label>");
    $('#DivNumber').append("<input class='col-md-6' type='text' id='Id'/><br />");
    $('#Id').val(data.Id);

    $("<div class='form-group' id='DivName'></div>").appendTo($('#left'));
    $('#DivName').append("<label class='col-md-5'>Name: </label>");
    $('#DivName').append("<input class='col-md-6' type='text' id='Name'/><br />");
    $('#Name').val(data.Name);

    $("<div class='form-group' id='DivStartDate'></div>").appendTo($('#left'));
    $('#DivStartDate').append("<label class='col-md-5'>Starting date: </label>");
    $('#DivStartDate').append("<input class='col-md-6' type='text' id='StartingDate'/><br />");
    $('#StartingDate').val(data.StartingDate);

    $("<div class='form-group' id='DivEndDate'></div>").appendTo($('#left'));
    $('#DivEndDate').append("<label class='col-md-5'>Ending date: </label>");
    $('#DivEndDate').append("<input type='text' class='col-md-6' id='EndingDate' /><br />");
    $('#EndingDate').val(data.EndingDate);

    $("<div class='form-group' id='DivType'></div>").appendTo($('#right'));
    $('#DivType').append("<label class='col-md-5'>Type: </label>");
    $('#DivType').append("<input class='col-md-6' type='text' id='Type'/><br />");
    $('#Type').val(data.Type);

    $("<div class='form-group' id='DivNoLand'></div>").appendTo($('#right'));
    $('#DivNoLand').append("<label class='col-md-5'>Landmarks: </label>");
    $('#DivNoLand').append("<input class='col-md-6' type='text' id='LandmarkCount'/><br />");
    $('#LandmarkCount').val(data.LandmarkCount);

    $("<div class='form-group' id='DivCity'></div>").appendTo($('#right'));
    $('#DivCity').append("<label class='col-md-5'>City: </label>");
    $('#DivCity').append("<input class='col-md-6' type='text' id='City'/><br />");
    $('#City').val(data.City);

    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>Close</button>");
    modalFooter.append("<button value=" + id + " onclick='ChangeCompetitionYes(this)' class='btn btn-danger'>Change </button>");
    popUpBody.modal("show");
}

function ChangeCompetitionYes(button) {
    var id = button.value;
    competitionModel = new FormData();
    var competitionForm = $('formCompetition').serializeArray();
    var formObject = {};
    for (var i = 0; i < competitionForm.length; i++) {
        formObject[competitionForm[i]['name']] = competitionForm[i]['value'];
    }
    for (var key in formObject) {
        competitionModel.append(key, formObject[key]);
    }

    $.ajax({
        type: "POST",
        dataType: "json",
        data: competitionModel,
        url: "/Admin/ChangeCompetition",
        chache: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
            $('#tableCompetitions').DataTable().ajax.reload().draw();
        }
    });
}

function ChangeLandmark(button) {
    var rowCompetition = $(button).parents('tr')[0];
    var data = $('#tableLandmarks').DataTable().row(rowCompetition).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Change landmark");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    //modalBody.append("<label>Name: </label>");
    //modalBody.append("<input type='text' id='LandName'/>");
    //modalBody.append("<label>City: </label>");
    //modalBody.append("<input type='text' id='CompCity'/>");
    //var modalFooter = $("#modalEdit");

    $("<form action='' id='formLandmark'></form>").appendTo(modalBody);
    $("<div col-md-6' style='float:left' id='left'></div>").appendTo($('#formLandmark'));
    $("<div col-md-6' style='float:right' id='right'></div>").appendTo($('#formLandmark'));

    $("<div class='form-group' id='DivNumber'></div>").appendTo($('#left'));
    $('#DivNumber').append("<label class='col-md-5'>Number: </label>");
    $('#DivNumber').append("<input class='col-md-6' type='text' id='Id'/><br />");
    $('#Id').val(data.Id);

    $("<div class='form-group' id='DivName'></div>").appendTo($('#left'));
    $('#DivName').append("<label class='col-md-5'>Name: </label>");
    $('#DivName').append("<input class='col-md-6' type='text' id='Name'/><br />");
    $('#Name').val(data.Name);

    $("<div class='form-group' id='DivCity'></div>").appendTo($('#right'));
    $('#DivCity').append("<label class='col-md-5'>City: </label>");
    $('#DivCity').append("<input class='col-md-6' type='text' id='City'/><br />");
    $('#City').val(data.City);

    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>Close</button>");
    modalFooter.append("<button value=" + id + " onclick='ChangeCompetitionYes(this)' class='btn btn-danger'>Change competition</button>");
    popUpBody.modal("show");
}

//function ChangeCompetitionYes(button) {
//    var id = button.value;
//    $.ajax({
//        type: "POST",
//        dataType: "json",
//        data: { "id": id },
//        url: "/Admin/ChangeCompetition",
//        chache: false,
//        success: function (data) {
//            $('#AdminModalDetails').modal('hide');
//            $('#tableCompetitions').DataTable().ajax.reload().draw();
//        }
//    });
//}