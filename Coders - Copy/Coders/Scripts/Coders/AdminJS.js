function DrawDropdownUsers() {
    $('<div class="form-group" style="float:right" id="dropdownUsersTable"></div>').appendTo('#tableUsers_filter');
    //$('<div class="form-group"> <label id="labelFilter"/>').text('Filter:').appendTo('#dropdownInvoice')
    $select = $('<select id="selectUserstable" style="background-color: #e8d2a2" class="form-control" />').appendTo('#dropdownUsersTable');
    $('<option/>').val('0').text('Select filter..').appendTo($select);
    $('<option/>').val('1').text('Name').appendTo($select);
    $('<option/>').val('2').text('Surname').appendTo($select);
    $('<option/>').val('3').text('Username').appendTo($select);
    $('<option/>').val('4').text('E-mail').appendTo($select);
}

function DrawDropdownCompetitions() {
    $('<div class="form-group" style="float:right" id="dropdownCompetitionsTable"></div>').appendTo('#tableCompetitions_filter');
    //$('<div class="form-group"> <label id="labelFilter"/>').text('Filter:').appendTo('#dropdownInvoice')
    $select = $('<select id="selectCompetitionsTable" style="background-color: #e8d2a2; float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownCompetitionsTable');
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
    $('<div class="form-group" style="float:right" id="dropdownLandmarksTable"></div>').appendTo('#tableLandmarks_filter');
    //$('<div class="form-group"> <label id="labelFilter"/>').text('Filter:').appendTo('#dropdownInvoice')
    $select = $('<select id="selectLandmarksTable" style="float:right; background-color: #e8d2a2; margin-left:2%" class="form-control" />').appendTo('#dropdownLandmarksTable');
    $('<option/>').val('0').text('Select filter..').appendTo($select);
    $('<option/>').val('1').text('Number').appendTo($select);
    $('<option/>').val('2').text('Name').appendTo($select);
    $('<option/>').val('3').text('City').appendTo($select);
}

function CloseModal() {
    $('#AdminModalDetails').modal('hide');
}

function DeleteUser(button, event) {
    event.stopPropagation();
    var rowCompetition = $(button).parents('tr')[0];
    var data = $('#tableUsers').DataTable().row(rowCompetition).data();
    var id = data.Id;
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

function DeleteCompetition(button, event) {
    event.stopPropagation();
    var rowCompetition = $(button).parents('tr')[0];
    var data = $('#tableCompetitions').DataTable().row(rowCompetition).data();
    var id = data.Id;
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
    event.preventDefault();
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
            $('#AdminModalDetails').modal('hide');
            $('#tableCompetitions').DataTable().ajax.reload().draw();
        }
    });
}

function DeleteLandmark(button, event) {
    event.stopPropagation();
    var rowCompetition = $(button).parents('tr')[0];
    var data = $('#tableLandmarks').DataTable().row(rowCompetition).data();
    var id = data.Id;
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
            $('#AdminModalDetails').modal('hide');
            $('#tableLandmarks').DataTable().ajax.reload().draw();
        }
    });
}

