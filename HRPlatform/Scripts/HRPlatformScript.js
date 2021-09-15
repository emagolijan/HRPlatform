$(document).ready(function () {

    var host = window.location.host;
    var port = "";
    var candidatesEndpoint = "/api/candidates/";
    var skillsEndpoint = "/api/skills/";
    var formActionCand = "Create";
    var formActionSkill = "Create";
    var editingId;

    $("body").on("click", "#btnDeleteCandidate", deleteCandidate);
    $("body").on("click", "#btnEditCandidate", editCandidate);

    $("body").on("click", "#btnDeleteSkill", deleteSkill);
    $("body").on("click", "#btnEditSkill", editSkill);
    $("#candformClear").on("click", candclearForm);
    $("#skillformClear").on("click", skillclearForm);
    $("#searchReset").on("click", filterReset);
    refreshTable();

    function candclearForm() {
        $("#candName").val("");
        $("#candDateOfBirth").val("");
        $("#candContactNumber").val("");
        $("#candEmail").val("");
        $('input:checkbox').prop('checked', false);
        formActionCand = "Create"
    }

    function skillclearForm() {
        $("#skillName").val("");
        formActionSkill = "Create"
    }

    function filterReset() {
        $("#candNameSearch").val("");
        $("#candSkillsSearch").val("");
        refreshTable();
    }

    function refreshTable() {

        var requestUrlCandidates = "http://" + host + port + candidatesEndpoint;
        $.getJSON(requestUrlCandidates, setCandidates);

        var requestUrlSkills = "http://" + host + port + skillsEndpoint;
        $.getJSON(requestUrlSkills, setSkills);

        $.getJSON(requestUrlSkills, function (data) {
            $("#candidateSkills").empty();
            for (var i = 0; i < data.length; i++) {
                $("#candidateSkills").append("<li><input type=checkbox value=" + data[i].Id + ">" + data[i].Name + "</li>");
            }
        });
    };

    $("#filterForm").submit(function (e) {
        e.preventDefault();

        var candName = $("#candNameSearch").val();
        var skillName = $("#candSkillsSearch").val();

        // objekat koji se salje
        var sendData = {
            "Name": candName,
            "SkillName": skillName,
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + port + "/api/candidates/search/",
            "data": sendData

        }).done(function (data, status) {
            console.log(data);
            setCandidates(data, status)

        }).fail(function (data) {
            alert(data);
        });
    });

    function setCandidates(data, status) {
        console.log("Status: " + status);

        var $container = $("#dataCandidates");
        $container.empty();

        if (status == "success") {
            console.log(data);
            // ispis naslova
            var div = $("<div></div>");
            var h2 = $("<h2>Candidates</h2>");
            div.append(h2);
            // ispis tabele
            var table = $("<table border=3 class='table table - responsive'></table>");
            var header = $("<tr><th>Name</th><th>Date of birth</th><th>Contact number</th><th>Email</th><th>Edit</th><th>Delete</th></tr>");

            table.append(header);

            for (i = 0; i < data.length; i++) {
                // prikazujemo novi red u tabeli
                var row = "<tr>";
                // prikaz podataka
                var displayData = "<td>" + data[i].Name + "</td><td>" + data[i].DateOfBirth + "</td><td>" + data[i].ContactNumber + "</td><td>" + data[i].Email + "</td>";

                var stringId = data[i].Id.toString();

                var displayEdit = "<td><button id=btnEditCandidate name=" + stringId + ">[Edit]</button></td>";
                var displayDelete = "<td><button id=btnDeleteCandidate name=" + stringId + ">[Delete]</button></td>";
                row += displayData + displayEdit + displayDelete + "</tr>";

                table.append(row);

                div.append(table);

                $container.append(div);
            }
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Error during fetch candidates!</h1>");
            div.append(h1);
            $container.append(div);
        }
    };

    function setSkills(data, status) {
        console.log("Status: " + status);

        var $container = $("#dataSkills");
        $container.empty();

        if (status == "success") {
            console.log(data);
            // ispis naslova
            var div = $("<div></div>");
            var h2 = $("<h2>Skills</h2>");
            div.append(h2);
            // ispis tabele
            var table = $("<table border=3 class='table table - responsive'></table>");
            var header = $("<tr><th>Name</th><th>Edit</th><th>Delete</th></tr>");

            table.append(header);

            for (i = 0; i < data.length; i++) {
                // prikazujemo novi red u tabeli
                var row = "<tr>";
                // prikaz podataka
                var displayData = "<td>" + data[i].Name + "</td>";

                var stringId = data[i].Id.toString();

                var displayEdit = "<td><button id=btnEditSkill name=" + stringId + ">[Edit]</button></td>";
                var displayDelete = "<td><button id=btnDeleteSkill name=" + stringId + ">[Delete]</button></td>";
                row += displayData + displayEdit + displayDelete + "</tr>";

                table.append(row);

                div.append(table);
         
                $container.append(div);
            }
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Error during fetch skills!</h1>");
            div.append(h1);
            $container.append(div);
        }
    };

    function deleteCandidate() {
        // izvlacimo {id}
        var deleteID = this.name;
        // saljemo zahtev 
        $.ajax({
            url: "http://" + host + port + candidatesEndpoint + deleteID.toString(),
            type: "DELETE",
        })
            .done(function (data, status) {
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Error during deleting!!!");
            });

    };

    function deleteSkill() {
        // izvlacimo {id}
        var deleteID = this.name;
        // saljemo zahtev 
        $.ajax({
            url: "http://" + host + port + skillsEndpoint + deleteID.toString(),
            type: "DELETE",
        })
            .done(function (data, status) {
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Error during deleting!!!");
            });

    };

    function editCandidate() {
        // izvlacimo id
        var editId = this.name;

        // saljemo zahtev da dobavimo tog candidata
        $.ajax({
            url: "http://" + host + port + candidatesEndpoint + editId.toString(),
            type: "GET",
        })
            .done(function (data, status) {
                $("#candName").val(data.Name);
                $("#candDateOfBirth").val(data.DateOfBirth);
                $("#candContactNumber").val(data.ContactNumber);
                $("#candEmail").val(data.Email);
                var skillIds = data.Skills.map(function (a) { return a.Id; });

                var requestUrlCandidates = "http://" + host + port + skillsEndpoint;
                $.getJSON(requestUrlCandidates, function (skills) {
                    $("#candidateSkills").empty();
                    for (var i = 0; i < skills.length; i++) {
                        if (skillIds.includes(skills[i].Id)) {
                            $("#candidateSkills").append("<li><input type=checkbox checked=checked value=" + skills[i].Id + ">" + skills[i].Name + "</li>");
                        }
                        else {
                            $("#candidateSkills").append("<li><input type=checkbox value=" + skills[i].Id + ">" + skills[i].Name + "</li>");
                        }
                    }
                });

                editingId = data.Id;
                formActionCand = "Update";
            })
            .fail(function (data, status) {
                formActionCand = "Create";
                alert("Desila se greska!");
            });
    };

    function editSkill() {
        // izvlacimo id
        var editId = this.name;

        $.ajax({
            url: "http://" + host + port + skillsEndpoint + editId.toString(),
            type: "GET",
        })
            .done(function (data, status) {
                $("#skillName").val(data.Name);
                editingId = data.Id;
                formActionSkill = "Update";
            })
            .fail(function (data, status) {
                formActionSkill = "Create";
                alert("Desila se greska!");
            });
    };

    $("#addCandForm").submit(function (e) {
        // sprecavanje default akcije forme
        e.preventDefault();
        var result = candidateValidation();

        if (!result) {
            return false;
        }

        var candName = $("#candName").val();
        var candDateOfBirth = $("#candDateOfBirth").val();
        var candContactNumber = $("#candContactNumber").val();
        var candEmail = $("#candEmail").val();
        var skills = [];
        $(':checkbox:checked').each(function (i) {
            skills[i] = { Id: $(this).val() };
        });
        var httpAction;
        var sendData;
        var url;

        // u zavisnosti od akcije pripremam objekat
        if (formActionCand === "Create") {
            httpAction = "POST";
            url = "http://" + host + port + candidatesEndpoint;

            sendData = {
                "Name": candName,
                "DateOfBirth": candDateOfBirth,
                "ContactNumber": candContactNumber,
                "Email": candEmail,
                "Skills": skills
            };
        }
        else {
            httpAction = "PUT";
            url = "http://" + host + port + candidatesEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Name": candName,
                "DateOfBirth": candDateOfBirth,
                "ContactNumber": candContactNumber,
                "Email": candEmail,
                "Skills": skills
            };
        }

        console.log("Objekat za slanje");
        console.log(sendData);

        $.ajax({
            url: url,
            type: httpAction,
            data: sendData
        })
            .done(function (data, status) {
                formActionCand = "Create";
                candclearForm();
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            })

    });

    $("#addSkillForm").submit(function (e) {
        // sprecavanje default akcije forme
        e.preventDefault();
        var result = skillValidation();

        if (!result) {
            return false;
        }

        var skillName = $("#skillName").val();
        var httpAction;
        var sendData;
        var url;

        // u zavisnosti od akcije pripremam objekat
        if (formActionSkill === "Create") {
            httpAction = "POST";
            url = "http://" + host + port + skillsEndpoint;

            sendData = {
                "Name": skillName,
            };
        }
        else {
            httpAction = "PUT";
            url = "http://" + host + port + skillsEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Name": skillName,
            };
        }

        console.log("Objekat za slanje");
        console.log(sendData);

        $.ajax({
            url: url,
            type: httpAction,
            data: sendData
        })
            .done(function (data, status) {
                formActionSkill = "Create";
                $("#skillName").val("");
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            })
    });
});