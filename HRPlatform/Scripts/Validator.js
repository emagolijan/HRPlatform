 function candidateValidation() {
        var candidateName = document.getElementById("candName").value;
        var candidateNameError = document.getElementById("candNameError");
        candidateNameError.innerHTML = "";

        var isValid = true;
        if (candidateName === "" || candidateName === undefined) {
            candidateNameError.innerHTML = "<span class=\"greska\">Name is required.</span>"
            isValid = false;
        }
        else if (candidateName.length > 40) {
            candidateNameError.innerHTML = "<span class=\"greska\">Name of candidate must not contain more than 40 characters.</span>"
            isValid = false;
        }
        return isValid;
}

function skillValidation() {
    var skillName = document.getElementById("skillName").value;
    var skillNameError = document.getElementById("skillNameError");
    skillNameError.innerHTML = "";

    var isValid = true;
    if (skillName === "" || skillName === undefined) {
        skillNameError.innerHTML = "<span class=\"greska\">Name is required.</span>"
        isValid = false;
    }
    else if (skillName.length > 20) {
        skillNameError.innerHTML = "<span class=\"greska\">Name of skill must not contain more than 20 characters.</span>"
        isValid = false;
    }
    return isValid;
}