function ChangeCompetition(button, event) {
    event.stopPropagation();
    var rowCompetition = $(button).parents('tr')[0];
    var data = $('#tableCompetitions').DataTable().row(rowCompetition).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Change competition");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivName'></div>").appendTo(modalBody);
    $('#DivName').append("<label class='col-md-5'>Name: </label>");
    $('#DivName').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-home'></i></span><input class='col-md-6 form-control' type='text' id='Name'/><br />");
    $('#Name').val(data.Name);

    $("<div class='form-group' id='DivStartDate'></div>").appendTo(modalBody);
    $('#DivStartDate').append("<label class='col-md-5'>Starting date: </label>");
    $('#DivStartDate').append("<input class='col-md-6 form-control' type='date' id='StartingDate'/> <br /><br />");
    $('#StartingDate')[0].value = data.StartingDate;

    $("<div class='form-group' id='DivEndDate'></div>").appendTo(modalBody);
    $('#DivEndDate').append("<label class='col-md-5'>Ending date: </label>");
    $('#DivEndDate').append("<input type='date' class='col-md-6 form-control' id='EndingDate' /> <br /><br />");
    $('#EndingDate')[0].value = data.EndingDate;

    $("<div class='form-group' id='DivType'></div>").appendTo(modalBody);
    $('#DivType').append("<label class='col-md-5'>Type: </label>");
    $('<div class="form-group col-md-6" id="dropdownTypes"></div>').appendTo('#DivType');
    $select = $('<select id="selectTypes" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownTypes');
    $('<option/>').val('0').text('Select type..').appendTo($select);
    $('<option/>').val('1').text('All').appendTo($select);
    $('<option/>').val('2').text('Museums').appendTo($select);
    $('<option/>').val('3').text('Sights').appendTo($select);
    $('<option/>').val('4').text('Taverns').appendTo($select);
    $('<option/>').val('5').text('Clubs').appendTo($select);
    $('<option/>').val('6').text('Pubs').appendTo($select);
    $('#selectTypes')[0].selectedIndex = data.Type;

    $("<div class='form-group' id='DivCity'></div>").appendTo(modalBody);
    $('#DivCity').append("<label class='col-md-5'>City: </label>");
    $('<div class="form-group col-md-6" id="dropdownCities"></div>').appendTo('#DivCity');
    $select = $('<select id="selectCities" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownCities');
    $('<option/>').val('0').text('Select city..').appendTo($select);
    $('<option/>').val('1').text('Nis').appendTo($select);
    $('<option/>').val('2').text('Beograd').appendTo($select);
    $('<option/>').val('3').text('London').appendTo($select);
    $('#selectCities')[0].selectedIndex = data.Type;

    $("<div class='form-group' id='DivNoLand'></div>").appendTo(modalBody);
    $('#DivNoLand').append("<label class='col-md-5'>Landmarks: </label>");
    $('#DivNoLand').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-tasks'></i></span><input class='col-md-6 form-control' type='text' id='LandmarkCount'/><br />");
    $('#LandmarkCount').val(data.LandmarkCount);

    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button>");
    modalFooter.append("<button value=" + id + " onclick='ChangeCompetitionYes(this)' class='btn btn-danger'><span class='glyphicon glyphicon-edit'></span> Change </button>");
    popUpBody.modal("show");
}

function ChangeCompetitionYes(button) {
    var id = button.value;
    var name = $('#Name').val();
    var startdate = $('#StartingDate').val();
    var enddate = $('#EndingDate').val();
    var type = $('#selectTypes')[0].selectedIndex;
    var landmarkcount = $('#LandmarkCount').val();
    var city = $('#selectCities')[0].selectedIndex;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "id": id, "name": name, "startdate": startdate, "enddate": enddate, "city": city, "landmarkcount": landmarkcount, "type": type },
        url: "/Admin/ChangeCompetition",
        chache: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
            $('#tableCompetitions').DataTable().ajax.reload().draw();
        }
    });
}

