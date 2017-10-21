var landmark;   //trenutni landmark
var hint;        //trenutni hint iako je mozda resen

function ModalClose() {
    $('#CompetitionModal').close();
}

function NextHint() {
    var hintId = hint.Id;
    var landmarkId = landmark;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "landmarkId": landmarkId, "hintId": hintId },
        url: "/Competition/NextHint",
        chache: false,
        success: function (data) {
            if (data != null) {
                hint = data;
                var modalHeader = $('#headerModal');
                modalHeader.empty();
                modalHeader.append("<h3>Next hint</h3>");
                var popUpBody = $('#CompetitionModal');
                var modalBody = $("#editDetails");
                modalBody.empty();

                $('#AnswerWrong').empty();
                $("<div class='form-group' id='DivDescription'></div>").appendTo(modalBody);
                $('#DivDescription').append("<label class='col-md-5'>Description: </label>");
                $('#DivDescription').append("<input class='col-md-5 form-control' type='text' id='Description'/><br />");
                $('#Description').val(data.Description);

                $("<div class='form-group' id='DivAnswer'></div>").appendTo(modalBody);
                $('#DivAnswer').append("<label class='col-md-5'>Answer: </label>");
                $('#DivAnswer').append("<input class='col-md-5 form-control' type='text' id='Answer'/><br />");
                $('#Answer').val(null);
                var modalFooter = $("#modalEdit");
                modalFooter.empty();
                modalFooter.append("<button onclick='AnswerLater()' class='btn btn-default'>Later</button>");
                modalFooter.append("<button onclick='AnswerForHint()' class='btn btn-default'>Try</button>");
                popUpBody.modal("show");
            }
        }
    });
}

function AnswerForHint() {
    if (hint.Answer != $('#Answer').val()) {
        $('#Answer').append("<label id='AnswerWrong'>The answer is not corect! Try something else </label>");
        $('#Answer').val(null);
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "json",
            data: { "id": id },
            url: "/Competition/HintCorect",
            chache: false,
            success: function (data) {
                ModalClose();
                var modalHeader = $('#headerModal');
                modalHeader.empty();
                modalHeader.append("<h3>Hint</h3>");
                var popUpBody = $('#CompetitionModal');
                var modalBody = $("#editDetails");
                modalBody.empty();
                modalBody.append("<p><h4>Congratulation!</h4></p> Corect answer. You just added some points to your team. Wait for next hint.</p>"); \
                var modalFooter = $("#modalEdit");
                modalFooter.empty();
                modalFooter.append("<button onclick='ModalClose()' class='btn btn-default'>Close</button>");
                popUpBody.modal("show");
            }
        });
    }
}

function ShowHint() {
    var div = $('CurrentHint');
}       //tvoriti div za pregled hinta

function AnswerLater() {
    $('CurrentHint');
    $('#HintDescription').va(hint.Description);
}

function Hint() {
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("Hint");
    var popUpBody = $('#AdminModalDetails');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $("<div class='form-group' id='DivDescription'></div>").appendTo(modalBody);
    $('#DivDescription').append("<label class='col-md-5'>Description: </label>");
    $('#DivDescription').append("<input class='col-md-5 form-control' type='text' id='Description'/><br />");
    $('#DescriptionH').val(hint.Description);

    $("<div class='form-group' id='DivAnswer'></div>").appendTo(modalBody);
    $('#DivAnswer').append("<label class='col-md-5'>Answer: </label>");
    $('#DivAnswer').append("<input class='col-md-5 form-control' type='text' id='Answer'/><br />");
    $('#Answer').val(null);

    var modalFooter = $('#modalEdit');
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-delete'></span> Close</button>");
    modalFooter.append("<div class='col-md-1'></div><button onclick='AnswerForHint()' class='btn btn-danger'><span class='glyphicon glyphicon-save'></span> Try</button>");
    popUpBody.modal("show");
}

function ShowLandmark() {
    var modalHeader = $('#headerModal');
    modalHeader.empty();
    modalHeader.append("<h3>Landmar</h3>");
    var popUpBody = $('#CompetitionModal');
    var modalBody = $("#editDetails");
    modalBody.empty();

    $('#DivImage').append("<img style='width:100ph; height:100px; margin:auto' src='" + ServerAddres + "Images/" + landmark.FilePathLandmark + "'>");

    $("<div class='form-group' id='DivNameL'></div>").appendTo(modalBody);
    $('#DivNameL').append("<label class='col-md-2'>Name: </label>");
    $('#DivNameL').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-home'></i></span><input class='col-md-3 form-control' type='text' id='NameL' disabled /><br />");
    $('#NameL').val(landmark.Name);

    $("<div class='form-group' id='DivCity'></div>").appendTo(modalBody);
    $('#DivCity').append("<label class='col-md-2'>City: </label>");
    $('#DivCity').append("<div class='input-group'><span class='input-group-addon'><i class='glyphicon glyphicon-home'></i></span><input class='col-md-3 form-control' type='text' id='CityL' disabled /> <br />");
    $('#CityL').val(landmark.City);

    var modalFooter = $('#modalEdit');
    modalFooter.empty();
    modalFooter.append("<button onclick='CloseModal()' class='btn btn-default'><span class='glyphicon glyphicon-thumbs-up'></span> Ok</button>");
}