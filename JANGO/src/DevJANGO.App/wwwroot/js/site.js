function ValorNaoPermitido() {
    $(document).ready(function () {
        $('#nome_contacto').on('input', function () {
            if (/[0-9]/g.test(this.value)) {
           
                //$("#nomesms").val('O campo nome só permite letras')
                //    .css("background-color", "#E10E1C")
                //    .css("color", "#FFFFFF");
                alert("O campo nome só permite letras");
                nome_contacto.value = '';
            }
        });

    });
}
/*--------------------------Get Número de Vagas----------------------------*/
function SetVaga() {
    $(document).ready(function () {
        $("#txtVaga").val('Selecione uma turma');
        $("#TurmaId").change(function () {
            var turmaId = $("#TurmaId").val();
            if (turmaId == "") {
                //$("#txtVaga").val('0')
                $("#txtVaga").val('Selecione uma turma')
                    .css("background-color", "#E10E1C")
                    .css("color", "#FFFFFF");
            }
            else {
                GetItemNumeroDeVaga(turmaId);
            }
        });
     
    });
}
function GetItemNumeroDeVaga(turmaId) {
    $.ajax({
        async: true,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        url: '/AlunoMatriculados/GetNumeroDeVagaNaTurma',

        data: { turmaId: turmaId },
        success: function (data) {
            $("#txtVaga").val(data)
                .css("background-color", "#E9ECEF")
                .css("color", "#000000");
        }
    });
    SetVaga();
    return false;
}
/*--------------------------Get Número de Vagas Encarregado----------------------------*/
function SetVagaComEncarregado() {
    $(document).ready(function () {
        $("#txtVaga").val('Selecione uma turma');
        $("#TurmaId").change(function () {
            var turmaId = $("#TurmaId").val();
            if (turmaId == "") {
                //$("#txtVaga").val('0')
                $("#txtVaga").val('Selecione uma turma')
                    .css("background-color", "#E10E1C")
                    .css("color", "#FFFFFF");
            }
            else {
                GetNumeroDeVagaComEncarregado(turmaId);
            }
        });

    });
}
function GetNumeroDeVagaComEncarregado(turmaId) {
    $.ajax({
        async: true,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        url: '/AlunoMatriculados/GetNumeroDeVagaNaTurmaComEncarregado',

        data: { turmaId: turmaId },
        success: function (data) {
            $("#txtVaga").val(data)
                .css("background-color", "#E9ECEF")
                .css("color", "#000000");
        }
    });
    SetVagaComEncarregado();
    return false;
}


function SetIdade() {
    $(document).ready(function () {
        $("#txtIdade").val('Selecione o aluno');
        $("#AlunoInscritoId").change(function () {
            var alunoInscritoId = $("#AlunoInscritoId").val();
            if (alunoInscritoId == "") { 
                $("#txtIdade").val('Selecione o aluno')
                    .css("background-color", "#E10E1C")
                    .css("color", "#FFFFFF");
            }
            else {
                GetItemIdade(alunoInscritoId);
            }
        });

    });
}
function GetItemIdade(alunoInscritoId) {
    $.ajax({
        async: true,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        url: '/AlunoMatriculados/GetIdade',

        data: { alunoInscritoId : alunoInscritoId },
        success: function (data) {
            $("#txtIdade").val(data)
                .css("background-color", "#E9ECEF")
                .css("color", "#000000");
        }
    });
    SetIdade();
    return false;
}