function ChangeLandmark(button, event) {
    event.stopPropagation();
    var rowlandmark = $(button).parents('tr')[0];
    var data = $('#tableLandmarks').DataTable().row(rowlandmark).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Change landmark");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group col-md-6' id='DivImage'></div>").appendTo(modalBody);
    $('#DivImage').append("<img style='width:100ph; height:100px; margin:auto' src='" + ServerAddres + "Images/" + data.FilePathLandmark + "'>");

    $("<div class='form-group' id='DivNameL'></div>").appendTo(modalBody);
    $('#DivNameL').append("<label class='col-md-5'>Name: </label>");
    $('#DivNameL').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-home'></i></span><input class='col-md-6 form-control' type='text' id='NameL'/><br />");
    $('#NameL').val(data.Name);
   
    $("<div class='form-group' id='DivCity'></div>").appendTo(modalBody);
    $('#DivCity').append("<label class='col-md-5'>City: </label>");
    $('<div class="form-group col-md-6" id="dropdownCities"></div>').appendTo('#DivCity');
    $select = $('<select id="selectCities" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownCities');
    $('<option/>').val('0').text('Select city..').appendTo($select);
    $('<option/>').val('1').text('Nis').appendTo($select);
    $('<option/>').val('2').text('Beograd').appendTo($select);
    $('<option/>').val('3').text('London').appendTo($select);
    $('#selectCities')[0].selectedIndex = data.City;

    var modalFooter = $('#modalEdit');
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button>");
    modalFooter.append("<button value=" + id + " onclick='ChangeLandmarkYes(this)' class='btn btn-danger'> <span class='glyphicon glyphicon-edit'></span> Change</button>");
    popUpBody.modal("show");
}

function ChangeLandmarkYes(button) {
    var id = button.value;
    var city = $('#selectCities')[0].selectedIndex
    var name = $('#NameL').val();

    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "id": id, "city" : city, "name" : name },
        url: "/Admin/ChangeLandmark",
        chache: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
            $('#tableLandmarks').DataTable().ajax.reload().draw();
        }
    });
}

function DetailsUser(data) {
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("User details");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivImage'></div>").appendTo(modalBody);
    $('#DivImage').append("<img style='margin:auto; width:100px; height:100px;' src='" + ServerAddres + "/Images/" + data.FilePath + "'>");
    $("<div col-md-6' style='float:left' id='left'></div>").appendTo(modalBody);
    $("<div col-md-6' style='float:right' id='right'></div>").appendTo(modalBody);

    $("<div class='form-group' id='DivName'></div>").appendTo($('#left'));
    $('#DivName').append("<label class='col-md-5'>Name: </label>");
    $('#DivName').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-user'></i></span><input class='col-md-6 form-control' type='text' id='Name' disabled /><br />");
    $('#Name').val(data.Named);

    $("<div class='form-group' id='DivSurname'></div>").appendTo($('#left'));
    $('#DivSurname').append("<label class='col-md-5'>Surname: </label>");
    $('#DivSurname').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-user'></i></span><input class='col-md-6 form-control' type='text' id='Surname' disabled /><br />");
    $('#Surname').val(data.Surname);

    $("<div class='form-group' id='DivUsername'></div>").appendTo($('#right'));
    $('#DivUsername').append("<label class='col-md-5'>Username: </label>");
    $('#DivUsername').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-user'></i></span><input type='text' class='col-md-6 form-control' id='Username' disabled /><br />");
    $('#Username').val(data.Username);

    $("<div class='form-group' id='DivPassword'></div>").appendTo($('#right'));
    $('#DivPassword').append("<label class='col-md-5'>Password: </label>");
    $('#DivPassword').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-user'></i></span><input class='col-md-6 form-control' type='text' id='Password' disabled /><br />");
    $('#Password').val(data.Password);

    $("<div class='form-group' id='DivEmail'></div>").appendTo($('#left'));
    $('#DivEmail').append("<label class='col-md-5'>E-mail: </label>");
    $('#DivEmail').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-user'></i></span><input class='col-md-6 form-control' type='text' id='Email' disabled /><br />");
    $('#Email').val(data.Email);

    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-thumbs-up'></span> OK</button>");
    popUpBody.modal("show");
}

