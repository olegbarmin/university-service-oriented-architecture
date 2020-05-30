const base_url = "https://localhost:44313"

const students_by_avg_mark_method_url = base_url + "/GetStudentsFilteredByAverageMark";
const all_students_url = base_url + "/GetAllStudents";

function findStudentsFilteredByAvgMark(postFunction) {
    lowerBound = document.getElementById("lowerBound").value;
    upperBound = document.getElementById("upperBound").value;

    if (lowerBound == '' || upperBound == '') {
        alert("Bounds not defined")
    }

    const body = { Item1: lowerBound, Item2: upperBound };
    postFunction(students_by_avg_mark_method_url, "POST", response => populateTabe(response), body)
}

function findAllStudents(postFunction) {
    postFunction(all_students_url, "GET", response => populateTabe(response))
}

function populateTabe(students) {
    const tableRowsHtml = students.map(s => `<tr><td>${s.Name}</td><td>${s.Surname}</td><td>${s.AvgMark}</td></tr>`).join("");
    document.getElementById('tableBody').innerHTML = tableRowsHtml;
}

function postAjax(url, method, done_func, body) {
    request = {
        type: method,
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

function postVanilla(url, method, done_func, body) {
    const xhr = new XMLHttpRequest();
    xhr.open(method, url);
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