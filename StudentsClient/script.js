const base_url = "http://localhost:63414"
const service_endpoint = "/StudentsService.asmx"

const students_by_avg_mark_method_url = base_url + service_endpoint + "/GetStudentsFilteredByAverageMark";
const all_students_url = base_url + service_endpoint + "/GetAllStudents";

function findStudentsFilteredByAvgMark(postFunction) {
    lowerBound = document.getElementById("lowerBound").value;
    upperBound = document.getElementById("upperBound").value;

    if (lowerBound == '' || upperBound == '') {
        alert("Bounds not defined")
    }

    body = { 'lowerBound': lowerBound, 'upperBound': upperBound };
    postFunction(students_by_avg_mark_method_url, response => populateTabe(response.d), body)
}

function findAllStudents(postFunction) {
    postFunction(all_students_url, response => populateTabe(response.d))
}

function populateTabe(students) {
    const tableRowsHtml = students.map(s => `<tr><td>${s.Name}</td><td>${s.Surname}</td><td>${s.AvgMark}</td></tr>`).join("");
    document.getElementById('tableBody').innerHTML = tableRowsHtml;
}

function postAjax(url, done_func, body) {
    request = {
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: stringify(body),
        error: (_request, status) => {
            alert(`Ajax request failed ${status}`);
        }
    }

    $.ajax(request).done(done_func);
}

function postVanilla(url, done_func, body) {
    const xhr = new XMLHttpRequest();
    xhr.open('POST', url);
    xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');

    requestBody = stringify(body);

    xhr.send(requestBody);
    xhr.onload = () => {
        if (xhr.status != 200) {
            alert(`Vanilla JS request failed ${xhr.status}`);
        } else {
            const response = JSON.parse(xhr.response)
            done_func(response);
        }
    };
}

function stringify(data) {
    return data ? JSON.stringify(data) : undefined;
}