function DetailsCompetition(data) {
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Change competition");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivName'></div>").appendTo(modalBody);
    $('#DivName').append("<label class='col-md-5'>Name: </label>");
    $('#DivName').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-home'></i></span><input class='col-md-6 form-control' type='text' id='Name'/><br />");
    $('#Name').val(data.Name);

    $("<div class='form-group' id='DivStartDate'></div>").appendTo(modalBody);
    $('#DivStartDate').append("<label class='col-md-5'>Starting date: </label>");
    $('#DivStartDate').append("<input class='col-md-6 form-control' type='date' id='StartingDate'/> <br /><br />");
    $('#StartingDate')[0].value = data.StartingDate;

    $("<div class='form-group' id='DivEndDate'></div>").appendTo(modalBody);
    $('#DivEndDate').append("<label class='col-md-5'>Ending date: </label>");
    $('#DivEndDate').append("<input type='date' class='col-md-6 form-control' id='EndingDate' /> <br /><br />");
    $('#EndingDate')[0].value = data.EndingDate;

    $("<div class='form-group' id='DivType'></div>").appendTo(modalBody);
    $('#DivType').append("<label class='col-md-5'>Type: </label>");
    $('<div class="form-group col-md-6" id="dropdownTypes"></div>').appendTo('#DivType');
    $select = $('<select id="selectTypes" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownTypes');
    $('<option/>').val('0').text('Select type..').appendTo($select);
    $('<option/>').val('1').text('All').appendTo($select);
    $('<option/>').val('2').text('Museums').appendTo($select);
    $('<option/>').val('3').text('Sights').appendTo($select);
    $('<option/>').val('4').text('Taverns').appendTo($select);
    $('<option/>').val('5').text('Clubs').appendTo($select);
    $('<option/>').val('6').text('Pubs').appendTo($select);
    $('#selectTypes')[0].selectedIndex = data.Type;

    $("<div class='form-group' id='DivCity'></div>").appendTo(modalBody);
    $('#DivCity').append("<label class='col-md-5'>City: </label>");
    $('<div class="form-group col-md-6" id="dropdownCities"></div>').appendTo('#DivCity');
    $select = $('<select id="selectCities" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownCities');
    $('<option/>').val('0').text('Select city..').appendTo($select);
    $('<option/>').val('1').text('Nis').appendTo($select);
    $('<option/>').val('2').text('Beograd').appendTo($select);
    $('<option/>').val('3').text('London').appendTo($select);
    $('#selectCities')[0].selectedIndex = data.Type;

    $("<div class='form-group' id='DivNoLand'></div>").appendTo(modalBody);
    $('#DivNoLand').append("<label class='col-md-5'>Landmarks: </label>");
    $('#DivNoLand').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-tasks'></i></span><input class='col-md-6 form-control' type='text' id='LandmarkCount'/><br />");
    $('#LandmarkCount').val(data.LandmarkCount);
    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-thumbs-up'></span> OK</button>");
    popUpBody.modal("show");
}

function DetailsLandmark(data) {
    $('#HintsModal').show();
    var modalBody = $("#divHints");
    modalBody.empty();
    $('#DivHintsDetails').empty();
    $("<div class='form-group' id='DivHintsDetails'></div>").appendTo(modalBody);
    $("<div class='form-group' id='DivImage'></div>").appendTo($('#DivHintsDetails'));
    $('#DivImage').append("<img style='width:100ph; height:100px; margin:auto' src='" + ServerAddres + "Images/" + data.FilePathLandmark + "'>");

    $("<div class='form-group' id='DivNameL'></div>").appendTo($('#DivHintsDetails'));
    $('#DivNameL').append("<label class='col-md-2'>Name: </label>");
    $('#DivNameL').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-home'></i></span><input class='col-md-3 form-control' type='text' id='NameL' disabled /><br />");
    $('#NameL').val(data.Name);

    $("<div class='form-group col-md-12' id='DivCity'></div>").appendTo($('#DivHintsDetails'));
    $('#DivCity').append("<label class='col-md-2'>City: </label>");
    $('<div class="form-group col-md-3" id="dropdownCities"></div>').appendTo('#DivCity');
    $select = $('<select id="selectCities" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownCities');
    $('<option/>').val('0').text('Select city..').appendTo($select);
    $('<option/>').val('1').text('Nis').appendTo($select);
    $('<option/>').val('2').text('Beograd').appendTo($select);
    $('<option/>').val('3').text('London').appendTo($select);
    $('#selectCities')[0].selectedIndex = data.City;

    $("<div class='form-group col-md-12' id='DivHintsL'></div>").appendTo($('#DivHintsDetails'));
    $('#DivHintsL').append("<br /><button type='button' style='background-color:#D5B267' value = " + data.Id + " onclick='AddHint(this)' class='btn btn-default'><span class='glyphicon glyphicon-plus'></span> Add hint</button><br />");
}

