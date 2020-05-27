const base_url = "http://localhost:63414/StudentsService.asmx"

function findStudentsFilteredByAvgMark(postFunction) {
    lowerBound = document.getElementById("lowerBound").value;
    upperBound = document.getElementById("upperBound").value;

    if (lowerBound == '' || upperBound == '') {
        alert("Bounds not defined")
    }

    const requestBody =
        `<?xml version="1.0" encoding="utf-8"?>` +
        `<soap12:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">` +
        `<soap12:Body>` +
        `<GetStudentsFilteredByAverageMark xmlns="http://www.friends.com/">` +
        `<lowerBound>${lowerBound}</lowerBound>` +
        `<upperBound>${upperBound}</upperBound>` +
        `</GetStudentsFilteredByAverageMark>` +
        `</soap12:Body >` +
        `</soap12:Envelope >`
    postFunction(requestBody, response => populateTabe(extractStudentsArray(response)))
}

function findAllStudents(postFunction) {
    const requestBody =
        `<?xml version="1.0" encoding="utf-8"?>` +
        `<soap12:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">` +
        `<soap12:Body>` +
        `<GetAllStudents xmlns="http://www.friends.com/" />` +
        `</soap12:Body>` +
        `</soap12:Envelope>`
    postFunction(requestBody, response => populateTabe(extractStudentsArray(response)))
}

function populateTabe(students) {
    const tableRowsHtml = students.map(s => `<tr><td>${s.Name}</td><td>${s.Surname}</td><td>${s.AvgMark}</td></tr>`).join("");
    document.getElementById('tableBody').innerHTML = tableRowsHtml;
}

function postAjax(body, done_func) {
    $.ajax(request = {
        type: "POST",
        url: base_url,
        contentType: "application/soap+xml; charset=utf-8",
        data: body,
        error: (_request, status) => {
            alert(`Ajax request failed ${status}`);
        }
    }).done(done_func);
}

function postVanilla(body, done_func) {
    const xhr = new XMLHttpRequest();
    xhr.open('POST', base_url);
    xhr.setRequestHeader('Content-Type', 'application/soap+xml; charset=utf-8')

    xhr.send(body);
    xhr.onload = () => {
        if (xhr.status != 200) {
            alert(`Vanilla JS request failed ${xhr.status}`);
        } else {
            done_func(xhr.responseXML);
        }
    };
}

function extractStudentsArray(xml) {
    const studentsXmlArray = Array.from(xml.getElementsByTagName("Student"));
    return Array.from(studentsXmlArray).map(studXml => extractStudentFromXml(studXml));
}

function extractStudentFromXml(xml) {
    return {
        Name: xml.getElementsByTagName("Name")[0].innerHTML,
        Surname: xml.getElementsByTagName("Surname")[0].innerHTML,
        AvgMark: xml.getElementsByTagName("AvgMark")[0].innerHTML
    }
}

function stringify(data) {
    return data ? JSON.stringify(data) : undefined;
}
