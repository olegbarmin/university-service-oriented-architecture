const base_url = "http://localhost:49893/StudentsService.svc/basic"

function findStudentsFilteredByAvgMark(postFunction) {
    lowerBound = document.getElementById("lowerBound").value;
    upperBound = document.getElementById("upperBound").value;

    if (lowerBound == '' || upperBound == '') {
        alert("Bounds not defined")
        return;
    }

    const requestBody =
        `<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:wcf="http://friends.com/wcf">` +
        `<soapenv:Header/>` +
        `<soapenv:Body>` +
        `<wcf:GetStudentsFilteredByAverageMark>` +
        `<wcf:lowerBound>${lowerBound}</wcf:lowerBound>` +
        `<wcf:upperBound>${upperBound}</wcf:upperBound>` +
        `</wcf:GetStudentsFilteredByAverageMark>` +
        `</soapenv:Body>` +
        `</soapenv:Envelope>`
    postFunction(requestBody, "GetStudentsFilteredByAverageMark",response => populateTabe(extractStudentsArray(response)))
}

function findAllStudents(postFunction) {
    const requestBody =
        `<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:wcf="http://friends.com/wcf">` +
        `<soapenv:Header/>` +
        `<soapenv:Body>` +
        `<wcf:GetAllStudents/>` +
        `</soapenv:Body>` +
        `</soapenv:Envelope>`

    postFunction(requestBody, "GetAllStudents", response => populateTabe(extractStudentsArray(response)))
}

function populateTabe(students) {
    const tableRowsHtml = students.map(s => `<tr><td>${s.Name}</td><td>${s.Surname}</td><td>${s.AvgMark}</td></tr>`).join("");
    document.getElementById('tableBody').innerHTML = tableRowsHtml;
}

function postAjax(body, method, done_func) {
    $.ajax(request = {
        type: "POST",
        url: base_url,
        beforeSend: request => request.setRequestHeader("SOAPAction", `http://friends.com/wcf/IStudentsService/${method}`),
        contentType: 'text/xml; charset=UTF-8',
        data: body,
        error: (_request, status) => {
            alert(`Ajax request failed ${status} `);
        }
    }).done(done_func);
}

function postVanilla(body, method, done_func) {
    const xhr = new XMLHttpRequest();
    xhr.open('POST', base_url);
    xhr.setRequestHeader('Content-Type', 'text/xml; charset=UTF-8')
    xhr.setRequestHeader("SOAPAction", '"http://friends.com/wcf/IStudentsService/' + method + '"');
    xhr.send(body);
    xhr.onload = () => {
        if (xhr.status != 200) {
            alert(`Vanilla JS request failed ${xhr.status} `);
        } else {
            done_func(xhr.responseXML);
        }
    };
}

function extractStudentsArray(xml) {
    const studentsXmlArray = Array.from(xml.getElementsByTagName("a:Student"));
    return Array.from(studentsXmlArray).map(studXml => extractStudentFromXml(studXml));
}

function extractStudentFromXml(xml) {
    return {
        Name: xml.getElementsByTagName("a:Name")[0].innerHTML,
        Surname: xml.getElementsByTagName("a:Surname")[0].innerHTML,
        AvgMark: xml.getElementsByTagName("a:AvgMark")[0].innerHTML
    }
}

function stringify(data) {
    return data ? JSON.stringify(data) : undefined;
}