function CloseDetailsLandmark() {
    $('#DivHintsDetails').remove();
}

function ShowAddHint() {
    $("<div class='form-group' id='DivDescriptionH'></div>").appendTo($('#right'));
    $('#DivDescriptionH').append("<label class='col-md-5'>Description: </label>");
    $('#DivDescriptionH').append("<input class='col-md-6 form-control' type='text' id='DescriptionH'/><br />");
    $('#DescriptionH').val(null);

    $("<div class='form-group' id='DivAnswerH'></div>").appendTo($('#right'));
    $('#DivAnswerH').append("<label class='col-md-5'>Answer: </label>");
    $('#DivAnswerH').append("<input class='col-md-6 corm-control' type='text' id='AnswerH'/><br />");
    $('#AnswerH').val(null);

    $('#right').append("<div class='col-md-2'></div><div class='col-md-2'><button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button></div>");
    $('#right').append("<div class='col-md-2'></div><div class='col-md-2'><button onclick='AddHintkYes(this)' class='btn btn-danger'><span class='glyphicon glyphicon-save'></span> Save</button></div>");
}

function ChangeHint(button) {
    var rowHints = $(button).parents('tr')[0];
    var data = $('#tableHints').DataTable().row(rowHints).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Change hint");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivDescriptionH'></div>").appendTo(modalBody);
    $('#DivDescriptionH').append("<label class='col-md-5'>Description: </label>");
    $('#DivDescriptionH').append("<input class='col-md-6 form-control' type='text' id='DescriptionH'/><br />");
    $('#DescriptionH').val(data.Description);

    $("<div class='form-group' id='DivAnswerH'></div>").appendTo(modalBody);
    $('#DivAnswerH').append("<label class='col-md-5'>City: </label>");
    $('#DivAnswerH').append("<input class='col-md-6 form-control' type='text' id='AnswerH'/><br />");
    $('#AnswerH').val(data.Answer);

    var modalFooter = $('#modalEdit');
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button>");
    modalFooter.append("<button value=" + id + " onclick='ChangeHintkYes(this)' class='btn btn-danger'><span class='glyphicon glyphicon-edit'></span> Change</button>");
    popUpBody.modal("show");
}

function ChangeHintkYes(button) {
    var id = button.value;
    var description = $('#DescriptionH').val();
    var answer = $('#AnswerH').val();

    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "id": id, "description": description, "answer": answer },
        url: "/Admin/ChangeHint",
        chache: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
            $('#tableHints').DataTable().ajax.reload().draw();
        }
    });
}

function DeleteHint(button) {
    var rowCompetition = $(button).parents('tr')[0];
    var data = $('#tableHints').DataTable().row(rowCompetition).data();
    var id = data.Id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();
    modalBody.append("<p>Are you sure you want to delete this hint?</p>");
    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'>No</button>");
    popUpBody.modal("show");
    modalFooter.append("<button value=" + id + " onclick='DeleteHintYes(this)' class='btn btn-danger'><span class='glyphicon glyphicon-trash'></span> Yes</button>");
}

function DeleteHintYes(button) {
    var id = button.value;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "id": id },
        url: "/Admin/DeleteHint",
        chache: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
            $('#tableHints').DataTable().ajax.reload().draw();
        }
    });
}

function AddCompetition() {
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Add competition");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivName'></div>").appendTo(modalBody);
    $('#DivName').append("<label class='col-md-5'>Name: </label>");
    $('#DivName').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-home'></i></span><input class='col-md-6 form-control' type='text' id='Name'/><br />");
    $('#Name').val(null);

    $("<div class='form-group' id='DivStartDate'></div>").appendTo(modalBody);
    $('#DivStartDate').append("<label class='col-md-5'>Starting date: </label>");
    $('#DivStartDate').append("<input class='col-md-6 form-control' type='date' id='StartingDate'/><br /><br />");
    $('#StartingDate').val(null);

    $("<div class='form-group' id='DivEndDate'></div>").appendTo(modalBody);
    $('#DivEndDate').append("<label class='col-md-5'>Ending date: </label>");
    $('#DivEndDate').append("<input type='date' class='col-md-6 form-control' id='EndingDate' /><br /><br />");
    $('#EndingDate').val(null);

    $("<div id='DivType'></div>").appendTo(modalBody);
    $('#DivType').append("<label class='col-md-5'>Type: </label>");

    $('<div class="form-group col-md-6" id="dropdownTypes"></div>').appendTo('#DivType');
    $select = $('<select id="selectTypes" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownTypes');
    $('<option/>').val('0').text('Select type..').appendTo($select);
    $('<option/>').val('1').text('All').appendTo($select);
    $('<option/>').val('2').text('Museums').appendTo($select);
    $('<option/>').val('3').text('Sights').appendTo($select);
    $('<option/>').val('4').text('Taverns').appendTo($select);
    $('<option/>').val('5').text('Clubs').appendTo($select);
    $('<option/>').val('6').text('Pubs').appendTo($select);

    $("<div class='form-group' id='DivCity'></div>").appendTo(modalBody);
    $('#DivCity').append("<label class='col-md-5'>City: </label>");
    $('<div class="form-group col-md-6" id="dropdownCities"></div>').appendTo('#DivCity');
    $select = $('<select id="selectCities" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownCities');
    $('<option/>').val('0').text('Select city..').appendTo($select);
    $('<option/>').val('1').text('Nis').appendTo($select);
    $('<option/>').val('2').text('Beograd').appendTo($select);
    $('<option/>').val('3').text('London').appendTo($select);

    $("<div class='form-group' id='DivNoLand'></div>").appendTo(modalBody);
    $('#DivNoLand').append("<label class='col-md-5'>Landmarks: </label>");
    $('#DivNoLand').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-tasks'></i></span><input class='col-md-6 form-control' type='text' id='LandmarkCount'/><br />");
    $('#LandmarkCount').val(null);

    modalBody.append('<div id="GoogleMapa">');
    modalBody.append('<div id="mapPlace" style="width:100%;height:250px;"></div>');
    var googlemapa = $('#GoogleMapa');

        var gsricpt = document.createElement("script");
        gsricpt.src = ServerAddres + "Scripts/Coders/GoogleScripts.js";

        document.head.appendChild(gsricpt);

    googlemapa.append('<button class="btn btn-default" onclick= "Send()" > Sent for validation</button ><br />');
    googlemapa.append('<button class="btn btn-default" onclick="GetRouts()"> Get routs</button>');
    googlemapa.append('<input type="number" class="form-control" placeholder="Number of players" value="0" id="numberOfTeammates" /> <br />');

    googlemapa.append("<div id='MapaButtons'></div>");
    $('#MapaButtons').append('<button class="btn btn-default" onclick="conf()">yes</button>');
    $('#MapaButtons').append('<button class="btn btn-default" onclick="conf2()">no</button>');
    $('#MapaButtons').hide();

    var modalFooter = $("#modalEdit");
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button>");
    modalFooter.append("<button onclick='AddCompetitionYes()' class='btn btn-danger'><span class='glyphicon glyphicon-save'></span> Save</button>");
    popUpBody.modal("show");
}

function AddCompetitionYes(button) {
    var name = $('#Name').val();
    var startdate = $('#StartingDate').val();
    var enddate = $('#EndingDate').val();
    var landmarkcount = button.value;
    var type = $('#selectTypes')[0].selectedIndex;
    var city = $('#selectCities')[0].selectedIndex;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "name": name, "startdate": startdate, "enddate": enddate, "city": city, "landmarkcount": landmarkcount, "type": type },
        url: "/Admin/AddCompetition",
        chache: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
            $('#tableCompetitions').DataTable().ajax.reload().draw();
        }
    });
}

function AddLandmark() {
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Add landmark");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivImageModal'></div>").appendTo(modalBody);
    $('#DivImageModal').append("<img src=''>");

    $("<div class='form-group' id='DivNameL'></div>").appendTo(modalBody);
    $('#DivNameL').append("<label class='col-md-5'>Name: </label>");
    $('#DivNameL').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-home'></i></span><input class='col-md-6 form-control' type='text' id='NameL'/><br />");
    $('#NameL').val(null);

    $("<div class='form-group' id='DivCity'></div>").appendTo(modalBody);
    $('#DivCity').append("<label class='col-md-5'>City: </label>");
    $('<div class="form-group col-md-6" id="dropdownCities"></div>').appendTo('#DivCity');
    $select = $('<select id="selectCities" style="float:right; margin-left:2%" class="form-control" />').appendTo('#dropdownCities');
    $('<option/>').val('0').text('Select city..').appendTo($select);
    $('<option/>').val('1').text('Nis').appendTo($select);
    $('<option/>').val('2').text('Beograd').appendTo($select);
    $('<option/>').val('3').text('London').appendTo($select);

    var modalFooter = $('#modalEdit');
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button>");
    modalFooter.append("<button onclick='AddLandmarkYes()' class='btn btn-danger'><span class='glyphicon glyphicon-save'></span> Save</button>");
    popUpBody.modal("show");
}

function AddLandmarkYes() {
    var city = $('#selectCities')[0].selectedIndex;
    var name = $('#NameL').val();
    var filePath = fileData[0].name;

    landmarkModel = new FormData();

    var extension = fileData[0].name.split('.').pop();
    if (extension == "png" || extension == "jpg" || extension == "jpeg" || extension == "JPG") {
        landmarkModel.append(fileData[0].name, fileData[0]);
    }
    else {
        fileData = null;
        var div = $('#DivImageModal');
        $('#FileLabel').remove();
        $('#divextension').remove();
        div.append("<p><div id = 'divextension'> Extension most be type pdf, jpg or png. Your file have extension " + extension + "</div></p>")
    }

    var formObject = {};
    formObject["City"] = city;
    formObject["Name"] = name;
    formObject["FilePathLandmark"] = filePath;

    for (var key in formObject) {
        landmarkModel.append(key, formObject[key]);
    }
    var landmark = landmarkModel

    $.ajax({
        type: "POST",
        dataType: "json",
        data: landmark,
        url: "/Admin/AddLandmark",
        contentType: false,
        processData: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
            $('#tableLandmarks').DataTable().ajax.reload().draw();
        }
    });
}

function AddHint(button) {
    var id = button.value;
    landmarkId = id;
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Add hint");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivDescriptionH'></div>").appendTo(modalBody);
    $('#DivDescriptionH').append("<label class='col-md-5'>Description: </label>");
    $('#DivDescriptionH').append("<input class='col-md-5 form-control' type='text' id='DescriptionH'/><br />");
    $('#DescriptionH').val(null);

    $("<div class='form-group' id='DivAnswerH'></div>").appendTo(modalBody);
    $('#DivAnswerH').append("<label class='col-md-5'>Answer: </label>");
    $('#DivAnswerH').append("<input class='col-md-5 form-control' type='text' id='AnswerH'/><br />");
    $('#AnswerH').val(null);

    var modalFooter = $('#modalEdit');
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button>");
    modalFooter.append("<div class='col-md-1'></div><button value=" + id + " onclick='AddHintYes(this)' class='btn btn-danger'><span class='glyphicon glyphicon-save'></span> Save</button>");
    popUpBody.modal("show");
}

function AddHintYes(button) {
    var description = $('#DescriptionH').val();
    var answer = $('#AnswerH').val();
    var landmarkId = button.value;
    var id = landmarkId;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "description": description, "answer": answer, "landmarkId": landmarkId },
        url: "/Admin/AddHint",
        chache: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
            $('#divHints').show();
            $('#tableHints').DataTable().ajax.reload().draw();
        }
    });
}

function UserLandmark(button) {
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Adding landmark");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivImageModal'></div>").appendTo(modalBody);
    $('#DivImageModal').append("<div id='browse'></div>");

    $('#divextension').remove();
    var div = $('#browse');
    $('#file').remove();
    $('#FileLabel').remove();
    div.append("<input type='file' size='60' id='file' style='color:white' />")
    div.append("<label style='color:#b94a48; font-size: 13px; font-family: initial;' id='FileLabel'><label>").appendTo(div);

    $("<div class='form-group' id='DivDescriptionLandmark'></div>").appendTo(modalBody);
    $('#DivDescriptionLandmark').append("<label class='col-md-5'>Description: </label>");
    $('#DivDescriptionLandmark').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-pencil'></i></span><input class='col-md-6 form-control' type='text' id='DescriptionLandmark'/><br />");
    $('#DescriptionLandmark').val(null);

    var modalFooter = $('#modalEdit');
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button>");
    modalFooter.append("<button onclick='AddLandmarkUser(this)' value='" + button.value + "' class='btn btn-danger'><span class='glyphicon glyphicon-save'></span> Save</button>");
    popUpBody.modal("show");
}

function AddLandmarkUser(button) {
    var description = $('#DescriptionLandmark').val();
    var file = $("#file")[0].files[0];
    var guid = button.value;

    if (file == null) {
            $('#FileWrong').remove();
            $('#FileLabel').append("<label id='FileWrong' style='color:#b94a48; font-size: 13px; font-family: initial;'>You have to add image! </label>");
            return;
        }
        else {
            $('#FileWrong').remove();
        }

    landmarkModel = new FormData();

    var extension = file.name.split('.').pop();
    if (extension == "jpeg" || extension == "jpg" || extension == "png" || extension == "JPG") {
        landmarkModel.append(file.name, file);
    }
    else {
        fileData = null;
        var div = $('#browse');
        $('#file').remove();
        $('#FileLabel').remove();
        $('#divextension').remove();
        div.append("<input type='file' size='60' id='file' style='color:white' />");
        div.append("<p><label style='color:#b94a48; font-size: 13px; font-family: initial;' id = 'divextension'> Extension most be type jpg or png. Your file have extension " + extension + "</label></p>");
        return;
    }

    var formObject = {};
    formObject["Picture"] = file.name;
    formObject["Description"] = description;
    formObject["UsersGuid"] = guid;

    for (var key in formObject) {
        landmarkModel.append(key, formObject[key]);
    }
    var landmark = landmarkModel

    $.ajax({
        type: "POST",
        dataType: "json",
        data: landmark,
        url: "/Admin/UserAddLandmark",
        contentType: false,
        processData: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
        }
    });
}

function Delete(newLandmarkId) {
    $('#DivNewLandmarkDelete' + landmarkId).remove();

    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "id": newLandmarkId},
        url: "/Admin/DeleteNewLandmark",
        contentType: false,
        processData: false,
        success: function (data) {
            $('#AdminModalDetails').modal('hide');
        }
    });
}